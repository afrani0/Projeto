using ListaDeContatos.Enumerador;
using ListaDeContatos.Negocio.Interface;
using ListaDeContatos.Repositorio.Implementacao;
using ListaDeContatos.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Negocio.Implementacao
{
    public class PrimeiroAcessoNegocio : IPrimeiroAcessoNegocio
    {
        private UsuarioRepositorio _usuarioRepositorio = null;
        private LoginRepositorio _loginRepositorio = null;

        public PrimeiroAcessoNegocio(UsuarioRepositorio usuarioRepositorio, LoginRepositorio loginRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _loginRepositorio = loginRepositorio;
        }

        public async Task<RespostaNegocio> ConcluirPrimeiroAcesso(string id, string senhaAtual, string novaSenha, string confirmarNovaSenha)
        {
            if (string.IsNullOrEmpty(id))
                return new RespostaNegocio() { Tipo = Tipo.Erro, Mensagem = "Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador" };

            if (string.IsNullOrEmpty(senhaAtual))
                return new RespostaNegocio() { Tipo = Tipo.Erro, Mensagem = "Erro: Senha Atual não pode ser nulo ou vazio, entre em contato com o Administrador" };

            if (string.IsNullOrEmpty(novaSenha))
                return new RespostaNegocio() { Tipo = Tipo.Erro, Mensagem = "Erro: Nova Senha não pode ser nulo ou vazio, entre em contato com o Administrador" };

            if (string.IsNullOrEmpty(confirmarNovaSenha))
                return new RespostaNegocio() { Tipo = Tipo.Erro, Mensagem = "Erro: Confirmar Nova Senha não pode ser nulo ou vazio, entre em contato com o Administrador" };

            if (novaSenha.Trim() != confirmarNovaSenha.Trim())
                return new RespostaNegocio() { Tipo = Tipo.Aviso, Mensagem = "Campos 'Nova Senha' e 'Confirmar Nova Senha' devem ser iguais" };

            if (novaSenha.Trim() == senhaAtual.Trim())
                return new RespostaNegocio() { Tipo = Tipo.Aviso, Mensagem = "A 'Nova Senha' não pode ser igual a 'Senha Atual'" };

            //buscar usuário
            var usuario = await _usuarioRepositorio.BuscarUsuarioPorId(id);

            if (usuario == null)
                return new RespostaNegocio() { Tipo = Tipo.Erro, Mensagem = "Erro: Usuário não pode ser nulo, entre em contato com o Administrador" };
           
            var usuarioCriado = await _usuarioRepositorio.MudarSenha(usuario, senhaAtual, novaSenha);

            if (usuarioCriado.Succeeded)
            {
                usuario.PrimeiroAcesso = false;
                var primeiroAcessoModificado = await _usuarioRepositorio.EditarUsuario(usuario);

                if(!primeiroAcessoModificado.Succeeded)
                    return new RespostaNegocio() { Tipo = Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao tentar modificar o estado do Login para deixar de ser primeiro acesso, entre em contato com o Administrador." };
                await _loginRepositorio.Logout();

                return new RespostaNegocio() { Tipo = Tipo.Sucesso, Mensagem = "Senha modificada com sucesso." };
            }
            else
            {                               
                return new RespostaNegocio() { Tipo = Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao tentar modificar a senha do usuário, entre em contato com o Administrador.", Erros = usuarioCriado.Errors };
            }
        }
    }
}
