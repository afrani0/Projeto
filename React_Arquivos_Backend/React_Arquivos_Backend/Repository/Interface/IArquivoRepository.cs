using Microsoft.AspNetCore.Http;
using React_Arquivos_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace React_Arquivos_Backend.Repository.Interface
{
    public interface IArquivoRepository
    {
        Task CriarImagem(Arquivo arquivo, IFormFile imagemArquivo);
        IEnumerable<Arquivo> ListarImagem(string nomeImagem);
        Task EditarImagem(Arquivo arquivo, IFormFile imagemArquivo);
        Task ExcluirImagem(Arquivo arquivo);
    }
}
