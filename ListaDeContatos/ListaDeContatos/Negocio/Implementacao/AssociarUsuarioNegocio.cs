using ListaDeContatos.Models;
using ListaDeContatos.Negocio.Interface;
using ListaDeContatos.Repositorio.Implementacao;
using ListaDeContatos.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Negocio.Implementacao
{
    public class AssociarUsuarioNegocio : IAssociarUsuarioNegocio
    {
        private readonly UsuarioNivelAcessoRepositorio _usuarioNivelAcessoRepositorio;
        private readonly NivelAcessoRepositorio _nivelAcessoRepositorio;

        public AssociarUsuarioNegocio(UsuarioNivelAcessoRepositorio usuarioNivelAcessoRepositorio, NivelAcessoRepositorio nivelAcessoRepositorio)
        {
            _usuarioNivelAcessoRepositorio = usuarioNivelAcessoRepositorio;
            _nivelAcessoRepositorio = nivelAcessoRepositorio;
        }

        public RespostaNegocio ListarUsuariosComOuSemNivelAcesso()
        {

            var listaUsuarioNivelAcesso = _usuarioNivelAcessoRepositorio.ListarUsuariosComOuSemNivelAcesso();

            if (listaUsuarioNivelAcesso == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar lista de usuários que tenham associado algum 'perfil / nível de acesso' , entre em contato com o Administrador" };

            return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Objeto = listaUsuarioNivelAcesso };
        }

        public RespostaNegocio ListarNiveisAcesso()
        {

            var listaUsuarioNivelAcesso = _nivelAcessoRepositorio.ListarNivelAcesso();

            if (listaUsuarioNivelAcesso == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar lista de 'perfil / nível de acesso' para edição, entre em contato com o Administrador" };

            return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Objeto = listaUsuarioNivelAcesso }; ;
        }

        public RespostaNegocio EditarNivelAcessoDoUsuario(string usuarioId, string nivelAcessoId)
        {

            if (string.IsNullOrEmpty(usuarioId))
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Id do Usuário não pode ser nulo ou vazio, entre em contato com o Administrador" };

            if (usuarioId == "0")
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Id do Usuário não pode ser zero, entre em contato com o Administrador" };

            if (string.IsNullOrEmpty(nivelAcessoId))
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Id do Nível Acesso não pode ser nulo ou vazio, entre em contato com o Administrador" };

            if (nivelAcessoId == "0")
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Id do Nível Acesso não pode ser zero, entre em contato com o Administrador" };

            //
            var usuarioNivelAcesso = new UsuarioNivelAcesso() { UserId = usuarioId, RoleId = nivelAcessoId };
            ///
            var listaNivelAcesso = _nivelAcessoRepositorio.ListarNivelAcesso();

            if (listaNivelAcesso == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar lista de 'perfil / nível de acesso' para edição, entre em contato com o Administrador" };

            var roleAdministrador = listaNivelAcesso.Where(c => c.Name == "Administrador").FirstOrDefault();

            if (roleAdministrador == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar 'perfil / nível de acesso' do tipo Administrador, entre em contato com o Administrador" };

            var listaUsuarioNivelAcesso = _usuarioNivelAcessoRepositorio.ListarUsuarioNivelAcesso();

            if (listaUsuarioNivelAcesso == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar lista de Usuários com seu respectivo 'perfil / nível de acesso', entre em contato com o Administrador" };

            //lista somente de Usuários que tenham Perfil Administrador 
            var listaUsuarioNivelAcessoTipoAdministrador = listaUsuarioNivelAcesso.Where(l => l.RoleId == roleAdministrador.Id).ToList();

            //Garante que o sistema não fique sem nenhum usuário administrador
            if (listaUsuarioNivelAcessoTipoAdministrador.Count() <= 1 && nivelAcessoId != roleAdministrador.Id && listaUsuarioNivelAcessoTipoAdministrador.Select(u => u.UserId).Contains(usuarioId))
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: Não foi permitindo edição pois tem somente 1 usuário no sistema com perfil 'administrador'" };
                        
            var sucesso = _usuarioNivelAcessoRepositorio.SalvarEditarUsuarioNivelAcesso(usuarioNivelAcesso);

            if (sucesso)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Mensagem = "Edição de Usuário associado com 'Perfil / Nível Acesso' salvo com sucesso" };
            else
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao tentar salvar a edição do Usuário com o 'Perfil / Nível Acesso' associado, entre em contato com o Administrador" };
                       
        }
        
        public RespostaNegocio ExcluirNivelAcessoDoUsuario(string usuarioId)
        {
            if (string.IsNullOrEmpty(usuarioId))
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Id do Usuário não pode ser nulo ou vazio, entre em contato com o Administrador" };

            if (usuarioId == "0")
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Id do Usuário não pode ser zero, entre em contato com o Administrador" };

            var listaNivelAcesso = _nivelAcessoRepositorio.ListarNivelAcesso();

            if (listaNivelAcesso == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar lista de 'perfil / nível de acesso' para exclusão, entre em contato com o Administrador" };

            var roleAdministrador = listaNivelAcesso.Where(c => c.Name == "Administrador").FirstOrDefault();

            if (roleAdministrador == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar 'perfil / nível de acesso' do tipo Administrador, entre em contato com o Administrador" };

            var listaUsuarioNivelAcesso = _usuarioNivelAcessoRepositorio.ListarUsuarioNivelAcesso();

            if (listaUsuarioNivelAcesso == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar lista de Usuários com seu respectivo 'perfil / nível de acesso' para exclusão, entre em contato com o Administrador" };

            //lista somente de Usuários que tenham Perfil Administrador 
            var listaUsuarioNivelAcessoTipoAdministrador = listaUsuarioNivelAcesso.Where(l => l.RoleId == roleAdministrador.Id).ToList();

            if (listaUsuarioNivelAcessoTipoAdministrador.Count() <= 1 && listaUsuarioNivelAcessoTipoAdministrador.Select(s => s.UserId).Contains(usuarioId))
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: Não foi permitindo exclusão pois tem somente 1 usuário no sistema com perfil 'administrador'" };

            //caso o usuário selecionado não tenha associação com Nível Acesso/Perfil, então pode ser que essa associação já tenha sido excluida
            var usuarioNivelAcesso = listaUsuarioNivelAcesso.Where(u => u.UserId == usuarioId).FirstOrDefault();

            if (usuarioNivelAcesso == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: Usuário não tem 'perfil / nível de acesso' associado para ser excluído" };

            var sucesso = _usuarioNivelAcessoRepositorio.ExcluirUsuarioNivelAcesso(usuarioNivelAcesso);

            if (sucesso)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Mensagem = "Usuário associado com 'Perfil / Nível Acesso' excluído com sucesso" };
            else
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao tentar excluir o Usuário com o 'Perfil / Nível Acesso' associado, entre em contato com o Administrador" };

        }


    }
}
