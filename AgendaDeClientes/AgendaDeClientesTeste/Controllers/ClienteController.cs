using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using AgendaDeClientes.Exceptions;
using AgendaDeClientes.Models;
using AgendaDeClientes.Services;
using AgendaDeClientes.Util;
using AgendaDeClientes.ViewModel.Cliente;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AgendaDeClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        public ClienteService _clienteService { get; set; }

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService; 
        }

        // GET: api/Cliente
        /// <summary>
        /// Pesquisa uma lista de Cliente(s) com seu respectivo Endereço, conforme valor do parâmetro 'nome'.
        /// </summary> O
        [HttpGet("PesquisarClientes")]
        public IActionResult PesquisarClientes(string nome)
        {
            try
            {
                var entidade = _clienteService.Pesquisar(new Cliente() { Nome = nome });

                return Ok(new
                {
                    Retorno = entidade,
                    Erro = false,
                    MensagemErro = ""
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = "Atenção: Ocorreu um erro ao pesquisar o Cliente. Entre em contato com o Administrador."
                });
            }
        }

        // GET: api/Cliente/5
        /// <summary>
        /// Pesquisa um Cliente com seu respectivo Endereço, conforme valor do parâmetro 'id'.
        /// </summary>
        [HttpGet("VisualizarCliente/{id}", Name = "Get")]
        public IActionResult PesquisarCliente(int id)
        {
            try
            {
                var entidade = _clienteService.Pesquisar(id);
                return Ok(new
                {
                    Retorno = entidade,
                    Erro = false,
                    MensagemErro = ""
                });
            }
            catch (ClienteIDNaoPodeSerZeroException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (ClienteNaoExisteException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = "Atenção: Ocorreu um erro ao pesquisar o Cliente. Entre em contato com o Administrador."
                });
            }
        }
        // Put: api/Cliente
        /// <summary>
        /// Inativar Cliente já ativo, conforme valor do parâmetro 'id'.
        /// </summary>        
        [HttpPut("InativarCliente/{id}")]
        public IActionResult InativarCliente(int id)
        {
            try
            {
                _clienteService.Inativar(id);
                return Ok(new
                {
                    Retorno = "",
                    Erro = false,
                    MensagemErro = ""
                });
            }
            catch (ClienteIDNaoPodeSerZeroException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (ClienteNaoExisteException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (Exception ex)
            {
                
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = "Atenção: Ocorreu um erro ao tentar inativar o Cliente. Entre em contato com o Administrador."
                });
            }
        }
        // Put: api/Cliente
        /// <summary>
        /// Ativar Cliente inativo, que não tenha um telefone igual de outro cliente já ativo, , conforme valor do parâmetro 'id'.
        /// </summary>        
        [HttpPut("AtivarCliente/{id}")]
        public IActionResult AnativarCliente(int id)
        {
            try
            {
                _clienteService.Ativar(id);
                return Ok(new
                {
                    Retorno = "",
                    Erro = false,
                    MensagemErro = ""
                });
            }
            catch (ClienteIDNaoPodeSerZeroException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (ClienteNaoExisteException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (ClienteAtivoComMesmoTelefoneException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = "Atenção: Ocorreu um erro ao tentar ativar o Cliente. Entre em contato com o Administrador."
                });
            }
        }

        // POST: api/Cliente
        /// <summary>
        /// Salvar novo Cliente com seu Endereço, conforme valor do objeto Cliente.
        /// </summary>        
        [HttpPost("AdicionarCliente")]
        public IActionResult AdicionarCliente([FromQuery] AdicionarClienteViewModel cliente)
        {
            try
            {
                Cliente clienteModel = new Cliente()
                {
                    Nome = cliente.Nome,
                    Telefone = cliente.Telefone,
                    TelefoneRecado = cliente.TelefoneRecado,
                    DataNascimento = cliente.DataNascimento,
                    Sexo = cliente.Sexo,
                    Foto = cliente.Foto,
                };

                clienteModel.Endereco = new Endereco()
                {
                    Cidade = cliente.Cidade,
                    Bairro = cliente.Bairro,
                    Rua = cliente.Rua,
                    Numero = cliente.Numero,
                    Complemento = cliente.Complemento
                };

                _clienteService.Adicionar(clienteModel);
                return Ok(new
                {
                    Retorno = "",
                    Erro = false,
                    MensagemErro = ""
                });
            }
            catch (ClienteNullException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (EnderecoNullException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (FotoNullException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (TelefoneNullException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (TelefoneJaExisteException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (DataNascimentoMaiorOuIgualException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (FuncSalvarArquivoNullException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (FuncAdicionarExcluirArquivoException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = "Atenção: Não foi possível salvar novo cliente, entre em contado com o Administrador"
                });
            }
        }
        // POST: api/Cliente
        /// <summary>
        /// Editar Cliente existente com seu Endereço, conforme valor do parâmetro 'id'.
        /// </summary>        
        // PUT: api/Cliente/5
        [HttpPut("EditarCliente/{id}")]
        public IActionResult EditarCliente(int id, [FromQuery] EditarClienteViewModel clienteModel)
        {
            try
            {
                if (id != clienteModel.ClienteId)
                {
                    return NotFound(
                    new
                    {
                        Retorno = "",
                        Erro = true,
                        MensagemErro = "Id do cliente não é o mesmo que está sendo editado"
                    });
                }

                var cliente = new Cliente()
                {
                    ClienteId = clienteModel.ClienteId,
                    Ativo = clienteModel.Ativo,
                    DataNascimento = clienteModel.DataNascimento,
                    EnderecoId = clienteModel.EnderecoId,
                    Foto = clienteModel.Foto,
                    Nome = clienteModel.Nome,
                    Sexo = clienteModel.Sexo,
                    Telefone = clienteModel.Telefone,
                    TelefoneRecado = clienteModel.TelefoneRecado,

                    Endereco = new Endereco()
                    {
                        EnderecoId = clienteModel.EnderecoId,
                        Bairro = clienteModel.Bairro,
                        Cidade = clienteModel.Cidade,
                        Complemento = clienteModel.Complemento,
                        Numero = clienteModel.Numero,
                        Rua = clienteModel.Rua
                    }
                };
                _clienteService.Editar(cliente);

                return Ok(new
                {
                    Retorno = "",
                    Erro = false,
                    MensagemErro = ""
                });
            }
            catch (ClienteNullException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (EnderecoNullException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (ClienteIDNaoPodeSerZeroException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (EnderecoIdNaoPodeSerZeroException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (FotoNullException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (TelefoneJaExisteException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (DataNascimentoMaiorOuIgualException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (FuncSalvarArquivoNullException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (FuncAdicionarExcluirArquivoException ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Retorno = "",
                    Erro = true,
                    MensagemErro = "Atenção: Ocorreu um erro ao tentar editar o Cliente. Entre em contato com o Administrador."
                });
            }
        }
    }
}
