using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace React_Arquivos_Backend.VO.ArquivoVO
{
    public class ArquivoVO
    {
        public int ArquivoId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string Descricao { get; set; }
                
        [Required(ErrorMessage = "Campo Obrigatório.")]
        public IFormFile Imagem { get; set; }
    }
}
