using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using React_Arquivos_Backend.Business.Implementation;
using React_Arquivos_Backend.Business.Interface;
using React_Arquivos_Backend.ExceptionCustom;
using React_Arquivos_Backend.ManagementAplicationDB;
using React_Arquivos_Backend.Models;
using React_Arquivos_Backend.VO.ArquivoVO;

namespace React_Arquivos_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArquivoController : ControllerBase
    {
        private readonly DBAplication _context;
        private readonly IArquivoBusiness _arquivoBusiness;

        public ArquivoController(DBAplication context, IArquivoBusiness arquivoBusiness)
        {
            _context = context;
            _arquivoBusiness = arquivoBusiness;
        }

        // GET: api/Arquivo


        // GET: api/Arquivo/5
        [Route("/api/arquivo/{id}")]//quando quer pegar somente parametro da 'rota'
        [HttpGet]
        public IActionResult GetArquivo([FromRoute] string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var arquivo = _arquivoBusiness.ListarImagem(string.Empty).Where(x => x.ArquivoId == (Int32.Parse(id))).FirstOrDefault();

                if (arquivo == null)
                {
                    return NotFound("Não foi encontrado nenhum arquivo com esse id");
                }

                return Ok(arquivo);
            }
            catch (ExceptionBusiness e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu um erro ao tentar acessar o arquivo, tente novamente ou informe o administrador.");
            }
        }

        //para usar mesma rota com mesmo nome, do tipo 'Get', foi necessário criar a rota de forma explicita.
        [Route("/api/arquivo")]
        [HttpGet("{nome}")]//quando quer pegar parametro(s) [chave=valor]
        public IActionResult GetArquivoLista([FromQuery] string nome)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var arquivo = _arquivoBusiness.ListarImagem(nome);

                if (arquivo == null)
                {
                    return NotFound("Não foi encontrado nenhum arquivo com esse nome");
                }

                return Ok(arquivo);
            }
            catch (ExceptionBusiness e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu um erro ao tentar salvar o arquivo, tente novamente ou informe o administrador.");
            }
        }

        // PUT: api/Arquivo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArquivo([FromRoute] string id, [FromForm] ArquivoEdicaoVO arquivoVO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (Int32.Parse(id) != arquivoVO.ArquivoId)
                {
                    return BadRequest("Não foi possível atualizar o arquivo, o id da rota é diferente do arquivo que esta sendo atualizado");
                }

                var retorno = _arquivoBusiness.ListarImagem(string.Empty)
                     .Where(x => x.ArquivoId == Int32.Parse(id)).FirstOrDefault();

                if (retorno == null)
                {
                    return NotFound("O arquivo não existe ou foi excluído");
                }

                //passando novos valores
                retorno.Nome = arquivoVO.Nome;
                retorno.Descricao = arquivoVO.Descricao;

                //_context.Entry(arquivo).State = EntityState.Modified;


                await _arquivoBusiness.EditarImagem(retorno, arquivoVO.Imagem);
                //await _context.SaveChangesAsync();


                return NoContent();

            }
            catch (ExceptionBusiness e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu um erro ao tentar atualizar o arquivo, tente novamente ou informe o administrador.");
            }
        }

        // POST: api/Arquivo
        [HttpPost]
        public async Task<IActionResult> PostArquivo([FromForm] ArquivoVO arquivoVO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var arquivo = new Arquivo()
                {
                    Nome = arquivoVO.Nome,
                    Descricao = arquivoVO.Descricao,
                    ArquivoId = 0
                };

                await _arquivoBusiness.CriarImagem(arquivo, arquivoVO.Imagem);

                //_context.Arquivos.Add(arquivo);
                //await _context.SaveChangesAsync();
                //return CreatedAtAction("GetArquivo", new { id = arquivo.CaminhoImg }, arquivo);
                return Created("Arquivo Criado com sucesso", null);
            }
            catch (ExceptionBusiness e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu um erro ao tentar salvar o arquivo, tente novamente ou informe o administrador.");
            }

        }

        // DELETE: api/Arquivo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArquivo([FromRoute] string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || id == "0")
                {
                    return BadRequest("Id não pode zero ou null");
                }

                var arquivo = _arquivoBusiness.ListarImagem(string.Empty)
                    .Where(x => x.ArquivoId == Int32.Parse(id)).FirstOrDefault();
                if (arquivo == null)
                {
                    return NotFound("O arquivo não existe ou já foi excluído");
                }

                await _arquivoBusiness.ExcluirImagem(arquivo);

                //_context.Arquivos.Remove(arquivo);
                //await _context.SaveChangesAsync();

                return Ok("O arquivo foi excluído com sucesso.");
            }
            catch (ExceptionBusiness e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu um erro ao tentar atualizar o arquivo, tente novamente ou informe o administrador.");
            }
        }

        private bool ArquivoExists(string id)
        {
            return _context.Arquivos.Any(e => e.CaminhoImg == id);
        }
    }
}