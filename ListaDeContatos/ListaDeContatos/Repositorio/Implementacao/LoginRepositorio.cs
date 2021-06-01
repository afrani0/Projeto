using ListaDeContatos.Models;
using ListaDeContatos.Repositorio.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Repositorio.Implementacao
{
    public class LoginRepositorio : ILoginRepositorio
    {
        private SignInManager<Usuario> _gerenciadorLogin;

        public LoginRepositorio(SignInManager<Usuario> gerenciadorLogin)
        {
            _gerenciadorLogin = gerenciadorLogin;
        }

        public virtual async Task Login(Usuario usuario)
        {
           await _gerenciadorLogin.SignInAsync(usuario, false);
        }

        public virtual async Task Logout()
        {
            await _gerenciadorLogin.SignOutAsync();
        }
    }
}
