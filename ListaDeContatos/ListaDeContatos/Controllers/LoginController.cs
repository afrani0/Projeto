using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaDeContatos.Models;
using ListaDeContatos.Negocio.Implementacao;
using ListaDeContatos.Negocio.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace ListaDeContatos.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly ILoginNegocio _loginNegocio;
        private readonly IRegistroNegocio _registroUsuarios;

        public LoginController(LoginNegocio loginNegocio, RegistroNegocio registroUsuarios)
        {
            _loginNegocio = loginNegocio;
            _registroUsuarios = registroUsuarios;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty((string)TempData["Sucesso"]))
                ViewBag.Sucesso = (string)TempData["Sucesso"];

            if (!string.IsNullOrEmpty((string)TempData["Aviso"]))
                ViewBag.Aviso = (string)TempData["Aviso"];

            if (!string.IsNullOrEmpty((string)TempData["Erro"]))
                ViewBag.Erro = (string)TempData["Erro"];

            try
            {

                //Caso não exista nenhum usuário criado no sistema, então será criado um usuario 'adm' com perfil 'Administrador'
                var primeiroAcessoSistema = _loginNegocio.PrimeiroUsoDoSistema();

                if (primeiroAcessoSistema.Tipo == Enumerador.Tipo.Erro)
                {
                    ViewBag.Erro = primeiroAcessoSistema.Mensagem;
                    return View();
                }
                else if (primeiroAcessoSistema.Tipo == Enumerador.Tipo.Aviso)
                {
                    ViewBag.Aviso = primeiroAcessoSistema.Mensagem;
                    return View();
                }

                return View();

            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Erro: um erro ocorreu ao validar se é a primeira vez que o sistema é usado, entre em contato com o Administrador";
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Caso não exista nenhum usuário criado no sistema, então será criado um usuario 'adm' com perfil 'Administrador'
                    var primeiroAcessoSistema = _loginNegocio.PrimeiroUsoDoSistema();

                    if (primeiroAcessoSistema.Tipo == Enumerador.Tipo.Erro)
                    {
                        ViewBag.Erro = primeiroAcessoSistema.Mensagem;
                        return View();
                    }
                    else if (primeiroAcessoSistema.Tipo == Enumerador.Tipo.Aviso)
                    {
                        ViewBag.Aviso = primeiroAcessoSistema.Mensagem;
                        return View();
                    }

                    var retornoLogin = await _loginNegocio.Login(new Usuario() { UserName = login.Usuario }, login.Senha);
                    
                    if (retornoLogin.Tipo == Enumerador.Tipo.Erro)
                    {
                        ViewBag.Erro = retornoLogin.Mensagem;
                        return View(login);
                    }
                    else if (retornoLogin.Tipo == Enumerador.Tipo.Aviso)
                    {
                        ViewBag.Aviso = retornoLogin.Mensagem;
                        return View(login);
                    }
                    else if (retornoLogin.Tipo == Enumerador.Tipo.Sucesso)
                    {
                        if (((Usuario)retornoLogin.Objeto).PrimeiroAcesso)
                            return RedirectToAction("PrimeiroAcesso", "PrimeiroAcesso", retornoLogin.Objeto);
                        else
                            return RedirectToAction("ListarContatos", "Contato");
                    }
                }

                return View();

            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Erro: um erro ocorreu ao tentar Logar o usuário, entre em contato com o Administrador";
                return View();
            }
        }


        public IActionResult LogOut()
        {
            try
            {
                _loginNegocio.Logout();
                return RedirectToAction("Login", "Login");

            }
            catch (Exception)
            {
                ViewBag.Erro = "Erro: um erro ocorreu ao tentar fazer Logout do usuário, entre em contato com o Administrador";
                return View();
            }
        }
    }
}