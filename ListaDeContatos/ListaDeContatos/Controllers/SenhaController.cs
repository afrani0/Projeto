using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaDeContatos.Negocio.Implementacao;
using ListaDeContatos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeContatos.Controllers
{
    [Authorize]
    public class SenhaController : Controller
    {
        private readonly SenhaNegocio _senhaNegocio;
        private readonly RegistroNegocio _registroNegocio;

        public SenhaController(SenhaNegocio senhaNegocio, RegistroNegocio registroNegocio)
        {
            _senhaNegocio = senhaNegocio;
            _registroNegocio = registroNegocio;
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult EditarSenha()
        {
            try
            {
                var listaUsuario = _registroNegocio.ListarUsuarios();

                if (listaUsuario.Tipo == Enumerador.Tipo.Erro)
                {
                    TempData["Erro"] = listaUsuario.Mensagem;
                    return RedirectToAction("ListarContatos");
                }

                ViewBag.ListaUsuarios = listaUsuario.Objeto;

                return View(new SenhaViewModel() { UsuarioId = "" });

            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro: um erro ocorreu ao tentar carregar lista de usuários para edição de senha, entre em contato com o Administrador";
                return RedirectToAction(nameof(ListaDeContatos.Controllers.ContatoController.ListarContatos));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult EditarSenha(SenhaViewModel senhaViewModel)
        {
            try
            {
                var listaUsuario = _registroNegocio.ListarUsuarios();

                if (listaUsuario.Tipo == Enumerador.Tipo.Erro)
                {
                    TempData["Erro"] = listaUsuario.Mensagem;
                    return RedirectToAction("ListarContatos");
                }

                ViewBag.ListaUsuarios = listaUsuario.Objeto;

                if (ModelState.IsValid)
                {
                    var retornoNegocio = _senhaNegocio.NovaSenha(senhaViewModel.UsuarioId).Result;

                    if (retornoNegocio.Tipo == Enumerador.Tipo.Erro)
                    {
                        ViewBag.Erro = retornoNegocio.Mensagem;
                        return View(senhaViewModel);
                    }
                    else if (retornoNegocio.Tipo == Enumerador.Tipo.Aviso)
                    {
                        ViewBag.Aviso = retornoNegocio.Mensagem;
                        return View(senhaViewModel);
                    }
                    else
                    {
                        ViewBag.Sucesso = retornoNegocio.Mensagem;
                        ViewBag.Senha = retornoNegocio.Objeto;

                        return View(senhaViewModel);

                    }

                }
                ViewBag.Senha = "";
                return View(senhaViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Erro: um erro ocorreu ao tentar criar nova senha do usuário selecionado, entre em contato com o Administrador";
                return View(senhaViewModel);
            }
        }

    }
}