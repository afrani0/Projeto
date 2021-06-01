using ListaDeContatos.Models;
using ListaDeContatos.Repositorio.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Repositorio.Implementacao
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        ApplicationDBContext _context;

        public ContatoRepositorio(ApplicationDBContext context)
        {
            _context = context;
        }

        public virtual List<Contato> ListarContatos()
        {
            return _context.Set<Contato>().AsNoTracking().ToList();
        }

        public virtual void AdicionarContato(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
        }

        public virtual Contato BuscarContatoPorId(int id)
        {
            return _context.Contatos.AsNoTracking().Where(c => c.ContatoId == id).FirstOrDefault();
        }

        public virtual void EditarContato(Contato contato)
        {
            _context.Update(contato);
            _context.SaveChanges();
        }

        public virtual void ExcluirContato(Contato contato)
        {
            _context.Remove(contato);
            _context.SaveChanges();
        }
    }
}
