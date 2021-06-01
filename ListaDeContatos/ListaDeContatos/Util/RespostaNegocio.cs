using ListaDeContatos.Enumerador;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Util
{
    public class RespostaNegocio
    {
        public Tipo Tipo { get; set; }
        public string Mensagem { get; set; }
        public dynamic Objeto { get; set; }
        public IEnumerable<IdentityError> Erros { get; set; }
    }
}
