using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.ViewModels
{
    public class UsuarioViewModel
    {
        public string Id { get; set; }
        [StringLength(30, MinimumLength = 1, ErrorMessage = "O Nome não pode ter mais do que 30 caracteres")]
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Nome { get; set; }
        [Display(Name="Sobre Nome")]
        [Required(ErrorMessage = "Sobre Nome é obrigatório.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O Sobre Nome não pode ter mais do que 100 caracteres")]
        public string SobreNome { get; set; }
        [Display(Name = "Nome Usuário")]
        [Required(ErrorMessage = "Nome Usuário é obrigatório.")]
        [StringLength(256, MinimumLength = 1, ErrorMessage = "O Nome Usuário não pode ter mais do que 256 caracteres")]
        public string NomeUsuario { get; set; }
        [Required(ErrorMessage = "Email é obrigatório.")]
        [DataType(DataType.EmailAddress)]
        [StringLength(256, ErrorMessage = "O Email não pode ter mais do que 256 caracteres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória.")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "A Senha não pode ter mais do que 20 caracteres")]
        public string Senha { get; set; }
    }
}
