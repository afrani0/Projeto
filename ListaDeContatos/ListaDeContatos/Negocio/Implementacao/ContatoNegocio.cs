using ListaDeContatos.Models;
using ListaDeContatos.Negocio.Interface;
using ListaDeContatos.Repositorio.Implementacao;
using ListaDeContatos.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Negocio.Implementacao
{
    public class ContatoNegocio : IContatoNegocio
    {
        ContatoRepositorio _contatoRepositorio;

        public ContatoNegocio(ContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        public RespostaNegocio BuscarContato(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador" };

            if (id == "0")
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Id não pode ser zero, entre em contato com o Administrador" };

            var retornoRepositorio = _contatoRepositorio.BuscarContatoPorId(int.Parse(id));

            if (retornoRepositorio == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: O contato pode ter sido excluído anteriormente ou não existe no sistema" };

            return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Objeto = retornoRepositorio };
        }

        public RespostaNegocio DeletarContato(int id)
        {

            if (id == 0)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: O Id do Contato não pode ser zero, entre em contato com o Administrador" };

            var retornoContato = _contatoRepositorio.BuscarContatoPorId(id);

            if (retornoContato == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: O contato pode ter sido excluído anteriormente ou não existe no sistema" };

            _contatoRepositorio.ExcluirContato(retornoContato);
                       
            return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Mensagem = "Contato deletado com sucesso" };
        }

        public RespostaNegocio EditarContato(Contato contato)
        {
            if (contato == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: O Contato não pode ser nulo, entre em contato com o Administrador" };

            if (contato.ContatoId == 0)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: O Id do Contato não pode ser zero, entre em contato com o Administrador" };

            if (contato.Telefone == null || contato.Telefone.Trim() == "")
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Telefone não pode ser nulo ou vazio, entre em contato com o Administrador" };

            if (contato.DataNascimento.Date > DateTime.Now.Date)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: A 'Data de Nascimento' não pode ser maior que a data de hoje" };

            var contatoExisteNoBanco = _contatoRepositorio.BuscarContatoPorId(contato.ContatoId);
            if (contatoExisteNoBanco == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: O contato não existe no sistema" };

            var telefoneExiste = _contatoRepositorio.ListarContatos().Where(l => l.Telefone == contato.Telefone && l.ContatoId != contato.ContatoId).ToList();

            if (telefoneExiste.Count() >= 1)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: Já existe outro contato com esse numero de telefone , só pode ser mudado para um numero de telefone que ainda não exista no sistema" };

            _contatoRepositorio.EditarContato(contato);

            return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Mensagem = "Contato editado com sucesso" };
        }

        public RespostaNegocio ListarContatos()
        {
            var retornoRepositorio = _contatoRepositorio.ListarContatos();

            if (retornoRepositorio == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar lista de contatos, entre em contato com o Administrador" };

            return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Objeto = retornoRepositorio };
        }

        public RespostaNegocio CriarContato(Contato contato)
        {
            if (contato == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Contato não pode ser nulo, entre em contato com o Administrador" };

            if (contato.Telefone == null || contato.Telefone.Trim() == "")
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Telefone não pode ser nulo ou vazio, entre em contato com o Administrador" };

            var retornoRepositorio = _contatoRepositorio.ListarContatos();

            if (retornoRepositorio == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar lista de contatos, entre em contato com o Administrador" };

            if (retornoRepositorio.Where(c => c.Telefone == contato.Telefone).Count() > 0)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: Já existe 'Telefone' com esse valor, só pode ser inserido 'Telefone' que ainda não exista no sistema" };

            if (contato.DataNascimento.Date > DateTime.Now.Date)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: A 'Data de Nascimento' não pode ser maior que a data de hoje" };

            _contatoRepositorio.AdicionarContato(contato);

            return new RespostaNegocio() { Mensagem = "Contato salvo com sucesso", Tipo = Enumerador.Tipo.Sucesso };
        }
    }
}
