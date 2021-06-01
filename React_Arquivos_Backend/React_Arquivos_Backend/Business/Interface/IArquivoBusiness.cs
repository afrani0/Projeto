using Microsoft.AspNetCore.Http;
using React_Arquivos_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace React_Arquivos_Backend.Business.Interface
{
    public interface IArquivoBusiness
    {
        Task CriarImagem(Arquivo arquivo, IFormFile imagem);
        Task EditarImagem(Arquivo arquivo, IFormFile imagem);
        IEnumerable<Arquivo> ListarImagem(string nomeImagem);
        Task ExcluirImagem(Arquivo arquivo);
    }
}
