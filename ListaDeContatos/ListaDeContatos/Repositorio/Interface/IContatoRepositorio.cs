using ListaDeContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Repositorio.Interface
{
    public interface IContatoRepositorio
    {
        List<Contato> ListarContatos();
        void AdicionarContato(Contato contato);
        Contato BuscarContatoPorId(int id);
        void EditarContato(Contato contato);
        void ExcluirContato(Contato contato);
    }
}
