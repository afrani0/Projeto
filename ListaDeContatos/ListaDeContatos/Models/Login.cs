using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Campo usuário é obrigatório.")]
        [StringLength(50, ErrorMessage = "Campo usuário não pode ser maior que 50 caracteres.")]
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Campo senha é obrigatório.")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Campo senha não pode ser maior que 50 caracteres.")]
        public string Senha { get; set; }
    }
}
