using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.ViewModels
{
    public class UsuarioNivelAcessoViewModel
    {
        public string UserId { get; set; }
        [Display(Name ="Login")]
        public string NomeUsuario { get; set; }

        [Display(Name = "Nível Acesso")]
        [Required(ErrorMessage = "Nível Acesso é obrigatório")]
        public string RoleId { get; set; }

        [Display(Name = "Nome do Nível Acesso")]
        [DisplayFormat(DataFormatString = "{0}", NullDisplayText = "- Sem Associação -")]//responsável por ter uma mensagem padrão caso o 'NomeNivelAcesso' venha nulo ou vazio
        public string NomeNivelAcesso { get; set; }
    }
}
