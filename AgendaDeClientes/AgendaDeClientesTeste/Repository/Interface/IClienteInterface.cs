using AgendaDeClientes.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Repository.Interface
{
    public interface IClienteInterface
    {
        void Adicionar(Cliente cliente);
        IQueryable<Cliente> Pesquisar(Cliente cliente);
        IQueryable<Cliente> PesquisarPorTelefone(Cliente cliente);
        Cliente Pesquisar(int id);
        void Inativar(Cliente cliente);
        void Ativar(Cliente cliente);
        void Editar(Cliente cliente);
        string BuscarURL(int clienteId);
    }
}
