using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Models
{
    public class Endereco
    {
        public int EnderecoId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo não pode ser vazio.")]
        public string Cidade { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo não pode ser vazio.")]
        public string Bairro { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo não pode ser vazio.")]
        public string Rua { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo não pode ser vazio.")]
        public int Numero { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo não pode ser vazio.")]
        public string Complemento { get; set; }

        //public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

    }
}
