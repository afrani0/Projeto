using ListaDeContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Repositorio.Interface
{
    public interface ILoginRepositorio
    {
        Task Login(Usuario usuario);
        Task Logout();
    }
}
