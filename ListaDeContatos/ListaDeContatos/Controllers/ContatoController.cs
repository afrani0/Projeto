using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ListaDeContatos.Dominio;
using ListaDeContatos.Models;
using ListaDeContatos.Negocio.Implementacao;
using ListaDeContatos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ListaDeContatos.Controllers
{
    [Authorize]
    public class ContatoController : Controller
    {
        private readonly ContatoNegocio _contatoNegocio;
        private readonly IMapper _mapper;

        public ContatoController(ContatoNegocio contatoNegocio, IMapper mapper)
        {
            _contatoNegocio = contatoNegocio;
            _mapper = mapper;
        }

        [Authorize(Roles = "Administrador,Completo,Basico")]
        public ActionResult ListarContatos()
        {
            try
            {               
                if (!string.IsNullOrEmpty((string)TempData["Sucesso"]))
                    ViewBag.Sucesso = (string)TempData["Sucesso"];

                if (!string.IsNullOrEmpty((string)TempData["Aviso"]))
                    ViewBag.Aviso = (string)TempData["Aviso"];

                if (!string.IsNullOrEmpty((string)TempData["Erro"]))
                    ViewBag.Erro = (string)TempData["Erro"];

                var retornoContato = _contatoNegocio.ListarContatos();

                if (retornoContato.Tipo == Enumerador.Tipo.Erro)
                {
                    ViewBag.Erro = retornoContato.Mensagem;
                    return View(new List<Contato>());
                }

                return View(retornoContato.Objeto);
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Erro: um erro ocorreu ao tentar carregar a lista de contatos, entre em contato com o Administrador";
                return View(new List<Contato>());
            }
        }

        [Authorize(Roles = "Administrador,Completo,Basico")]
        public ActionResult VisualizarContato(int id)
        {
            try
            {
                var retornoContato = _contatoNegocio.BuscarContato(id.ToString());

                if (retornoContato.Tipo == Enumerador.Tipo.Erro)
                {
                    TempData["Erro"] = retornoContato.Mensagem;
                    return RedirectToAction("ListarContatos");
                }
                if (retornoContato.Tipo == Enumerador.Tipo.Aviso)
                {
                    TempData["Aviso"] = retornoContato.Mensagem;
                    return RedirectToAction("ListarContatos");
                }


                var entidade = _mapper.Map<ContatoViewModel>(retornoContato.Objeto);

                return View(entidade);

            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro: um erro ocorreu ao tentar carregar contato selecionado, entre em contato com o Administrador";
                return RedirectToAction(nameof(ListarContatos));
            }

        }
        
        [Authorize(Roles = "Administrador,Completo")]
        public ActionResult CriarContato()
        {
            try
            {

                AcessoApiExterna.AppCLiente acesso = new AcessoApiExterna.AppCLiente();
                var retorno = acesso.EstadoAsync().Result;

                List<Estado> lista = JsonConvert.DeserializeObject<List<Estado>>(retorno);
                ViewBag.Estado = lista;

                ViewBag.Cidade = JsonConvert.DeserializeObject<List<Cidade>>("[]");
                ViewBag.Bairro = JsonConvert.DeserializeObject<List<CEP>>("[]");

                return View();
            }
            catch (Exception)
            {
                TempData["Erro"] = "Erro: um erro ocorreu ao tentar carregar tela de novo contato, entre em contato com o Administrador";
                return RedirectToAction(nameof(ListarContatos));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Completo")]
        public ActionResult CriarContato(ContatoViewModel contatoViewModel)
        {

            try
            {
                AcessoApiExterna.AppCLiente acesso = new AcessoApiExterna.AppCLiente();
                var retorno = acesso.EstadoAsync().Result;
                //carregando Lista de Estados
                ViewBag.Estado = JsonConvert.DeserializeObject<List<Estado>>(retorno);

                if (contatoViewModel.Estado != null)
                {
                    var listaCidadeJson = acesso.CidadeAsync(contatoViewModel.Estado).Result;

                    ViewBag.Cidade = JsonConvert.DeserializeObject<List<Cidade>>(listaCidadeJson);

                }
                else
                    ViewBag.Cidade = JsonConvert.DeserializeObject<List<Cidade>>("[]");


                ViewBag.Bairro = JsonConvert.DeserializeObject<List<CEP>>("[]");

                if (ModelState.IsValid)
                {

                    var entidade = _mapper.Map<Contato>(contatoViewModel);

                    var retornoNegocio = _contatoNegocio.CriarContato(entidade);

                    if (retornoNegocio.Tipo == Enumerador.Tipo.Sucesso)
                    {
                        TempData["Sucesso"] = retornoNegocio.Mensagem;
                        return RedirectToAction(nameof(ListarContatos));
                    }
                    else if (retornoNegocio.Tipo == Enumerador.Tipo.Aviso)
                    {
                        ViewBag.Aviso = retornoNegocio.Mensagem;
                        return View(contatoViewModel);
                    }
                    else
                    {
                        TempData["Erro"] = retornoNegocio.Mensagem;
                        return RedirectToAction(nameof(ListarContatos));
                    }
                }

                return View(contatoViewModel);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro: um erro ocorreu ao tentar salvar novo contato, entre em contato com o Administrador";
                return RedirectToAction(nameof(ListarContatos));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Completo")]
        public JsonResult CarregarCidade(string estado)
        {
            try
            {

                AcessoApiExterna.AppCLiente acesso = new AcessoApiExterna.AppCLiente();
                var retorno = acesso.CidadeAsync(estado).Result;

                var lista = JsonConvert.DeserializeObject<List<Cidade>>(retorno);

                var json = new JsonResult(lista);

                return json;

            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new { success = false });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Completo")]
        public JsonResult CarregarDadosCEP(string CEP)
        {
            try
            {

                AcessoApiExterna.AppCLiente acesso = new AcessoApiExterna.AppCLiente();
                var retorno = acesso.CEPAsync(CEP).Result;

                var cep = JsonConvert.DeserializeObject<CEP>(retorno);

                var json = new JsonResult(cep);

                return json;
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                
                return Json(new { success = false });
            }
        }

        [Authorize(Roles = "Administrador,Completo")]
        public ActionResult EditarContato(int id)
        {
            try
            {
                var retornoContato = _contatoNegocio.BuscarContato(id.ToString());

                AcessoApiExterna.AppCLiente acesso = new AcessoApiExterna.AppCLiente();
                var retornoEstado = acesso.EstadoAsync().Result;

                List<Estado> listaEstado = JsonConvert.DeserializeObject<List<Estado>>(retornoEstado);                
                ViewBag.Estado = new SelectList(listaEstado, "Sigla", "Nome", ((Contato)retornoContato.Objeto).Estado);

                var retornoCidade = acesso.CidadeAsync(((Contato)retornoContato.Objeto).Estado).Result;

                List<Cidade> listaCidade = JsonConvert.DeserializeObject<List<Cidade>>(retornoCidade);
                ViewBag.Cidade = new SelectList(listaCidade, "Nome", "Nome", ((Contato)retornoContato.Objeto).Cidade);
                
                var entidade = _mapper.Map<ContatoViewModel>(retornoContato.Objeto);

                return View(entidade);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro: um erro ocorreu ao tentar carregar a tela de edição de contato, entre em contato com o Administrador";
                return RedirectToAction("ListarContatos");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Completo")]
        public ActionResult EditarContato(int id, ContatoViewModel contatoViewModel)
        {
            try
            {
                AcessoApiExterna.AppCLiente acesso = new AcessoApiExterna.AppCLiente();
                var retorno = acesso.EstadoAsync().Result;
                //carregando Lista de Estados
                ViewBag.Estado = new SelectList(JsonConvert.DeserializeObject<List<Estado>>(retorno), "Sigla", "Nome", contatoViewModel.Estado);

                if (contatoViewModel.Estado != null)
                {
                    var listaCidadeJson = acesso.CidadeAsync(contatoViewModel.Estado).Result;

                    ViewBag.Cidade = new SelectList(JsonConvert.DeserializeObject<List<Cidade>>(listaCidadeJson), "Nome", "Nome", contatoViewModel.Cidade);

                }
                else
                    ViewBag.Cidade = JsonConvert.DeserializeObject<List<Cidade>>("[]");

                ViewBag.Bairro = JsonConvert.DeserializeObject<List<CEP>>("[]");

                if (ModelState.IsValid)
                {

                    var entidade = _mapper.Map<Contato>(contatoViewModel);

                    var retornoNegocio = _contatoNegocio.EditarContato(entidade);

                    if (retornoNegocio.Tipo == Enumerador.Tipo.Sucesso)
                    {
                        TempData["Sucesso"] = retornoNegocio.Mensagem;
                        return RedirectToAction(nameof(ListarContatos));
                    }
                    else if (retornoNegocio.Tipo == Enumerador.Tipo.Aviso)
                    {
                        ViewBag.Aviso = retornoNegocio.Mensagem;
                        return View(contatoViewModel);
                    }
                    else
                    {
                        TempData["Erro"] = retornoNegocio.Mensagem;
                        return RedirectToAction(nameof(ListarContatos));
                    }
                }

                return View(contatoViewModel);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro: um erro ocorreu ao tentar editar o contato, entre em contato com o Administrador";
                return RedirectToAction(nameof(ListarContatos));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Completo")]
        public ActionResult DeletarContato(int id)
        {
            try
            {
                var contatoNegocio = _contatoNegocio.DeletarContato(id);

                if (contatoNegocio.Tipo == Enumerador.Tipo.Sucesso)
                {
                    TempData["Sucesso"] = contatoNegocio.Mensagem;
                    return RedirectToAction(nameof(ListarContatos));
                }
                else if (contatoNegocio.Tipo == Enumerador.Tipo.Aviso)
                {
                    TempData["Aviso"] = contatoNegocio.Mensagem;
                    return RedirectToAction(nameof(ListarContatos));
                }
                else
                {
                    TempData["Erro"] = contatoNegocio.Mensagem;
                    return RedirectToAction(nameof(ListarContatos));
                }
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro: um erro ocorreu ao tentar excluir o contato, entre em contato com o Administrador";
                return RedirectToAction(nameof(ListarContatos));
            }
        }

    }
}