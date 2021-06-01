using ListaDeContatos.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Negocio.Interface
{
    public interface IAssociarUsuarioNegocio
    {
        RespostaNegocio ListarUsuariosComOuSemNivelAcesso();
        RespostaNegocio EditarNivelAcessoDoUsuario(string usuarioId, string NiveAcessoId);
        RespostaNegocio ExcluirNivelAcessoDoUsuario(string usuarioId);
        RespostaNegocio ListarNiveisAcesso();
    }
}
