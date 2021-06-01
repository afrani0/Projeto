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
    public class LoginNegocio : ILoginNegocio
    {
        private LoginRepositorio _loginRepositorio;
        private UsuarioRepositorio _usuarioRepositorio;
        private NivelAcessoRepositorio _nivelAcessoRepositorio;
        private UsuarioNivelAcessoRepositorio _usuarioNivelAcessoRepositorio;

        public LoginNegocio(LoginRepositorio loginRepositorio, UsuarioRepositorio usuarioRepositorio, NivelAcessoRepositorio nivelAcessoRepositorio, UsuarioNivelAcessoRepositorio usuarioNivelAcessoRepositorio)
        {
            _loginRepositorio = loginRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _nivelAcessoRepositorio = nivelAcessoRepositorio;
            _usuarioNivelAcessoRepositorio = usuarioNivelAcessoRepositorio;
        }

        public async Task<RespostaNegocio> Login(Usuario usuario, string senha)
        {
            if (usuario == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Usuário não pode ser nulo, entre em contato com o Administrador" };

            if (string.IsNullOrEmpty(usuario.UserName))
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Nome Usuário não pode ser nulo ou vazio, entre em contato com o Administrador" };

            if (string.IsNullOrEmpty(senha))
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Senha não pode ser nula ou vazia, entre em contato com o Administrador" };

            //para saber se o usuário existe
            var usuarioExiste = await _usuarioRepositorio.BuscarUsuarioPorNomeUsuario(usuario.UserName);

            if (usuarioExiste == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Aviso: O usuário pode ter sido excluído anteriormente ou não existe no sistema" };

            var senhaValida = await _usuarioRepositorio.ValidaSenhaParaUsuarioEspecificado(usuarioExiste, senha);

            if (senhaValida == false)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Aviso, Mensagem = "Senha não é válida para o usuário espeficicado" };

            await _loginRepositorio.Login(usuarioExiste);

            return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Mensagem = "Logado com sucesso", Objeto = usuarioExiste };

        }

        public async Task<RespostaNegocio> Logout()
        {
            await _loginRepositorio.Logout();

            return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso, Mensagem = "Deslogado com sucesso" };
        }

        public RespostaNegocio PrimeiroUsoDoSistema()
        {
            // se for a primeira vez que o sistema é usado, então será criado os níveis de acesso automaticamente

            _nivelAcessoRepositorio.CriarNiveisAcesso();

            var listaNivelAcesso = _nivelAcessoRepositorio.ListarNivelAcesso();

            if (listaNivelAcesso == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar perfil, entre em contato com o Administrador" };

            var existeNivelAcesso = listaNivelAcesso.Where(e => e.Name == "Administrador").FirstOrDefault();
            if (existeNivelAcesso == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar perfil/nível acesso do tipo 'Administrador', entre em contato com o Administrador" };

            var listarUsuarioNivelAcesso = _usuarioNivelAcessoRepositorio.ListarUsuarioNivelAcesso();

            if (listarUsuarioNivelAcesso == null)
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar lista de usuários associados, entre em contato com o Administrador" };
            var existeUsuarioNivelAcesso = listarUsuarioNivelAcesso.Where(una => una.RoleId == existeNivelAcesso.Id);

            if (existeUsuarioNivelAcesso.Count() == 0)
            {
                var listarUsuarios = _usuarioRepositorio.ListarUsuarios();

                if (listarUsuarios == null)
                    return new RespostaNegocio() { Tipo = Enumerador.Tipo.Erro, Mensagem = "Erro: Ocorreu um erro ao buscar lista de usuários , entre em contato com o Administrador" };

                var existeAoMenosUmUsuario = listarUsuarios.Where(c => c.UserName == "adm").ToList();

                if (existeAoMenosUmUsuario.Count() > 0)
                    return new RespostaNegocio()
                    {
                        Tipo = Enumerador.Tipo.Erro,
                        Mensagem = "Erro: Não existe nenhum usuário com perfil 'Administrador' no sistema, e ao tentar criar um usuário" +
                        " com nome do login 'adm' para uso, foi identificado que o mesmo já existe gerando o erro, entre em contato com o Administrador"
                    };


                var retornoUsuarioNivelAcesso = _usuarioNivelAcessoRepositorio.CriarPrimeiroUsuarioComNivelAcessoDoTipoAdministrador();

                if (retornoUsuarioNivelAcesso)
                {
                    return new RespostaNegocio()
                    {
                        Tipo = Enumerador.Tipo.Aviso,
                        Mensagem = "***ATENÇÃO: Como é a primeira vez que o sistema está sendo utilizado, foi criado um usuário" +
                            " administrador para utilizar o sistema. \n\n O Login é  'adm' e a senha provisória é 'Aaa111*', que deve ser mudada para uma senha de sua escolha."

                    };
                }
                else
                {
                    return new RespostaNegocio()
                    {
                        Tipo = Enumerador.Tipo.Erro,
                        Mensagem = "Erro: Ocorreu um erro ao tentar Salvar/Editar um novo usuário do tipo 'Administrador' para tornar possível a utilização do sistema, entre em contato com o Administrador"

                    };
                }

            }
            else
            {
                //o sucesso significa que não é a primeira vez que o sistema está em uso ,pois já existe usuário com perfil administrador.
                return new RespostaNegocio() { Tipo = Enumerador.Tipo.Sucesso };
            }
        }
    }
}
