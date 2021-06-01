using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.ViewModels
{
    public class PrimeiroAcessoViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Senha Atual")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo Senha Atual é obrigatório")]
        [StringLength(20, ErrorMessage = "O campo Senha Atual não pode ter mais do que 20 caracteres")]
        public string SenhaAtual { get; set; }
        [Display(Name ="Nova Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo Nova Senha é obrigatório")]
        [StringLength(20, ErrorMessage = "O campo Nova Senha não pode ter mais do que 20 caracteres")]
        public string NovaSenha { get; set; }
        [Display(Name ="Confirmar Nova Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo Confirmar Nova Senha é obrigatório")]
        [StringLength(20, ErrorMessage = "O campo Confirmar Nova Senha não pode ter mais do que 20 caracteres")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
