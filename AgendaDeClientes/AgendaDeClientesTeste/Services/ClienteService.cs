using AgendaDeClientes.Exceptions;
using AgendaDeClientes.Models;
using AgendaDeClientes.Repository.Implementation;
using AgendaDeClientes.Repository.Interface;
using AgendaDeClientes.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Services
{
    public class ClienteService
    {
        public IClienteInterface _clienteInterface;
        public ManipulacaoDeArquivo _manipulacaoDeArquivo;

        public ClienteService(IClienteInterface clienteImplementation, ManipulacaoDeArquivo manipulacaoDeArquivo)
        {
            _clienteInterface = clienteImplementation;
            _manipulacaoDeArquivo = manipulacaoDeArquivo;
        }

        public void Adicionar(Cliente cliente)
        {
            if (cliente == null) throw new ClienteNullException("Cliente não pode ser nulo.");
            if (cliente.Endereco == null) throw new EnderecoNullException("Endereco não pode ser nulo.");
            if (cliente.Foto == null) throw new FotoNullException("Foto não pode ser nula.");
            if (cliente.Telefone == null) throw new TelefoneNullException("Telefone não pode ser nulo.");

            //deve pesquisar se o telefone já existe
            if (_clienteInterface.PesquisarPorTelefone(cliente).ToList().Where(c => c.Ativo == true).Count() > 0)
                throw new TelefoneJaExisteException("Atenção: Não é possível incluir novo cliente devido ao Telefone digitado já " +
                    "estar gravado em outro cliente (ativo). *** Caso esteja certo que esse numero deve ser salvo para" +
                    "esse novo cliente, então mude o numero do 'cliente antigo' ou inative o 'cliente antigo'.");

            if (cliente.DataNascimento.Date >= DateTime.Now.Date) throw new DataNascimentoMaiorOuIgualException("Data de Nascimento não pode ser maior que a data de hoje.");

            /*# deve implementar função que salva fisicamente imagem no diretório do projeto,
            * pegar o endereço onde a imagem foi salvar e salvar esse endereço em forma de
            * string em cliente.URL.
            */
            cliente.URL = _manipulacaoDeArquivo.SalvarArquivo(cliente.Foto);
            if (string.IsNullOrEmpty(cliente.URL)) throw new FuncSalvarArquivoNullException("Atenção: Não foi possível incluir novo cliente devido a um problema no momento de salvar a imagem.");

            try
            {
                _clienteInterface.Adicionar(cliente);
            }
            catch (Exception ex)
            {
                bool urlExcluida = false;
                //excluir o arquivo caso de algum erro no momento de salvar o cliente
                if (!string.IsNullOrEmpty(cliente.URL))
                    urlExcluida = _manipulacaoDeArquivo.ExcluirArquivo(cliente.URL);

                if (!urlExcluida) throw new FuncAdicionarExcluirArquivoException("Atenção: Não foi possível salvar novo cliente, entre em contato com o Administrador. /n #Erro Interno - Erro ao tentar excluir url do cliente. ");

                throw ex;
            }
        }

        public List<Cliente> Pesquisar(Cliente cliente)
        {
            return _clienteInterface.Pesquisar(cliente).ToList();
        }

        public Cliente Pesquisar(int id)
        {
            if (id == 0) throw new ClienteIDNaoPodeSerZeroException("O id não pode ser zero.");
            var cliente = _clienteInterface.Pesquisar(id);
            if (cliente == null) throw new ClienteNaoExisteException("Não existe cliente com o id pesquisado.");
            return cliente;
        }

        public void Inativar(int id)
        {
            if (id == 0) throw new ClienteIDNaoPodeSerZeroException("O id não pode ser zero.");

            var cliente = _clienteInterface.Pesquisar(id);
            if (cliente == null) throw new ClienteNaoExisteException("Não existe cliente com o id pesquisado.");

            _clienteInterface.Inativar(cliente);
        }

        public void Ativar(int id)
        {
            if (id == 0) throw new ClienteIDNaoPodeSerZeroException("O id não pode ser zero");
            var cliente = _clienteInterface.Pesquisar(id);
            if (cliente == null) throw new ClienteNaoExisteException("Não existe cliente com o id pesquisado.");
            //lista Cliente com mesmo telefone
            var listaMesmoTelefoneCliente = _clienteInterface.PesquisarPorTelefone(cliente);
            //deve pesquisar se o telefone já existe para outros clientes ativos
            if (listaMesmoTelefoneCliente.Where(c => c.Ativo == true).Count() > 0) throw new ClienteAtivoComMesmoTelefoneException("Não é possível ativar " +
                "cliente atual devido a outro cliente ativo já ter o mesmo numero de telefone.");

            _clienteInterface.Ativar(cliente);
        }

        public void Editar(Cliente cliente)
        {
            if (cliente == null) throw new ClienteNullException("Cliente não pode ser nulo.");
            if (cliente.Endereco == null) throw new EnderecoNullException("Endereco não pode ser nulo.");
            if (cliente.ClienteId == 0) throw new ClienteIDNaoPodeSerZeroException("O id do cliente não pode ser zero.");
            if (cliente.EnderecoId == 0) throw new EnderecoIdNaoPodeSerZeroException("O id do endereco não pode ser zero.");
            if (cliente.Foto == null) throw new FotoNullException("Foto não pode ser nula.");

            var lista = _clienteInterface.PesquisarPorTelefone(cliente)
                .Where(c => c.Ativo == true && cliente.ClienteId != c.ClienteId);
            if (lista.Count() > 0) throw new TelefoneJaExisteException("Atenção: Não é possível modificar cliente " +
                "devido ao Telefone digitado já estar gravado em outro cliente (ativo). *** Caso " +
                "esteja certo que esse numero deve ser salvo para esse cliente, então mude o numero" +
                " do 'cliente antigo' ou inative o 'cliente antigo'.");

            if (cliente.DataNascimento.Date >= DateTime.Now.Date) throw new DataNascimentoMaiorOuIgualException("Data de Nascimento não pode ser maior que a data de hoje.");
            
            var urlAntiga = _clienteInterface.BuscarURL(cliente.ClienteId);

            cliente.URL = _manipulacaoDeArquivo.SalvarArquivo(cliente.Foto);
            if (string.IsNullOrEmpty(cliente.URL)) throw new FuncSalvarArquivoNullException("Atenção: Não foi possível incluir novo cliente devido a um problema no momento de salvar a imagem.");

            try
            {
                _clienteInterface.Editar(cliente);
                if (!string.IsNullOrEmpty(urlAntiga))
                    _manipulacaoDeArquivo.ExcluirArquivo(urlAntiga);
            }
            catch(Exception ex)
            {
                bool urlExcluida = false;
                //excluir o arquivo caso de algum erro no momento de salvar o cliente
                if (!string.IsNullOrEmpty(cliente.URL))
                    urlExcluida = _manipulacaoDeArquivo.ExcluirArquivo(cliente.URL);

                if (!urlExcluida) throw new FuncAdicionarExcluirArquivoException("Atenção: Não foi possível salvar novo cliente, entre em contato com o Administrador. /n #Erro Interno - Erro ao tentar excluir url do cliente. ");

                throw ex;
            }
        }
    }
}
