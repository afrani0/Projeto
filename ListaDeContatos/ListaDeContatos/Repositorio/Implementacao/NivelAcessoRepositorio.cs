using ListaDeContatos.Models;
using ListaDeContatos.Repositorio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Repositorio.Implementacao
{
    public class NivelAcessoRepositorio : INivelAcessoRepositorio
    {
        private ApplicationDBContext _applicationDBContext;

        public NivelAcessoRepositorio(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public virtual void CriarNiveisAcesso()
        {
            if (_applicationDBContext.NiveisAcessos.Count() == 0)//níveis de acesso devem ser incluídos apenas uma única vez no sistema
            {
                _applicationDBContext.NiveisAcessos.Add(new NivelAcesso {NormalizedName = "ADMINISTRADOR",  Name = "Administrador", Descricao = "Tem acesso total de todas as funcionalidades do sistema, inclusive funcionalidades de gerenciamento de usuários, senhas, acessos" });
                _applicationDBContext.NiveisAcessos.Add(new NivelAcesso { NormalizedName = "COMPLETO", Name = "Completo", Descricao = "Tem acesso total de todas as funcionalidades do sistema, como criar, editar, excluir, ver detalhes e listar" });
                _applicationDBContext.NiveisAcessos.Add(new NivelAcesso { NormalizedName = "BASICO", Name = "Basico", Descricao = "Tem acesso limitado de todas as funcionalidades do sistema, pode só ver a lista e detalhes de uma funcionalidade" });

                _applicationDBContext.SaveChanges();

            }

        }

        public virtual List<NivelAcesso> ListarNivelAcesso()
        {
            return _applicationDBContext.NiveisAcessos.ToList();
        }

        public virtual List<NivelAcesso> ListarUsuariosComNivelAcesso()
        {
            var lista = _applicationDBContext.Usuarios.Join(_applicationDBContext.UserLogins, u => u.Id, ul => ul.UserId, (u, ul) => new { u, ul });
            return null;
        }
    }
}
