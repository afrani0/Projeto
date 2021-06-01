using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.DTO
{
    public class UsuarioNivelAcessoDTO
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string NomeUsuario { get; set; }
        public string NomeNivelAcesso { get; set; }
    }
}
