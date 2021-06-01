using ListaDeContatos.Models;
using ListaDeContatos.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Negocio.Interface
{
    public interface IContatoNegocio
    {
        RespostaNegocio CriarContato(Contato contato);
        RespostaNegocio EditarContato(Contato contato);
        RespostaNegocio BuscarContato(string id);
        RespostaNegocio DeletarContato(int id);
        RespostaNegocio ListarContatos();
    }
}
