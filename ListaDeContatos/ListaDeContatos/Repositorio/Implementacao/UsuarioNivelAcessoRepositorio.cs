using ListaDeContatos.DTO;
using ListaDeContatos.Models;
using ListaDeContatos.Repositorio.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Repositorio.Implementacao
{
    public class UsuarioNivelAcessoRepositorio : IUsuarioNivelAcessoRepositorio
    {

        private ApplicationDBContext _applicationDBContext;
        private readonly UserManager<Usuario> _gerenciadorUsuarios;

        public UsuarioNivelAcessoRepositorio(ApplicationDBContext applicationDBContext, UserManager<Usuario> gerenciadorUsuarios)
        {
            _applicationDBContext = applicationDBContext;
            _gerenciadorUsuarios = gerenciadorUsuarios;
        }

        public virtual bool SalvarEditarUsuarioNivelAcesso(UsuarioNivelAcesso usuarioNivelAcesso)
        {
            using (var dbContextTransaction = _applicationDBContext.Database.CurrentTransaction ?? _applicationDBContext.Database.BeginTransaction())
            {
                try
                {
                    var usuarioExiste = _applicationDBContext.UsuariosNivelAcessos.Where(u => u.UserId == usuarioNivelAcesso.UserId).FirstOrDefault();

                    if (usuarioExiste == null)
                    {
                        _applicationDBContext.UsuariosNivelAcessos.Add(usuarioNivelAcesso);
                        _applicationDBContext.SaveChanges();

                        dbContextTransaction.Commit();
                        return true;
                    }
                    else
                    {
                        _applicationDBContext.Remove(usuarioExiste);
                        _applicationDBContext.SaveChanges();

                        _applicationDBContext.UsuariosNivelAcessos.Add(usuarioNivelAcesso);
                        _applicationDBContext.SaveChanges();

                        dbContextTransaction.Commit();
                        return true;
                    }
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return false;
                }
            }
        }

        //cria um Usuário padrão com Login 'adm' e senha 'Aaa111*' e atrela ele ao NivelAcesso do tipo 'Administrador', necessário para gerenciar o sistema
        public virtual bool CriarPrimeiroUsuarioComNivelAcessoDoTipoAdministrador()
        {
            using (var dbContextTransaction = _applicationDBContext.Database.BeginTransaction())
            {
                try
                {
                    //criar usuário
                    var usuario = new Usuario()
                    {
                        UserName = "adm",
                        Email = "a@user.com.br",
                        Nome = "Administrador",
                        SobreNome = "do Sistema",
                        PrimeiroAcesso = true
                    };

                    var senhaValida = "Aaa111*";
                    
                    usuario.Id = Guid.NewGuid().ToString();
                    var resultado = _gerenciadorUsuarios.CreateAsync(usuario, senhaValida).Result;

                    if (resultado.Succeeded == false)
                    {
                        dbContextTransaction.Rollback();
                        return false;
                    }

                    var nivelAcessoAdministrador = _applicationDBContext.NiveisAcessos.Where(n => n.Name == "Administrador").FirstOrDefault();

                    if (nivelAcessoAdministrador == null)
                    {
                        dbContextTransaction.Rollback();
                        return false;
                    }

                    var usuarioExiste = _applicationDBContext.Usuarios.Where(u => u.Id == usuario.Id).FirstOrDefault();

                    if (usuarioExiste == null)
                    {
                        dbContextTransaction.Rollback();
                        return false;
                    }

                    var usuarioNivelAcesso = new UsuarioNivelAcesso { RoleId = nivelAcessoAdministrador.Id, UserId = usuarioExiste.Id };

                    _applicationDBContext.UsuariosNivelAcessos.Add(usuarioNivelAcesso);
                    _applicationDBContext.SaveChanges();

                    dbContextTransaction.Commit();
                    return true;
                    
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return false;
                }
            }
        }

        public virtual List<UsuarioNivelAcessoDTO> ListarUsuariosComOuSemNivelAcesso()
        {
            var listaUsuario = _applicationDBContext.Usuarios;

            var listaUsuarioNivelAcesso = _applicationDBContext.UsuariosNivelAcessos;
            var listaNivelAcessos = _applicationDBContext.NiveisAcessos;

            var resultado = from u in listaUsuario
                            join
            una in listaUsuarioNivelAcesso on u.Id equals una.UserId
            into resultado1
                            from resultaLeft in resultado1.DefaultIfEmpty()
                            join na in listaNivelAcessos on resultaLeft.RoleId equals na.Id into rfLeft
                            from rfLeftJoin in rfLeft.DefaultIfEmpty()
                            select new UsuarioNivelAcessoDTO { UserId = u.Id, RoleId = rfLeftJoin.Id, NomeNivelAcesso = rfLeftJoin.Name, NomeUsuario = u.UserName };

            return resultado.ToList();
        }

        public virtual List<UsuarioNivelAcesso> ListarUsuarioNivelAcesso()
        {
            return _applicationDBContext.UsuariosNivelAcessos.ToList();
        }

        public virtual bool ExcluirUsuarioNivelAcesso(UsuarioNivelAcesso UsuarioNivelAcesso)
        {
            try
            {
                _applicationDBContext.UsuariosNivelAcessos.Remove(UsuarioNivelAcesso);
                _applicationDBContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
