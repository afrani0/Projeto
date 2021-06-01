using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ListaDeContatos.Models;
using ListaDeContatos.Negocio.Implementacao;
using ListaDeContatos.Negocio.Interface;
using ListaDeContatos.Util;
using ListaDeContatos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeContatos.Controllers
{
    //[Authorize]
    public class RegistroController : Controller
    {
        private readonly IRegistroNegocio _registroUsuarios;
        private readonly IMapper _mapper;

        public RegistroController(RegistroNegocio registroUsuarios, IMapper mapper)
        {
            _registroUsuarios = registroUsuarios;
            _mapper = mapper;
        }

        // GET: Registro
        [Authorize(Roles = "Administrador")]
        public ActionResult ListaUsuarios()
        {

            try
            {

                if (!string.IsNullOrEmpty((string)TempData["Sucesso"]))
                    ViewBag.Sucesso = (string)TempData["Sucesso"];

                if (!string.IsNullOrEmpty((string)TempData["Aviso"]))
                    ViewBag.Aviso = (string)TempData["Aviso"];

                if (!string.IsNullOrEmpty((string)TempData["Erro"]))
                    ViewBag.Erro = (string)TempData["Erro"];

                var listaUsuarios = _registroUsuarios.ListarUsuarios();

                if (listaUsuarios.Tipo == Enumerador.Tipo.Erro)
                {
                    ViewBag.Erro = listaUsuarios.Mensagem;
                    listaUsuarios.Objeto = new List<Usuario>();
                }

                var entidade = _mapper.Map<IEnumerable<UsuarioEditarViewModel>>(listaUsuarios.Objeto);

                return View(entidade);

            }
            catch (Exception)
            {
                ViewBag.Erro = "Erro: um erro ocorreu ao tentar carregar a lista de 'Usuários', entre em contato com o Administrador";
                return View(_mapper.Map<IEnumerable<UsuarioEditarViewModel>>(new List<Usuario>()));
            }
        }


        // GET: Registro/Create
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public ActionResult CriarUsuario()
        {
            return View();
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<ActionResult> CriarUsuario(UsuarioViewModel usuarioViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entidade = _mapper.Map<Usuario>(usuarioViewModel);

                    var usuarioCriado = await _registroUsuarios.CriarUsuario(entidade, usuarioViewModel.Senha);

                    if (usuarioCriado.Tipo == Enumerador.Tipo.Sucesso)
                    {
                        TempData["Sucesso"] = usuarioCriado.Mensagem;
                        return RedirectToAction("ListaUsuarios");
                    }
                    else if (usuarioCriado.Tipo == Enumerador.Tipo.Aviso)
                    {
                        ViewBag.Aviso = usuarioCriado.Mensagem;
                        ModelState.AddModelError("NomeUsuario", " '" + usuarioViewModel.NomeUsuario + " ' já existe, deve ser digitado outro Nome Usuário");
                        return View(usuarioViewModel);
                    }
                    else if (usuarioCriado.Tipo == Enumerador.Tipo.Erro)
                    {
                        if (usuarioCriado.Erros != null && 0 < (usuarioCriado.Erros.Where(e => (e.Code == "InvalidUserName") || (e.Code == "DuplicateUserName")
                        || e.Code == "PasswordTooShort" || e.Code == "PasswordRequiresNonAlphanumeric" || e.Code == "PasswordRequiresDigit"
                        || e.Code == "PasswordRequiresUpper" || e.Code == "PasswordRequiresLower").ToList().Count))
                        {

                            var erros = usuarioCriado.Erros;

                            foreach (var erro in erros)
                            {
                                if (erro.Code.Contains("InvalidUserName"))
                                    ModelState.AddModelError("NomeUsuario", "O Nome Usuário é inválido, pode conter somente letras e dígitos válidos.");
                                if (erro.Code.Contains("DuplicateUserName"))
                                    ModelState.AddModelError("NomeUsuario", " '" + usuarioViewModel.NomeUsuario + " ' já existe, deve ser digitado outro Nome Usuário");
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
                            }
                            return View(usuarioViewModel);
                        }
                        else
                        {
                            TempData["Erro"] = usuarioCriado.Mensagem;
                            return RedirectToAction("ListaUsuarios");

                        }

                    }

                    return View(usuarioViewModel);
                }
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro: um erro ocorreu ao tentar criar o usuário, entre em contato com o Administrador";
                return RedirectToAction("ListaUsuarios");
            }



            return View(usuarioViewModel);
        }
        // GET: Registro/Edit/5
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<ActionResult> EditarUsuario(string id)
        {

            try
            {
                var entidade = await _registroUsuarios.BuscarUsuarioPorId(id);


                if (entidade.Tipo == Enumerador.Tipo.Sucesso)
                {
                    var usuario = _mapper.Map<UsuarioEditarViewModel>(entidade.Objeto);

                    return View(usuario);
                }
                else if (entidade.Tipo == Enumerador.Tipo.Aviso)
                {

                    TempData["Aviso"] = entidade.Mensagem;
                    return RedirectToAction("ListaUsuarios");

                }
                else
                {
                    TempData["Erro"] = entidade.Mensagem;
                    return RedirectToAction("ListaUsuarios");
                }

            }
            catch (Exception)
            {

                TempData["Erro"] = "Erro: um erro ocorreu ao tentar carregar os dados do usuário para edição, entre em contato com o Administrador";
                return RedirectToAction("ListaUsuarios");
            }

        }

        //POST: Registro/Edit/5
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarUsuario(string id, UsuarioEditarViewModel usuarioViewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (id != usuarioViewModel.Id)
                    {
                        TempData["Erro"] = "Erro, Id do Usuário não confere, entre em contato com o administrador";

                        return RedirectToAction(nameof(ListaUsuarios));
                    }

                    var retornoUsuario = await _registroUsuarios.BuscarUsuarioPorId(id);

                    if (retornoUsuario.Tipo == Enumerador.Tipo.Erro)
                    {
                        TempData["Erro"] = retornoUsuario.Mensagem = retornoUsuario.Mensagem;
                        return RedirectToAction(nameof(ListaUsuarios));
                    }
                    else if (retornoUsuario.Tipo == Enumerador.Tipo.Aviso)
                    {
                        TempData["Aviso"] = retornoUsuario.Mensagem = retornoUsuario.Mensagem;
                        return RedirectToAction(nameof(ListaUsuarios));
                    }

                    var entidade = _mapper.Map(usuarioViewModel, retornoUsuario.Objeto);
                    
                    RespostaNegocio usuarioEditado = await _registroUsuarios.EditarUsuario(entidade);

                    if (usuarioEditado.Tipo == Enumerador.Tipo.Sucesso)
                    {
                        TempData["Sucesso"] = usuarioEditado.Mensagem;
                        return RedirectToAction(nameof(ListaUsuarios));
                    }
                    else if (usuarioEditado.Tipo == Enumerador.Tipo.Erro)
                    {
                        if (usuarioEditado.Erros != null && 0 < (usuarioEditado.Erros.Where(e => e.Code == "InvalidUserName").ToList().Count))
                        {

                            var erros = usuarioEditado.Erros;

                            foreach (var erro in erros)
                            {
                                if (erro.Code.Contains("InvalidUserName"))
                                    ModelState.AddModelError("NomeUsuario", "O Nome Usuário é inválido, pode conter somente letras e dígitos válidos.");
                            }
                            return View(usuarioViewModel);
                        }
                        else
                        {
                            TempData["Erro"] = usuarioEditado.Mensagem;
                            return RedirectToAction(nameof(ListaUsuarios));
                        }
                    }
                    else if (usuarioEditado.Tipo == Enumerador.Tipo.Aviso)
                    {
                        ViewBag.Aviso = usuarioEditado.Mensagem;                       
                        return View(usuarioViewModel);
                    }
                }
                
                return View(usuarioViewModel);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro: um erro ocorreu ao tentar editar o usuário, entre em contato com o Administrador";
                return RedirectToAction("ListaUsuarios");
            }
        }
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> VisualizarUsuario(string id)
        {
            try
            {

                var entidade = await _registroUsuarios.BuscarUsuarioPorId(id);


                if (entidade.Tipo == Enumerador.Tipo.Sucesso)
                {
                    var usuario = _mapper.Map<UsuarioViewModel>(entidade.Objeto);

                    return View(usuario);
                }
                else if (entidade.Tipo == Enumerador.Tipo.Aviso)
                {
                    TempData["Aviso"] = entidade.Mensagem;
                    return RedirectToAction("ListaUsuarios");
                }
                else
                {
                    TempData["Erro"] = entidade.Mensagem;
                    return RedirectToAction("ListaUsuarios");
                }


            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro: um erro ocorreu ao tentar buscar o usuário, entre em contato com o Administrador";
                return RedirectToAction("ListaUsuarios");
            }


        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarUsuario(string id)
        {
            try
            {
                var exclusaoComSucesso = await _registroUsuarios.DeletarUsuario(new Usuario { Id = id });

                if (exclusaoComSucesso.Tipo == Enumerador.Tipo.Sucesso)
                {
                    TempData["Sucesso"] = exclusaoComSucesso.Mensagem;
                    return RedirectToAction("ListaUsuarios");
                }
                else if (exclusaoComSucesso.Tipo == Enumerador.Tipo.Aviso)
                {
                    TempData["Aviso"] = exclusaoComSucesso.Mensagem;
                    return RedirectToAction("ListaUsuarios");
                }
                else
                {
                    TempData["Erro"] = exclusaoComSucesso.Mensagem;
                    return RedirectToAction("ListaUsuarios");
                }
            }
            catch (Exception)
            {
                TempData["Erro"] = "Erro: um erro ocorreu ao tentar deletar o usuário, entre em contato com o Administrador";
                return RedirectToAction("ListaUsuarios");
            }
        }
    }
}