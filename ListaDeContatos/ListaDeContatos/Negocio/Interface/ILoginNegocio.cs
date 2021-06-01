using ListaDeContatos.Models;
using ListaDeContatos.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Negocio.Interface
{
    public interface ILoginNegocio
    {
        Task<RespostaNegocio> Login(Usuario usuario, string senha);
        Task<RespostaNegocio> Logout();
        RespostaNegocio PrimeiroUsoDoSistema();
    }
}
