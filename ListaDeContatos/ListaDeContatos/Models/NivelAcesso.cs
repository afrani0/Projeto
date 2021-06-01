using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Models
{
    public class NivelAcesso : IdentityRole
    {
        [Display(Name = "Nome")]
        public override string Name { get => base.Name; set => base.Name = value; }
        public string Descricao { get; set; }
        public ICollection<UsuarioNivelAcesso> UsuarioNivelAcessos { get; set; }


    }
}
