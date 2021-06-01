using ListaDeContatos.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Repositorio.Interface
{
    public interface IUsuarioRepositorio
    {
        Task<IdentityResult> CriarUsuario(Usuario usuario, string senha);
        Task<IdentityResult> EditarUsuario(Usuario usuario);
        Task<Usuario> BuscarUsuarioPorId(string id);
        Task<IList<string>> BuscarRolesPorUsuario(Usuario usuario);
        Task<IList<Usuario>> BuscarUsuarioPorRole(string role);
        Task<IdentityResult> RemoverRolesPorUsuario(Usuario usuario, IEnumerable<string> roles);
        bool DeletarUsuario(Usuario usuario, IEnumerable<string> roles);
        IQueryable<Usuario> ListarUsuarios();
        Task<Usuario> BuscarUsuarioPorNomeUsuario(string userName);

        Task<bool> ValidaSenhaParaUsuarioEspecificado(Usuario usuario, string senha);       
    }
}
