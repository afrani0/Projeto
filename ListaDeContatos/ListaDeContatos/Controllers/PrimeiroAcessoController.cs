using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaDeContatos.Models;
using ListaDeContatos.Negocio.Implementacao;
using ListaDeContatos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeContatos.Controllers
{
    [Authorize]
    public class PrimeiroAcessoController : Controller
    {
        private readonly PrimeiroAcessoNegocio _primeiroAcesso;
        
        public PrimeiroAcessoController(PrimeiroAcessoNegocio primeiroAcesso)
        {
            _primeiroAcesso = primeiroAcesso;
        }

        [HttpGet]        
        public IActionResult PrimeiroAcesso(Usuario usuario)
        {
            try
            {

                if (!string.IsNullOrEmpty((string)TempData["Sucesso"]))
                    ViewBag.Sucesso = (string)TempData["Sucesso"];

                if (!string.IsNullOrEmpty((string)TempData["Aviso"]))
                    ViewBag.Aviso = (string)TempData["Aviso"];

                if (!string.IsNullOrEmpty((string)TempData["Erro"]))
                    ViewBag.Erro = (string)TempData["Erro"];

                if (usuario == null)
                {
                    ViewBag.Erro = "Erro: Usuário não pode ser nulo, entre em contato com o Administrador";
                    return RedirectToAction("Login", "Login");
                }

                var primeiroAcesso = new PrimeiroAcessoViewModel();
                primeiroAcesso.Id = usuario.Id;

                return View(primeiroAcesso);
            }
            catch (Exception)
            {
                TempData["Erro"] = "Erro: um erro ocorreu ao tentar carregar os dados do primeiro acesso, entre em contato com o Administrador";
                return RedirectToAction(nameof(LoginController.Login), "Login");
            }

        }

        [HttpPost]        
        [AllowAnonymous]
        public async Task<IActionResult> PrimeiroAcesso(PrimeiroAcessoViewModel primeiroAcesso)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var retornoNegocio = await _primeiroAcesso.ConcluirPrimeiroAcesso(primeiroAcesso.Id, primeiroAcesso.SenhaAtual, primeiroAcesso.NovaSenha, primeiroAcesso.ConfirmarNovaSenha);

                    ModelState.Clear();

                    if (retornoNegocio.Tipo == Enumerador.Tipo.Sucesso)
                    {
                        TempData["Sucesso"] = retornoNegocio.Mensagem;
                        return RedirectToAction(nameof(LoginController.Login), "Login");
                    }
                    else if (retornoNegocio.Tipo == Enumerador.Tipo.Aviso)
                    {
                        ViewBag.Aviso = retornoNegocio.Mensagem;
                        return View("PrimeiroAcesso", primeiroAcesso);
                    }
                    else if (retornoNegocio.Tipo == Enumerador.Tipo.Erro)
                    {
                        if (retornoNegocio.Erros != null && 0 < (retornoNegocio.Erros.Where(e =>
                         e.Code == "PasswordTooShort" || e.Code == "PasswordRequiresNonAlphanumeric" || e.Code == "PasswordRequiresDigit"
                        || e.Code == "PasswordRequiresUpper" || e.Code == "PasswordRequiresLower" || e.Code == "PasswordMismatch").ToList().Count))
                        {

                            var erros = retornoNegocio.Erros;

                            foreach (var erro in erros)
                            {
                                if (erro.Code.Contains("PasswordTooShort"))
                                    ModelState.AddModelError("", "Nova Senha deve ter ao menos 6 caracteres.");
                                if (erro.Code.Contains("PasswordRequiresNonAlphanumeric"))
                                    ModelState.AddModelError("", "Nova Senha deve ter ao menos 1 caracter especial.");
                                if (erro.Code.Contains("PasswordRequiresDigit"))
                                    ModelState.AddModelError("", "Nova Senha deve ter ao menos 1 digito (entre 0 e 9).");
                                if (erro.Code.Contains("PasswordRequiresUpper"))
                                    ModelState.AddModelError("", "Nova Senha deve ter ao menos 1 caracter maiúsculo (entre 'A' e 'Z').");
                                if (erro.Code.Contains("PasswordRequiresLower"))
                                    ModelState.AddModelError("", "Nova Senha deve ter ao menos 1 caracter minúsculo (entre 'a' e 'z').");
                                if (erro.Code.Contains("PasswordMismatch"))
                                    ModelState.AddModelError("", "A Senha Atual digitada está incorreta.");


                            }
                            return View(primeiroAcesso);
                        }
                        else
                        {
                            TempData["Erro"] = retornoNegocio.Mensagem;
                            return RedirectToAction(nameof(LoginController.Login), "Login");

                        }

                    }

                }
                return View(primeiroAcesso);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro: um erro ocorreu ao tentar fazer o primeiro acesso, entre em contato com o Administrador";
                return RedirectToAction(nameof(LoginController.Login), "Login");
            }
        }
    }
}