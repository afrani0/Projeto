using ListaDeContatos.Models;
using ListaDeContatos.Negocio.Interface;
using ListaDeContatos.Repositorio.Implementacao;
using ListaDeContatos.Repositorio.Interface;
using ListaDeContatos.Util;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Negocio.Implementacao
{
    public class RegistroNegocio : IRegistroNegocio
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public RegistroNegocio(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<RespostaNegocio> EditarUsuario(Usuario usuario)
        {
            if (usuario == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Usuário não pode ser nulo, entre em contato com o Administrador" };

            if (string.IsNullOrEmpty(usuario.Id))
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador" };

            var existeUsuario = await _usuarioRepositorio.BuscarUsuarioPorId(usuario.Id);

            if (existeUsuario == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: O usuário não existe no sistema" };

            var listaUsuarios = _usuarioRepositorio.ListarUsuarios();

            if (listaUsuarios == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar lista de usuários, entre em contato com o Administrador" };

            listaUsuarios = listaUsuarios.Where(l => (l.UserName.ToUpper() == usuario.UserName.ToUpper()) && (l.Id.Trim() != usuario.Id.Trim()));

            if (listaUsuarios.ToList().Count > 0)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: Já existe 'Nome Usuário' com esse valor, só pode ser inserido 'Nome Usuário' que ainda não exista no sistema" };

            var retornoRepositorio = await _usuarioRepositorio.EditarUsuario(usuario);

            if (retornoRepositorio.Succeeded)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Mensagem = "Usuário editado com sucesso" };
            else
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao tentar editar o usuário, entre em contato com o Administrador", Erros = retornoRepositorio.Errors };
        }

        public async Task<RespostaNegocio> BuscarUsuarioPorRole(string role)
        {
            if (string.IsNullOrEmpty(role))
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: papel não pode ser nulo ou vazio, entre em contato com o Administrador" };

            var retornoRepositorio = await _usuarioRepositorio.BuscarUsuarioPorRole(role);

            if (retornoRepositorio == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Lista de Usuários não pode ser nula, entre em contato com o Administrador" };

            return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Objeto = retornoRepositorio };
        }

        public async Task<RespostaNegocio> RemoverRolesPorUsuario(Usuario usuario, IList<string> roles)
        {
            if (usuario == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Usuário não pode ser nulo, entre em contato com o Administrador" };

            if (string.IsNullOrEmpty(usuario.Id))
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador" };

            if(roles == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Lista de Permissões não pode ser nula, entre em contato com o Administrador" };

            var usuarioExiste = await _usuarioRepositorio.BuscarUsuarioPorId(usuario.Id);

            if(usuarioExiste == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: O usuário pode ter sido excluído anteriormente ou não existe no sistema" };

            var retornoRepositorio =  await _usuarioRepositorio.RemoverRolesPorUsuario(usuario, roles);
            
            if (retornoRepositorio.Succeeded == true)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Mensagem = "Permissões removidas com sucesso" };
            else
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao tentar remover as permissões, entre em contato com o Administrador" };
        }

        public async Task<RespostaNegocio> CriarUsuario(Usuario usuario, string senha)
        {
            if (usuario == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Usuário não pode ser nulo, entre em contato com o Administrador" };

            if (string.IsNullOrEmpty(senha))
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Senha não pode ser nula ou vazia, entre em contato com o Administrador" };

            var usuarioExiste = await _usuarioRepositorio.BuscarUsuarioPorNomeUsuario(usuario.UserName);

            if (usuarioExiste != null)///
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: '" + usuario.UserName + " ' já existe no sistema, deve ser digitado outro 'Nome Usuário'" };
                        
            var retornoRepositorio = await _usuarioRepositorio.CriarUsuario(usuario, senha);

            if (retornoRepositorio.Succeeded == true)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Mensagem = "Usuário salvo com sucesso" };
            else
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao tentar salvar o usuário, entre em contato com o Administrador", Erros = retornoRepositorio.Errors };

        }

        public async Task<RespostaNegocio> BuscarRolesPorUsuario(Usuario usuario)
        {
            if (usuario == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Usuário não pode ser nulo, entre em contato com o Administrador" };

            if (string.IsNullOrEmpty(usuario.Id))
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Id do Usuário não pode ser nulo ou vazio, entre em contato com o Administrador" };

            var usuarioBuscar = await _usuarioRepositorio.BuscarUsuarioPorId(usuario.Id);

            if (usuarioBuscar == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: O usuário pode ter sido excluído anteriormente ou não existe no sistema" };

            var roleUsuario = await _usuarioRepositorio.BuscarRolesPorUsuario(usuarioBuscar);

            if (roleUsuario == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar as permissões do usuário, entre em contato com o Administrador" };
            return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Objeto = roleUsuario };
        }

        public async Task<RespostaNegocio> DeletarUsuario(Usuario usuario)
        {
            if (usuario == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: O Usuário não pode ser nulo, entre em contato com o Administrador" };
            if (string.IsNullOrEmpty(usuario.Id))
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador" };

            var usuarioDeletar = await _usuarioRepositorio.BuscarUsuarioPorId(usuario.Id);
            
            if (usuarioDeletar == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: O usuário pode ter sido excluído anteriormente ou não existe no sistema" };

            var usuarioRole = await _usuarioRepositorio.BuscarRolesPorUsuario(usuarioDeletar);

            if (usuarioRole == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Não foi possível excluir usuário, porque antes ocorreu um erro ao tentar buscar a permissão de acesso do usuário, entre em contato com o Administrador" };

            //remove todas as associações de roles de um usuário
            if (usuarioRole.Count > 0)
            {
                if (usuarioRole.Contains("Administrador"))
                {
                    var totalRoleAdministrador = await _usuarioRepositorio.BuscarUsuarioPorRole("Administrador");

                    if (totalRoleAdministrador == null)
                        return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Não foi possível excluir usuário, porque antes ocorreu um erro ao tentar buscar usuário para a permissão 'Administrador', entre em contato com o Administrador" };

                    if (totalRoleAdministrador.Count() <= 1)
                    {
                        return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Atenção: Não é permitido exclusão do usuário ' " + usuarioDeletar.UserName + " '. O sistema não pode ficar sem ao menos 1 usuário com permissão 'Administrador'" };
                    }
                }
            }

            var statusUsuario = _usuarioRepositorio.DeletarUsuario(usuarioDeletar, usuarioRole);
            
            if (statusUsuario)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Mensagem = "Usuário excluído com sucesso" };
            else
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao tentar excluir o usuário, entre em contato com o Administrador" };
        }

        public async Task<RespostaNegocio> BuscarUsuarioPorId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador" };
            }

            if (id == "0")
            {
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Id não pode ser zero, entre em contato com o Administrador" };
            }

            var retorno = await _usuarioRepositorio.BuscarUsuarioPorId(id.Trim());

            if (retorno == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: Não foi encontrado nenhum usuário com o id especificado, entre em contato com o Administrador" };
            else
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Objeto = retorno };
        }

        public virtual RespostaNegocio ListarUsuarios()
        {
            var entidade = _usuarioRepositorio.ListarUsuarios();

            if (entidade == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar lista de usuários, entre em contato com o Administrador" };
            else
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Objeto = entidade };
        }
    }
}
