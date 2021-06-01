using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Models
{
    public class UsuarioNivelAcesso : IdentityUserRole<string>
    {
        public virtual Usuario Usuario { get; set; }
        public virtual NivelAcesso NivelAcesso { get; set; }

        public override string UserId { get; set; }        
        public override string RoleId { get; set; }
    }
}
