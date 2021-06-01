using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.ViewModel.Cliente
{
    public class _GenericClienteViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo não pode ser vazio.")]
        [MaxLength(50)]
        public string Nome { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo não pode ser vazio.")]
        //sempre que tiver um condição ou |, então deve ser colocado a condição de repetição maior primeiro. no exemplo [0-9]{11}|[0-9]{10}
        [RegularExpression(@"([0-9]{11}|[0-9]{10})", ErrorMessage = "Telefone deve ser somente numeros entre 10 a 11 digitos.")]
        public string Telefone { get; set; }
        [Display(Name = "Telefone para recado")]
        [RegularExpression(@"([0-9]{11}|[0-9]{10})", ErrorMessage = "Telefone deve ser somente numeros entre 10 a 11 digitos.")]
        public string TelefoneRecado { get; set; }
        [Display(Name = "Data de nascimento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo não pode ser vazio.")]
        public DateTime DataNascimento { get; set; }
        [Required(ErrorMessage = "Campo não pode ser vazio.")]
        public bool Sexo { get; set; }        

        [MaxLength(30)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo não pode ser vazio.")]
        public string Cidade { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo não pode ser vazio.")]
        [MaxLength(30)]
        public string Bairro { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo não pode ser vazio.")]
        [MaxLength(50)]
        public string Rua { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo não pode ser vazio.")]
        [Range(0, 99999, ErrorMessage = "Numero maximo até 99999")]
        public int Numero { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo não pode ser vazio.")]
        [MaxLength(20)]
        public string Complemento { get; set; }
    }
}
