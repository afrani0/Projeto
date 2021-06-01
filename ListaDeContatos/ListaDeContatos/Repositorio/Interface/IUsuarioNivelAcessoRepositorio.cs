using ListaDeContatos.DTO;
using ListaDeContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Repositorio.Interface
{
    public interface IUsuarioNivelAcessoRepositorio
    {
        bool SalvarEditarUsuarioNivelAcesso(UsuarioNivelAcesso usuarioNivelAcesso);
        List<UsuarioNivelAcessoDTO> ListarUsuariosComOuSemNivelAcesso();
        List<UsuarioNivelAcesso> ListarUsuarioNivelAcesso();
        bool ExcluirUsuarioNivelAcesso(UsuarioNivelAcesso UsuarioNivelAcesso);
    }
}
