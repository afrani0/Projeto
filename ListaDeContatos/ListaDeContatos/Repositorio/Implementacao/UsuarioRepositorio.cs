using ListaDeContatos.Models;
using ListaDeContatos.Repositorio.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Repositorio.Implementacao
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly UserManager<Usuario> _gerenciadorUsuarios;
        private ApplicationDBContext _applicationDBContext;

        public UsuarioRepositorio(UserManager<Usuario> gerenciadorUsuarios, ApplicationDBContext applicationDBContext)
        {
            _gerenciadorUsuarios = gerenciadorUsuarios;
            _applicationDBContext = applicationDBContext;
        }

        public virtual async Task<Usuario> BuscarUsuarioPorId(string id)
        {
            return await _gerenciadorUsuarios.FindByIdAsync(id);
        }

        public virtual async Task<IList<string>> BuscarRolesPorUsuario(Usuario usuario)
        {
            return await _gerenciadorUsuarios.GetRolesAsync(usuario);
        }

        public virtual async Task<IdentityResult> CriarUsuario(Usuario usuario, string senha)
        {
            usuario.Id = Guid.NewGuid().ToString();
            return await _gerenciadorUsuarios.CreateAsync(usuario, senha);

        }

        public virtual async Task<IdentityResult> EditarUsuario(Usuario usuario)
        {
            return await _gerenciadorUsuarios.UpdateAsync(usuario);
        }

        public virtual async Task<IList<Usuario>> BuscarUsuarioPorRole(string role)
        {
            return await _gerenciadorUsuarios.GetUsersInRoleAsync(role);
        }

        public virtual async Task<IdentityResult> RemoverRolesPorUsuario(Usuario usuario, IEnumerable<string> roles)
        {
            return await _gerenciadorUsuarios.RemoveFromRolesAsync(usuario, roles);

        }

        //Deleta usuário e 'roles/níveis acesso' associados
        public virtual bool DeletarUsuario(Usuario usuario, IEnumerable<string> roles)
        {
            using (var dbContextTransaction = _applicationDBContext.Database.BeginTransaction())
            {
                try
                {
                    var retornoRoles = _gerenciadorUsuarios.RemoveFromRolesAsync(usuario, roles).Result;
                    if (!retornoRoles.Succeeded)
                    {
                        dbContextTransaction.Rollback();
                        return false;
                    }

                    var retornoUsuarioDeletado = _gerenciadorUsuarios.DeleteAsync(usuario).Result;

                    if (!retornoUsuarioDeletado.Succeeded)
                    {
                        dbContextTransaction.Rollback();
                        return false;
                    }

                    dbContextTransaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return false;
                }
            }
        }

        public virtual IQueryable<Usuario> ListarUsuarios()
        {
            return _gerenciadorUsuarios.Users.AsNoTracking();
        }

        public virtual async Task<IdentityResult> MudarSenha(Usuario usuario, string SenhaAtual, string NovaSenha)
        {
            return await _gerenciadorUsuarios.ChangePasswordAsync(usuario, SenhaAtual, NovaSenha);
        }

        public virtual async Task<IdentityResult> MudarSenhaUsandoPerfilAdministrador(Usuario usuario, string novaSenha)
        {
            //É possível substituir a senha usando o parâmetro 'token' em vez da 'senha atual'.
            var token = await _gerenciadorUsuarios.GeneratePasswordResetTokenAsync(usuario);

            return await _gerenciadorUsuarios.ResetPasswordAsync(usuario, token, novaSenha);

        }

        public virtual Task<Usuario> BuscarUsuarioPorNomeUsuario(string userName)
        {
            return _gerenciadorUsuarios.FindByNameAsync(userName);
        }
        
        public virtual Task<bool> ValidaSenhaParaUsuarioEspecificado(Usuario usuario, string senha)
        {
            return _gerenciadorUsuarios.CheckPasswordAsync(usuario, senha);
        }

    }
}
