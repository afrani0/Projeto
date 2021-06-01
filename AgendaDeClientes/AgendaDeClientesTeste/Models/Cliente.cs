using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo não pode ser vazio.")]
        public string Nome { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo não pode ser vazio.")]
        public string Telefone { get; set; }
        public string TelefoneRecado { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo não pode ser vazio.")]
        public DateTime DataNascimento { get; set; }
        public DateTime? Atualizacao { get; set; }
        public bool Sexo { get; set; }
        public string URL { get; set; }
        public bool Ativo { get; set; }
        [NotMapped]
        public IFormFile Foto { get; set; }

        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
    }
}
