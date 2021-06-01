using AgendaDeClientes.Models;
using AgendaDeClientes.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Repository.Implementation
{
    public class ClienteImplementation : IClienteInterface
    {
        public AplicationContext _aplicationContext;

        public ClienteImplementation(AplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }

        public virtual void Adicionar(Cliente cliente)
        {
            cliente.Atualizacao = DateTime.Now;
            cliente.Ativo = true;
            _aplicationContext.Add<Cliente>(cliente);
            _aplicationContext.SaveChanges();
        }

        public virtual IQueryable<Cliente> Pesquisar(Cliente cliente)
        {
            cliente.Nome = cliente.Nome == null ? "" : cliente.Nome;
            return _aplicationContext.Set<Cliente>()
                .Where(c => c.Nome.Contains(cliente.Nome));
        }

        public virtual IQueryable<Cliente> PesquisarPorTelefone(Cliente cliente)
        {
            cliente.Telefone = cliente.Telefone == null ? "" : cliente.Telefone;

            return _aplicationContext.Set<Cliente>()
                .Where(c => c.Telefone.Contains(cliente.Telefone));
        }

        public virtual Cliente Pesquisar(int id)
        {
            return _aplicationContext.Find<Cliente>(id);
        }

        public virtual void Inativar(Cliente cliente)
        {
            cliente.Atualizacao = DateTime.Now;
            cliente.Ativo = false;
            _aplicationContext.Update(cliente);
            _aplicationContext.SaveChanges();
        }

        public virtual void Ativar(Cliente cliente)
        {
            cliente.Atualizacao = DateTime.Now;
            cliente.Ativo = true;
            _aplicationContext.Update(cliente);
            _aplicationContext.SaveChanges();
        }

        public virtual void Editar(Cliente cliente)
        {
            cliente.Atualizacao = DateTime.Now;
            _aplicationContext.Update(cliente);
            _aplicationContext.SaveChanges();
        }

        public virtual string BuscarURL(int clienteId)
        {
            return _aplicationContext.Set<Cliente>().Where(c => c.ClienteId == clienteId).Select(x => x.URL).FirstOrDefault();
        }
    }
}
