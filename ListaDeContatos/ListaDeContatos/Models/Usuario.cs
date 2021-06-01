using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Models
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public bool PrimeiroAcesso { get; set; }
        
        public ICollection<UsuarioNivelAcesso> UsuarioNivelAcessos { get; set; }

    }
}
