using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaDeContatos.DTO;
using ListaDeContatos.Models;
using ListaDeContatos.Negocio.Implementacao;
using ListaDeContatos.Repositorio.Implementacao;
using ListaDeContatos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ListaDeContatos.Controllers
{
    [Authorize]
    public class AssociarUsuarioController : Controller
    {
        private readonly UserManager<Usuario> _gerenciadorUsuarios;
        private readonly SignInManager<Usuario> _gerenciadorLogin;
        private readonly RoleManager<NivelAcesso> _roleManager;
        private readonly NivelAcessoRepositorio _nivelAcessoRepositorio;
        private readonly UsuarioNivelAcessoRepositorio _usuarioNivelAcessoRepositorio;

        private readonly AssociarUsuarioNegocio _associarUsuarioNegocio;

        private readonly ApplicationDBContext _contexto;

        public AssociarUsuarioController(UserManager<Usuario> gerenciadorUsuarios, SignInManager<Usuario> gerenciadorLogin, RoleManager<NivelAcesso> roleManager, ApplicationDBContext contexto, NivelAcessoRepositorio nivelAcessoRepositorio, UsuarioNivelAcessoRepositorio usuarioNivelAcessoRepositorio, AssociarUsuarioNegocio associarUsuarioNegocio)
        {
            _gerenciadorUsuarios = gerenciadorUsuarios;
            _gerenciadorLogin = gerenciadorLogin;
            _roleManager = roleManager;
            _contexto = contexto;
            _nivelAcessoRepositorio = nivelAcessoRepositorio;
            _usuarioNivelAcessoRepositorio = usuarioNivelAcessoRepositorio;
            _associarUsuarioNegocio = associarUsuarioNegocio;

        }

        [Authorize(Roles = "Administrador")]
        public IActionResult ListarAssociarUsuario()
        {
            if (!string.IsNullOrEmpty((string)TempData["Sucesso"]))
                ViewBag.Sucesso = (string)TempData["Sucesso"];

            if (!string.IsNullOrEmpty((string)TempData["Aviso"]))
                ViewBag.Aviso = (string)TempData["Aviso"];

            if (!string.IsNullOrEmpty((string)TempData["Erro"]))
                ViewBag.Erro = (string)TempData["Erro"];
                        
            try
            {
                var retornoUsuarioNivelAcesso = _associarUsuarioNegocio.ListarUsuariosComOuSemNivelAcesso();

                if (retornoUsuarioNivelAcesso.Tipo == Enumerador.Tipo.Erro)
                {
                    ViewBag.Erro = retornoUsuarioNivelAcesso.Mensagem;
                    return View(new List<UsuarioNivelAcessoViewModel>());
                }
                else if (retornoUsuarioNivelAcesso.Tipo == Enumerador.Tipo.Aviso)
                {
                    ViewBag.Aviso = retornoUsuarioNivelAcesso.Mensagem;
                    return View(new List<UsuarioNivelAcessoViewModel>());
                }
                else
                {
                    var listaView = new List<UsuarioNivelAcessoViewModel>();

                    var ListaDTO = (List<UsuarioNivelAcessoDTO>)retornoUsuarioNivelAcesso.Objeto;

                    foreach (var item in ListaDTO)
                    {
                        listaView.Add(
                        new UsuarioNivelAcessoViewModel() { UserId = item.UserId, RoleId = item.RoleId, NomeNivelAcesso = item.NomeNivelAcesso, NomeUsuario = item.NomeUsuario });
                    }
                    return View(listaView);
                }
            }
            catch (Exception)
            {
                ViewBag.Erro = "Erro: um erro ocorreu ao tentar carregar a lista de usuário(s) com seu nível de acesso, entre em contato com o Administrador";
                return View(new List<UsuarioNivelAcessoViewModel>());
            }

        }

        [Authorize(Roles = "Administrador")]
        public ActionResult PesquisarLogin(string login)
        {
            login = login == null ? login = "" : login;

            try
            {
                var retornoUsuarioNivelAcesso = _associarUsuarioNegocio.ListarUsuariosComOuSemNivelAcesso();

                if (retornoUsuarioNivelAcesso.Tipo == Enumerador.Tipo.Erro)
                {
                    ViewBag.Erro = retornoUsuarioNivelAcesso.Mensagem;
                    return View(nameof(ListarAssociarUsuario), new List<UsuarioNivelAcessoViewModel>());
                }
                else if (retornoUsuarioNivelAcesso.Tipo == Enumerador.Tipo.Aviso)
                {
                    ViewBag.Aviso = retornoUsuarioNivelAcesso.Mensagem;
                    return View(nameof(ListarAssociarUsuario), new List<UsuarioNivelAcessoViewModel>());
                }
                else
                {
                    var listaView = new List<UsuarioNivelAcessoViewModel>();

                    var ListaDTO = (List<UsuarioNivelAcessoDTO>)retornoUsuarioNivelAcesso.Objeto;

                    foreach (var item in ListaDTO.Where(x => x.NomeUsuario.Contains(login)))
                    {
                        listaView.Add(
                        new UsuarioNivelAcessoViewModel() { UserId = item.UserId, RoleId = item.RoleId, NomeNivelAcesso = item.NomeNivelAcesso, NomeUsuario = item.NomeUsuario });
                    }
                    return View(nameof(ListarAssociarUsuario), listaView);
                }
            }
            catch (Exception)
            {
                ViewBag.Erro = "Erro: um erro ocorreu ao tentar carregar a lista de usuário(s) com seu nível de acesso, entre em contato com o Administrador";
                return View(nameof(ListarAssociarUsuario), new List<UsuarioNivelAcessoViewModel>());
            }

        }

        [Authorize(Roles = "Administrador")]
        public ActionResult EditarAssociarUsuario(string id)
        {
            try
            {                
                var retornoUsuarioNivelAcesso = _associarUsuarioNegocio.ListarUsuariosComOuSemNivelAcesso();

                if (retornoUsuarioNivelAcesso.Tipo == Enumerador.Tipo.Erro)
                {

                    ViewBag.Erro = retornoUsuarioNivelAcesso.Mensagem;
                    return View(nameof(ListarAssociarUsuario), new List<UsuarioNivelAcessoViewModel>());
                }
                else if (retornoUsuarioNivelAcesso.Tipo == Enumerador.Tipo.Aviso)
                {
                    ViewBag.Aviso = retornoUsuarioNivelAcesso.Mensagem;
                    return View(nameof(ListarAssociarUsuario), new List<UsuarioNivelAcessoViewModel>());
                }
                else
                {
                    var usuarioSelecionado = ((List<UsuarioNivelAcessoDTO>)retornoUsuarioNivelAcesso.Objeto).Where(x => x.UserId == id).FirstOrDefault();

                    if (usuarioSelecionado == null)
                    {
                        ViewBag.Erro = "Erro: um erro ocorreu ao tentar carregar o usuário com seu nível de acesso para edição, entre em contato com o Administrador";
                        return View(nameof(ListarAssociarUsuario), new List<UsuarioNivelAcessoViewModel>());
                    }

                    var listaNiveisAcesso = _associarUsuarioNegocio.ListarNiveisAcesso();

                    if (listaNiveisAcesso.Tipo == Enumerador.Tipo.Erro)
                    {

                        ViewBag.Erro = listaNiveisAcesso.Mensagem;
                        return View(nameof(ListarAssociarUsuario), new List<UsuarioNivelAcessoViewModel>());
                    }
                    else if (listaNiveisAcesso.Tipo == Enumerador.Tipo.Aviso)
                    {
                        ViewBag.Aviso = listaNiveisAcesso.Mensagem;
                        return View(nameof(ListarAssociarUsuario), new List<UsuarioNivelAcessoViewModel>());
                    }
                    else
                    {
                        ViewBag.NivelAcesso = new SelectList(listaNiveisAcesso.Objeto, "Id", "Name", string.IsNullOrEmpty(usuarioSelecionado.NomeNivelAcesso) ? "" : usuarioSelecionado.RoleId);

                        var usuarioNivelAcessoViewModel = new UsuarioNivelAcessoViewModel() { UserId = usuarioSelecionado.UserId, RoleId = usuarioSelecionado.RoleId, NomeNivelAcesso = usuarioSelecionado.NomeNivelAcesso, NomeUsuario = usuarioSelecionado.NomeUsuario };

                        return View(usuarioNivelAcessoViewModel);
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.Erro = "Erro: um erro ocorreu ao tentar salvar o usuário com seu nível de acesso, entre em contato com o Administrador";
                return View(nameof(ListarAssociarUsuario), new List<UsuarioNivelAcessoViewModel>());
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult EditarAssociarUsuario(UsuarioNivelAcessoViewModel usuarioNivelAcessoViewModel)
        {
            try
            {
                var retornoUsuarioNivelAcesso = _associarUsuarioNegocio.ListarUsuariosComOuSemNivelAcesso();

                if (retornoUsuarioNivelAcesso.Tipo == Enumerador.Tipo.Erro)
                {

                    ViewBag.Erro = retornoUsuarioNivelAcesso.Mensagem;
                    return View(nameof(ListarAssociarUsuario), new List<UsuarioNivelAcessoViewModel>());
                }
                else if (retornoUsuarioNivelAcesso.Tipo == Enumerador.Tipo.Aviso)
                {
                    ViewBag.Aviso = retornoUsuarioNivelAcesso.Mensagem;
                    return View(nameof(ListarAssociarUsuario), new List<UsuarioNivelAcessoViewModel>());
                }
                else
                {

                    var usuarioSelecionado = ((List<UsuarioNivelAcessoDTO>)retornoUsuarioNivelAcesso.Objeto).Where(x => x.UserId == usuarioNivelAcessoViewModel.UserId).FirstOrDefault();

                    if (usuarioSelecionado == null)
                    {
                        ViewBag.Erro = "Erro: um erro ocorreu ao tentar carregar o usuário com seu nível de acesso para edição, entre em contato com o Administrador";
                        return View(nameof(ListarAssociarUsuario), new List<UsuarioNivelAcessoViewModel>());
                    }

                    var listaNiveisAcesso = _associarUsuarioNegocio.ListarNiveisAcesso();

                    if (listaNiveisAcesso.Tipo == Enumerador.Tipo.Erro)
                    {

                        ViewBag.Erro = listaNiveisAcesso.Mensagem;
                        return View(nameof(ListarAssociarUsuario), new List<UsuarioNivelAcessoViewModel>());
                    }
                    else if (listaNiveisAcesso.Tipo == Enumerador.Tipo.Aviso)
                    {
                        ViewBag.Aviso = listaNiveisAcesso.Mensagem;
                        return View(nameof(ListarAssociarUsuario), new List<UsuarioNivelAcessoViewModel>());
                    }
                    else
                    {

                        ViewBag.NivelAcesso = new SelectList(listaNiveisAcesso.Objeto, "Id", "Name", string.IsNullOrEmpty(usuarioSelecionado.NomeNivelAcesso) ? "" : usuarioSelecionado.RoleId);

                        if (ModelState.IsValid)
                        {
                            var retornoEditarNivelAcesso = _associarUsuarioNegocio.EditarNivelAcessoDoUsuario(usuarioNivelAcessoViewModel.UserId, usuarioNivelAcessoViewModel.RoleId);

                            if (retornoEditarNivelAcesso.Tipo == Enumerador.Tipo.Erro)
                            {

                                ViewBag.Erro = retornoEditarNivelAcesso.Mensagem;
                                return View(usuarioNivelAcessoViewModel);
                            }
                            else if (retornoEditarNivelAcesso.Tipo == Enumerador.Tipo.Aviso)
                            {
                                ViewBag.Aviso = retornoEditarNivelAcesso.Mensagem;
                                return View(usuarioNivelAcessoViewModel);
                            }
                            else
                            {
                                TempData["Sucesso"] = retornoEditarNivelAcesso.Mensagem;

                                return RedirectToAction(nameof(ListarAssociarUsuario));
                            }
                        }
                        return View(usuarioNivelAcessoViewModel);
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.Erro = "Erro: um erro ocorreu no sistema, entre em contato com o Administrador";
                return View(usuarioNivelAcessoViewModel);
            }
        }
                

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarAssociarUsuario(string id)
        {

            try
            {
                var retornoUsuarioNivelAcesso = _associarUsuarioNegocio.ExcluirNivelAcessoDoUsuario(id);

                if (retornoUsuarioNivelAcesso.Tipo == Enumerador.Tipo.Erro)
                {

                    TempData["Erro"] = retornoUsuarioNivelAcesso.Mensagem;
                    return RedirectToAction(nameof(ListarAssociarUsuario));
                }
                else if (retornoUsuarioNivelAcesso.Tipo == Enumerador.Tipo.Aviso)
                {
                    TempData["Aviso"] = retornoUsuarioNivelAcesso.Mensagem;
                    return RedirectToAction(nameof(ListarAssociarUsuario));
                }
                else
                {
                    TempData["Sucesso"] = retornoUsuarioNivelAcesso.Mensagem;
                    return RedirectToAction(nameof(ListarAssociarUsuario));
                }
            }
            catch (Exception)
            {
                ViewBag.Erro = "Erro: um erro ocorreu no sistema, entre em contato com o Administrador";
                return RedirectToAction(nameof(ListarAssociarUsuario));
            }
            
        }

    }
}