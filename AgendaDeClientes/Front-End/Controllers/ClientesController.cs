using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Front_End.Models;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Front_End.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ContextoFront _context;
        IHostingEnvironment _env;

        public ClientesController(ContextoFront context, IHostingEnvironment environment)
        {
            _context = context;
            _env = environment;
        }

        // GET: Clientes
        [Route("")]
        public async Task<IActionResult> Index()
        {

            //HttpClient httpClient = new HttpClient();
            var url = "https://localhost:44325/api/Cliente/TesteImagem";
            string filePath = @"D:\Versoes WebForm\Versoes Conectadas com o GIT\Projeto - Angeda Cliente Back-end\AgendaDeClientes\Imagens\spinner.GIF";
            var client = new HttpClient();

            MultipartFormDataContent content = new MultipartFormDataContent();

            HttpResponseMessage response;

            using (var file = System.IO.File.OpenRead(filePath))
            {

                var streamContent = new StreamContent(file);

                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "spinner",
                    FileName = "spinner.GIF"
                };

                var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync());

                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

                // "file" parametro nome (ex:'arquivo') deve ter o mesmo nome(ex:'arquivo') do parametro do 'lado servidor'
                content.Add(fileContent, "arquivos", Path.GetFileName(filePath));

                file.Dispose();

                response = await client.PostAsync(url, content);

            }


            var result = response.Content.ReadAsStringAsync().Result;

            result = JsonConvert.DeserializeObject<string>(result);

            var imagemFile = System.Drawing.Image.FromFile(result);

            var dir = Directory.GetCurrentDirectory().Replace("AgendaDeClientesTeste", "Imagens");

            // Copiar aquivo para destination.

            var roots = _env.WebRootPath + "//" + "spinner.GIF";

            //Salvando caminho em wwwroot
            MemoryStream arquivo = new MemoryStream();

            string root = Directory.GetCurrentDirectory();
            System.IO.File.Copy(result, roots, true);

            ViewBag.ImagemCliente = "spinner.GIF";

            return View();
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Foto")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Foto")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }
    }
}
