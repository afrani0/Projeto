using ListaDeContatos.Models;
using ListaDeContatos.Util;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Negocio.Interface
{
    public interface IRegistroNegocio
    {
        Task<RespostaNegocio> CriarUsuario(Usuario usuario, string senha);
        Task<RespostaNegocio> EditarUsuario(Usuario usuario);
        Task<RespostaNegocio> BuscarUsuarioPorId(string id);
        Task<RespostaNegocio> BuscarRolesPorUsuario(Usuario usuario);
        Task<RespostaNegocio> BuscarUsuarioPorRole(string role);
        Task<RespostaNegocio> RemoverRolesPorUsuario(Usuario usuario, IList<string> roles);
        Task<RespostaNegocio> DeletarUsuario(Usuario usuario);
        RespostaNegocio ListarUsuarios();
    }
}
