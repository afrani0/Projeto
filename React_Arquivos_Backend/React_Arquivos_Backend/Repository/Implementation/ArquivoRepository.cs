using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using React_Arquivos_Backend.ExceptionCustom;
using React_Arquivos_Backend.ManagementAplicationDB;
using React_Arquivos_Backend.Models;
using React_Arquivos_Backend.Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace React_Arquivos_Backend.Repository.Implementation
{
    public class ArquivoRepository : IArquivoRepository
    {
        private static string Host;
        private static string DiretorioRemoto;
        private static string UserName;
        private static string Password;

        public readonly DBAplication _context;
        //inject the whole configuration and get the value you want with a specified key
        private readonly IConfiguration _config;

        public ArquivoRepository(DBAplication context, IConfiguration config)
        {
            _context = context;
            _config = config;
            Host = _config.GetValue<string>("DadosArquivo:Host");
            DiretorioRemoto = _config.GetValue<string>("DadosArquivo:PastaRemota");
            UserName = _config.GetValue<string>("DadosArquivo:UserName");
            Password = _config.GetValue<string>("DadosArquivo:Password");
        }



        public async Task CriarImagem(Arquivo arquivo, IFormFile imagemArquivo)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    PreencherDadosArquivo(arquivo, imagemArquivo);

                    //salvar os valores no arquivo antes de salvar

                    //salvar a imagem
                    _context.Arquivos.Add(arquivo);
                    await _context.SaveChangesAsync();

                    var sucesso = await UploadFileCriacaoEdicao(imagemArquivo, arquivo);
                    if (sucesso)
                        transaction.Commit();
                    else
                        throw new ExceptionBusiness("Atenção: Os dados não foram salvos, Ocorreu um erro ao tentar salvar a imagem.");
                }
                catch (ExceptionBusiness ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        private Arquivo PreencherDadosArquivo(Arquivo arquivo, IFormFile imagemArquivo)
        {
            //deve acrescentar ao nome do arquivo, um '_GUID'
            var guid = Guid.NewGuid();
            arquivo.CaminhoImg = string.Format(
             "{0}{1}_{2}",
                _config.GetValue<string>("DadosArquivo:URLArquivo"),
                guid,
                imagemArquivo.FileName);
            arquivo.Tamanho = (Int32)imagemArquivo.Length;
            arquivo.Formato = imagemArquivo.ContentType.ToLower().Replace("image/", "");
            return arquivo;
        }

        public async Task EditarImagem(Arquivo arquivo, IFormFile imagemArquivo)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                var caminhoComNomeImgAntigo = arquivo.CaminhoImg;
                try
                {
                    if (imagemArquivo != null)
                    {
                        PreencherDadosArquivo(arquivo, imagemArquivo);
                        //salvar edição da imagem
                        _context.Entry(arquivo).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        var sucesso = await UploadFileCriacaoEdicao(imagemArquivo, arquivo);
                        if (sucesso)
                            transaction.Commit();
                        else
                            throw new ExceptionBusiness("Erro: Os dados não foram modificados, Ocorreu um erro ao tentar salvar a nova imagem.");

                    }
                    else
                    {
                        _context.Entry(arquivo).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                }
                catch (ExceptionBusiness ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }

                if (imagemArquivo != null && DeleteFile(caminhoComNomeImgAntigo) == false)
                    throw new ExceptionBusiness("*** Aviso: O arquivo e a nova imagem foram substituídos " +
                        "com sucesso, porém devido a uma falha a imagem antiga não foi deletada. " +
                        "Entre em contato com o Administrador para remoção da imagem antiga.");
            }
        }

        public async Task ExcluirImagem(Arquivo arquivo)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Arquivos.Remove(arquivo);
                    await _context.SaveChangesAsync();

                    if (DeleteFile(arquivo.CaminhoImg) == false)
                        throw new ExceptionBusiness("Erro: Os dados do arquivo não foram excluídos, " +
                            "Ocorreu um erro ao tentar deletar a imagem.");

                    transaction.Commit();
                }
                catch (ExceptionBusiness ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public IEnumerable<Arquivo> ListarImagem(string nomeImagem)
        {
            nomeImagem = nomeImagem == null ? "" : nomeImagem;
            return _context.Arquivos.Where(a => a.Nome.ToUpper().Contains(nomeImagem.ToUpper()));
        }

        #region Método para fazer upload do arquivo
        private async Task<bool> UploadFileCriacaoEdicao(IFormFile formFile, Arquivo arquivo)
        {

            var fileName = arquivo.CaminhoImg.Split("//").Last();

            Console.WriteLine("Creating client and connecting");
            //using (var client = new SftpClient(Host, 21, UserName, Password))
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(Host + DiretorioRemoto + fileName));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(UserName, Password);
                request.UseBinary = true;

                // Copy the contents of the file to the request stream.
                byte[] fileContents;

                /*
                 Ao contrário de salvar os dados como uma string (que aloca mais memória do que o necessário
                 e pode não funcionar se os dados binários contiverem bytes nulos), seria recomendado uma
                 abordagem mais parecida conforme abaixo para arquivos vindos em MEMÓRIA como iformfile
                 */
                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);
                    fileContents = memoryStream.ToArray();
                }

                /*nesse modelo abaixo os dados estavam sendo carregados com bytes maiores que o arquivo original 
                 ocasionando erro ao abrir o arquivo, por essa razão foi comentado*/
                //using (StreamReader sourceStream = new StreamReader(formFile.OpenReadStream()))
                //{
                //    fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                //}

                request.ContentLength = fileContents.Length;

                using (Stream requestStream = await request.GetRequestStreamAsync())//inicia o envio do arquivo por ftp, com zero byte
                {
                    await requestStream.WriteAsync(fileContents, 0, fileContents.Length); // passa todos os bytes restantes via ftp concluindo o arquivo enviado
                }

                using (FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync())//pega a resposta/status do arquivo enviado
                {
                    Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Método para fazer exclusão do arquivo

        private bool DeleteFile(string urlArquivo)
        {
            bool sucesso = false;

            Console.WriteLine("Creating client and connecting");
            //using (var client = new SftpClient(Host, 21, UserName, Password))
            try
            {
                var fileName = urlArquivo.Split("//").Last();

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(Host + DiretorioRemoto + fileName));
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(UserName, Password);
                request.UseBinary = true;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine("O status é: " + response.StatusCode);
                    Console.WriteLine("Directory List Complete, status {0}", response.StatusDescription);
                }
                sucesso = true;

            }
            catch (Exception ex)
            {
                return sucesso;
            }

            return sucesso;
        }

        #endregion

        /*private async Task<bool> SalvarImagemPastaLocal(IFormFile imagemArquivo, string caminhoComNomeImg)
        {
            var sucesso = false;
            //deve salvar a imagem            
            if (imagemArquivo.Length > 0)
            {
                //string filePath = Path.Combine(uploads, file.FileName);
                string filePath = caminhoComNomeImg;
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imagemArquivo.CopyToAsync(fileStream);
                    sucesso = true;
                }
            }

            return sucesso;
        }*/

        /*
        private bool RemoveImagemPastaLocal(string caminhoComNomeImg)
        {
            var sucesso = false;

            // Delete a file by using File class static method...
            if (System.IO.File.Exists(caminhoComNomeImg))
            {
                System.IO.File.Delete(caminhoComNomeImg);
                sucesso = true;
            }

            return sucesso;
        }*/
    }
}
