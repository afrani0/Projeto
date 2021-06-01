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
    public class SenhaNegocio : ISenhaNegocio
    {
        public UsuarioRepositorio _usuarioRepositorio;
        public SenhaUtil _senhaUtil;

        public SenhaNegocio(UsuarioRepositorio usuarioRepositorio, SenhaUtil senhaUtil)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _senhaUtil = senhaUtil;
        }

        public async Task<RespostaNegocio> NovaSenha(string UsuarioId)
        {
            if (String.IsNullOrEmpty(UsuarioId))
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador" };

            if (UsuarioId == "0")
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Id não pode ser zero, entre em contato com o Administrador" };

            var retornoUsuarioRepositorio = await _usuarioRepositorio.BuscarUsuarioPorId(UsuarioId);

            if (retornoUsuarioRepositorio == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: O usuário selecionado não foi encontrado no sistema, pode ser que tenha sido excluído, caso o erro persista entre em contato com o administrador" };

            var retornoSenha = _senhaUtil.GeraSenhaAleatoria();

            if (retornoSenha == "")
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Não foi possível criar nova senha para usuário selecionado, entre em contato com o Administrador" };

            retornoUsuarioRepositorio.PrimeiroAcesso = true;

            var retornoMudarSenhaRepositorio = _usuarioRepositorio.MudarSenhaUsandoPerfilAdministrador(retornoUsuarioRepositorio, retornoSenha).Result;
            
            if (retornoMudarSenhaRepositorio.Succeeded)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Mensagem = "Nova Senha criada com sucesso", Objeto = retornoSenha };
            else
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao tentar modificar a senha, entre em contato com o Adminstrador" };
        }
    }
}
