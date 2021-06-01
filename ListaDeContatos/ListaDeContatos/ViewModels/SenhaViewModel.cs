using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.ViewModels
{
    public class SenhaViewModel
    {
        [Key]
        [Required(ErrorMessage = "Login deve ser selecionado")]
        public string UsuarioId { get; set; }
    }
}
