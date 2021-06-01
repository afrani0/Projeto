using Microsoft.AspNetCore.Http;
using React_Arquivos_Backend.Business.Interface;
using React_Arquivos_Backend.ExceptionCustom;
using React_Arquivos_Backend.Models;
using React_Arquivos_Backend.Repository.Implementation;
using React_Arquivos_Backend.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace React_Arquivos_Backend.Business.Implementation
{
    public class ArquivoBusiness : IArquivoBusiness
    {
        private readonly IArquivoRepository _repository;

        public ArquivoBusiness(IArquivoRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarImagem(Arquivo arquivo, IFormFile imagem)
        {
            if (arquivo == null) throw new ExceptionBusiness("Arquivo não pode ser null");

            if (imagem == null || imagem.Length == 0) throw new ExceptionBusiness("Imagem deve ser carregada");
            
            if (imagem.Length > 1048576) throw new ExceptionBusiness("Imagem não pode ser maior que 1 MB");//1MB
                                                                                                   //formatos validos png, jpg, ico, jpeg, gif
            var formatos = new []{ "image/png", "image/jpg", "image/vnd.microsoft.icon", "image/x-icon", "image/jpeg", "image/gif", "image/bpm" };
            if (!formatos.Contains(imagem.ContentType))
                throw new ExceptionBusiness("O Formato é inválido, formatos aceitos: png, jpg, ico, jpeg, gif");
            //nome deve ser único
            var existeNome = _repository.ListarImagem(string.Empty);
            if (existeNome.Any(a => a.Nome.ToUpper() == arquivo.Nome.ToUpper()))
                throw new ExceptionBusiness("Nome inválido. Já existe outro arquivo com mesmo nome.");

            await _repository.CriarImagem(arquivo, imagem);

        }

        public async Task EditarImagem(Arquivo arquivo, IFormFile imagem)
        {

            if (arquivo == null) throw new ExceptionBusiness("Arquivo não pode ser null");

            if (arquivo.ArquivoId == 0) throw new ExceptionBusiness("Arquivo não pode ter o ArquivoId igual a zero");

            //if (imagem == null || imagem.Length == 0) throw new ExceptionBusiness("Imagem deve ser carregada");

            if (imagem != null && imagem.Length > 1048576) throw new ExceptionBusiness("Imagem não pode ser maior que 1 MB");//1MB

            if (arquivo.ArquivoId == 0) throw new ExceptionBusiness("O id não pode ser zero");

            //formatos validos png, jpg, ico, jpeg, gif
            if (imagem != null)
            {
                var formatos = new[] { "image/png", "image/jpg", "image/vnd.microsoft.icon", "image/x-ico", "image/x-icon", "image/jpeg", "image/gif", "image/bpm" };
                if (!formatos.Contains(imagem.ContentType))
                    throw new ExceptionBusiness("O Formato é inválido, formatos aceitos: png, jpg, ico, jpeg, gif");
            }
            //nome deve ser único
            var existeNome = _repository.ListarImagem(string.Empty);
            if (existeNome.Any(a => a.Nome.ToUpper().Trim() == arquivo.Nome.ToUpper().Trim() && a.ArquivoId != arquivo.ArquivoId))
                throw new ExceptionBusiness("Nome inválido. Já existe outro arquivo com mesmo nome.");

            //arquivo.CaminhoImg = existeNome.FirstOrDefault().CaminhoImg;

            await _repository.EditarImagem(arquivo, imagem);
        }

        public async Task ExcluirImagem(Arquivo arquivo)
        {
            if (arquivo == null) throw new ExceptionBusiness("Arquivo não pode ser null");
            if (arquivo.ArquivoId == 0) throw new ExceptionBusiness("O id não pode ser zero");

            await _repository.ExcluirImagem(arquivo);
        }

        public IEnumerable<Arquivo> ListarImagem(string nomeImagem)
        {
            return _repository.ListarImagem(nomeImagem);
        }
    }
}
