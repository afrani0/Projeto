using ListaDeContatos.Enumerador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.ViewModels
{
    public class ContatoViewModel
    {
        [Key]
        public int ContatoId { get; set; }
        [Display(Name = "Nome *")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "O Nome não pode ter mais do que 50 caracteres")]
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Nome { get; set; }
                
        [Display(Name = "Telefone *")]
        [RegularExpression(@"([0-9]{10,11})", ErrorMessage = "O Telefone válido deve ter 10 ou 11 dígitos.")]
        [Required(ErrorMessage = "Telefone é obrigatório.")]
        public string Telefone { get; set; }

        [Display(Name = "Telefone para Recado")]
        [RegularExpression(@"([0-9]{10,11})", ErrorMessage = "O Telefone para Recado válido deve ter 10 ou 11 dígitos.")]
        public string TelefoneRecado { get; set; }

        [Display(Name = "Data de Nascimento *")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Data de Nascimento é obrigatório.")]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Sexo *")]
        [Required(ErrorMessage = "Sexo é obrigatório.")]
        public bool? Sexo { get; set; }

        [Display(Name = "RG *")]
        [Required(ErrorMessage = "RG é obrigatório.")]
        [StringLength(11, MinimumLength = 8, ErrorMessage = "O RG válido deve ter 8 ou 11 dígitos")]
        [RegularExpression(@"([0-9]{8,11})", ErrorMessage = "Deve ser somente números.")]
        public string RG { get; set; }

        //selecionavel
        [Display(Name = "Estado *")]
        [Required(ErrorMessage = "Estado é obrigatório.")]
        public string Estado { get; set; }

        //selecionavel
        [Display(Name = "Cidade *")]
        [Required(ErrorMessage = "Cidade é obrigatório.")]
        public string Cidade { get; set; }

        //selecionavel
        [Display(Name = "Bairro *")]
        [Required(ErrorMessage = "Bairro é obrigatório.")]
        public string Bairro { get; set; }

        [Display(Name = "CEP")]
        [RegularExpression(@"[0-9]{8}", ErrorMessage = "CEP deve conter 8 números.")]
        public string CEP { get; set; }

        [Display(Name = "Rua *")]
        [Required(ErrorMessage = "A Rua é obrigatória.")]
        public string Rua { get; set; }

        [Display(Name = "Numero *")]
        [Required(ErrorMessage = "Número é obrigatório.")]
        [Range(1, 999999, ErrorMessage = "{0} não pode ser {1} e nem maior que {2}")]        
        public int? Numero { get; set; }

        [Display(Name = "Complemento *")]     
        [RequiredEnumAttribute(ErrorMessage = "Complemento é obrigatório.")]//anotation criada/customizada para quando for selecionado enum vazio... a classe que representa essa anotation foi criada na pasta 'Enumerador' 
        public Moradia? Complemento { get; set; }
    }
}
