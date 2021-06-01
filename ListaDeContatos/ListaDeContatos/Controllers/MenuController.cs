using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ListaDeContatos.Models;
using Newtonsoft.Json;
using ListaDeContatos.Dominio;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ListaDeContatos.ViewModels;
using ListaDeContatos.Repositorio.Implementacao;

namespace ListaDeContatos.Controllers
{
    //[Authorize]
    public class MenuController : Controller
    {

        [HttpGet]
        public IActionResult AcessoNegado()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
