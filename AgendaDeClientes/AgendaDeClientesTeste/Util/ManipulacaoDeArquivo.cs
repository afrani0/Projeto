using AgendaDeClientes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgendaDeClientes.Util
{
    public class ManipulacaoDeArquivo
    {
        public virtual string SalvarArquivo(IFormFile arquivo)
        {
            //criando pasta dentro do projeto se não existir , seria utilizada para um client-side local, para este pegar a imagem desta pasta e colocar em wwwroot dele

            var local = System.Environment.CurrentDirectory.Replace("AgendaDeClientesTeste", "Imagens");

            try
            {
                #region criando pasta
                // Determina se existe esse diretório.
                if (!Directory.Exists(local))
                {
                    // Cria o Diretório caso o mesmo ainda não exista.
                    DirectoryInfo di = Directory.CreateDirectory(local);
                    Console.WriteLine("O diretório local foi criado em: {0}.", Directory.GetCreationTime(local));
                }
                #endregion
                string caminhoDoArquivo = "";

                if (arquivo.Length > 0)
                { 
                    caminhoDoArquivo = local + "\\" + Guid.NewGuid() + arquivo.ContentType.Replace("image/", "."); ;
                    using (var stream = System.IO.File.Create(caminhoDoArquivo))
                    {
                        arquivo.CopyTo(stream);
                    }
                }

                return caminhoDoArquivo;
            }
            catch (Exception ex)
            {
                throw new Exception("Atenção: Ocorreu um erro ao tentar salvar a imagem.");
            }
        }

        public virtual bool ExcluirArquivo(string url)
        {
            try
            {
                System.IO.File.Delete(url);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Atenção: Ocorreu um erro ao tentar excluir imagem da pasta local.");
            }
        }

    }
}
