using ListaDeContatos.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Negocio.Interface
{
    public interface ISenhaNegocio
    {
        Task<RespostaNegocio> NovaSenha(string UsuarioId);
    }
}
