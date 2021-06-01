using ListaDeContatos.Enumerador;
using ListaDeContatos.Models;
using ListaDeContatos.Negocio.Implementacao;
using ListaDeContatos.Repositorio.Implementacao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestesUnitarios
{
    [TestFixture]
    class LoginTeste
    {
        #region Login(Usuario usuario, string senha)

        [Test]
        //o objeto usuário deve ser null
        public void Login_UsuarioDeveSerNull_Erro()
        {
            Usuario usuario = null;
            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Usuario>>();
            var SignInManagerMock = new SignInManager<Usuario>(new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object, _contextAccessor.Object, _userPrincipalFactory.Object, null, null, null);
            var loginNegocio = new LoginNegocio(new LoginRepositorio(SignInManagerMock),
                new UsuarioRepositorio(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())),
                new NivelAcessoRepositorio(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>())),
                new UsuarioNivelAcessoRepositorio(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()), new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object));

            var retornoNegocio = loginNegocio.Login(usuario, null).Result;

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Usuário não pode ser nulo, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }


        [Test]
        //a propriedade UserName do objeto usuário deve ser null ou vazio
        public void Login_UsuarioUserNameDeveSerNullOuVazio_Erro()
        {

            var usuario = new Usuario() { UserName = "" };

            var iHttpContextAccessor = new Mock<IHttpContextAccessor>().Object;
            var iUserClaimsPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Usuario>>().Object;
            var loginRepositorio = new LoginRepositorio(new SignInManager<Usuario>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), iHttpContextAccessor, iUserClaimsPrincipalFactory, null, null, null));
            var loginNegocio = new LoginNegocio(loginRepositorio, new UsuarioRepositorio(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())),                
                new NivelAcessoRepositorio(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>())),
                new UsuarioNivelAcessoRepositorio(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()), new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object));

            var retornoNegocio = loginNegocio.Login(usuario, null).Result;

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Nome Usuário não pode ser nulo ou vazio, entre em contato com o Administrador", retornoNegocio.Mensagem);

            usuario = null;
            usuario = new Usuario() { UserName = null };
            retornoNegocio = null;
            retornoNegocio = loginNegocio.Login(usuario, null).Result;

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Nome Usuário não pode ser nulo ou vazio, entre em contato com o Administrador", retornoNegocio.Mensagem);
        }


        [Test]
        //o parâmetro senha deve ser null ou vazia
        public void Login_SenhaDeveSerNulaOuVazia_Erro()
        {
            var usuario = new Usuario() { UserName = "895df8d800" };
            var senha = "";
            var iUserClaimsPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Usuario>>().Object;
            var iHttpContextAccessor = new Mock<IHttpContextAccessor>().Object;
            var loginRepositorio = new LoginRepositorio(new SignInManager<Usuario>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), iHttpContextAccessor, iUserClaimsPrincipalFactory, null, null, null));
            var loginNegocio = new LoginNegocio(loginRepositorio, new UsuarioRepositorio(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())),                
                new NivelAcessoRepositorio(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>())),
                new UsuarioNivelAcessoRepositorio(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()), new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object));

            var retornoNegocio = loginNegocio.Login(usuario, senha).Result;

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Senha não pode ser nula ou vazia, entre em contato com o Administrador", retornoNegocio.Mensagem);


            senha = null;
            retornoNegocio = null;
            retornoNegocio = loginNegocio.Login(usuario, senha).Result;

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Senha não pode ser nula ou vazia, entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        //o usuário não deve existir no sistema
        public void Login_Fun_BuscarUsuarioPorNomeUsuario_UsuarioNaoDeveExistir_Aviso()
        {
            var usuario = new Usuario() { UserName = "fabind800" };
            var senha = "Abc123*";
            var iUserClaimsPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<Usuario>>().Object;
            var iHttpContextAccessorMock = new Mock<IHttpContextAccessor>().Object;
            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var loginRepositorioMock = new Mock<LoginRepositorio>(new SignInManager<Usuario>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), iHttpContextAccessorMock, iUserClaimsPrincipalFactoryMock, null, null, null)).Object;
            var loginNegocio = new LoginNegocio(loginRepositorioMock, usuarioRepositorioMock.Object,
                new NivelAcessoRepositorio(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>())),
                new UsuarioNivelAcessoRepositorio(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()), new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object));

            var usuarioBuscado = "fabind899";

            usuarioRepositorioMock.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorNomeUsuario(usuarioBuscado)).Returns(Task.FromResult((usuario.UserName == usuarioBuscado) ? usuario : null));

            var retornoNegocio = loginNegocio.Login(usuario, senha).Result;

            Assert.AreEqual(Tipo.Aviso, retornoNegocio.Tipo);
            Assert.AreEqual("Aviso: O usuário pode ter sido excluído anteriormente ou não existe no sistema", retornoNegocio.Mensagem);

        }

        [Test]
        //o usuário não deve existir no sistema
        public void Login_Fun_ValidaSenhaParaUsuarioEspecificado_UsuarioNaoDeveExistir_Aviso()
        {
            //simulando usuário e senha no sistema
            var usuario = new Usuario() { UserName = "fabind800" };
            var senha = "Abc123*";

            var iUserClaimsPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<Usuario>>().Object;
            var iHttpContextAccessorMock = new Mock<IHttpContextAccessor>().Object;
            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var loginRepositorioMock = new Mock<LoginRepositorio>(new SignInManager<Usuario>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), iHttpContextAccessorMock, iUserClaimsPrincipalFactoryMock, null, null, null)).Object;
            var loginNegocio = new LoginNegocio(loginRepositorioMock, usuarioRepositorioMock.Object,
                new NivelAcessoRepositorio(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>())),
                new UsuarioNivelAcessoRepositorio(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()), new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object));

            //usuario e senha digitados
            var usuarioBuscado = "fabind800";
            var senhaDigitada = "Abc122*";

            usuarioRepositorioMock.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorNomeUsuario(usuarioBuscado)).Returns(Task.FromResult((usuario.UserName == usuarioBuscado) ? usuario : null));
            usuarioRepositorioMock.Setup<Task<bool>>(u => u.ValidaSenhaParaUsuarioEspecificado(usuario, senhaDigitada)).Returns(Task.FromResult((senhaDigitada == senha) ? true : false));


            var retornoNegocio = loginNegocio.Login(usuario, senha).Result;

            Assert.AreEqual(Tipo.Aviso, retornoNegocio.Tipo);
            Assert.AreEqual("Senha não é válida para o usuário espeficicado", retornoNegocio.Mensagem);

        }

        [Test]
        public void Login_Sucesso()
        {
            //simulando usuário e senha no sistema
            var usuario = new Usuario() { UserName = "fabind800" };
            var senha = "Abc123*";

            var iUserClaimsPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<Usuario>>().Object;
            var iHttpContextAccessorMock = new Mock<IHttpContextAccessor>().Object;
            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var loginRepositorioMock = new Mock<LoginRepositorio>(new SignInManager<Usuario>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), iHttpContextAccessorMock, iUserClaimsPrincipalFactoryMock, null, null, null));
            var loginNegocio = new LoginNegocio(loginRepositorioMock.Object, usuarioRepositorioMock.Object,
                new NivelAcessoRepositorio(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>())),
                new UsuarioNivelAcessoRepositorio(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()), new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object));

            //usuario e senha digitados
            var usuarioBuscado = "fabind800";
            var senhaDigitada = "Abc123*";

            usuarioRepositorioMock.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorNomeUsuario(usuarioBuscado)).Returns(Task.FromResult((usuario.UserName == usuarioBuscado) ? usuario : null));
            usuarioRepositorioMock.Setup<Task<bool>>(u => u.ValidaSenhaParaUsuarioEspecificado(usuario, senhaDigitada)).Returns(Task.FromResult((senhaDigitada == senha) ? true : false));

            loginRepositorioMock.Setup(l => l.Login(usuario));

            var retornoNegocio = loginNegocio.Login(usuario, senha).Result;

            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual("Logado com sucesso", retornoNegocio.Mensagem);

        }

        #endregion

        #region Logout()
        [Test]
        public void Logout_Sucesso()
        {
            var iHttpContextAccessor = new Mock<IHttpContextAccessor>().Object;
            var iUserClaimsPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Usuario>>().Object;

            var loginRepositorio = new Mock<LoginRepositorio>(new SignInManager<Usuario>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), iHttpContextAccessor, iUserClaimsPrincipalFactory, null, null, null));
            var loginNegocio = new LoginNegocio(loginRepositorio.Object, new UsuarioRepositorio(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())),                
                new NivelAcessoRepositorio(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>())),
                new UsuarioNivelAcessoRepositorio(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()), new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object));

            loginRepositorio.Setup(l => l.Logout());


            var retornoNegocio = loginNegocio.Logout().Result;

            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual("Deslogado com sucesso", retornoNegocio.Mensagem);
        }

        #endregion

        #region PrimeiroUsoDoSistema()
        [Test]
        //A func ListarNivelAcesso  deve retornar lista nula
        public void PrimeiroUsoDoSistema_Func_ListarNivelAcesso_ListaDeveSerNull_Erro()
        {
            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Usuario>>();
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()));
            var SignInManagerMock = new SignInManager<Usuario>(new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object, _contextAccessor.Object, _userPrincipalFactory.Object, null, null, null);
            var loginNegocio = new LoginNegocio(new LoginRepositorio(SignInManagerMock),
                new UsuarioRepositorio(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())),
                _nivelAcessoRepositorioMock.Object,
                new UsuarioNivelAcessoRepositorio(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()), new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object));

            _nivelAcessoRepositorioMock.Setup(n => n.CriarNiveisAcesso());//simula função executada
            _nivelAcessoRepositorioMock.Setup(n => n.ListarNivelAcesso());

            var retornoNegocio = loginNegocio.PrimeiroUsoDoSistema();

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao buscar perfil, entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        //O retorno da ListarNivelAcesso não deve ter Nível Acesso com 'Name == Administrador'
        public void PrimeiroUsoDoSistema_RetornoListarNivelAcesso_DeveSerNull_Erro()
        {
            List<NivelAcesso> listaBanco = new List<NivelAcesso>();
            //listaBanco.Add(new NivelAcesso { Name = "Administrador", Descricao = "Tem acesso total de todas as funcionalidades do sistema, inclusive funcionalidades de gerenciamento de usuários, senhas, acessos" });
            listaBanco.Add(new NivelAcesso { Name = "Completo", Descricao = "Tem acesso total de todas as funcionalidades do sistema, como criar, editar, excluir, ver detalhes e listar" });
            listaBanco.Add(new NivelAcesso { Name = "Basico", Descricao = "Tem acesso limitado de todas as funcionalidades do sistema, pode só ver a lista e detalhes de uma funcionalidade" });

            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Usuario>>();
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()));
            var SignInManagerMock = new SignInManager<Usuario>(new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object, _contextAccessor.Object, _userPrincipalFactory.Object, null, null, null);
            var loginNegocio = new LoginNegocio(new LoginRepositorio(SignInManagerMock),
                new UsuarioRepositorio(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())),
                _nivelAcessoRepositorioMock.Object,
                new UsuarioNivelAcessoRepositorio(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()), new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object));

            _nivelAcessoRepositorioMock.Setup(n => n.CriarNiveisAcesso());//simula função executada
            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(n => n.ListarNivelAcesso()).Returns(listaBanco);

            var retornoNegocio = loginNegocio.PrimeiroUsoDoSistema();

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao buscar perfil/nível acesso do tipo 'Administrador', entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        //A func ListarUsuarioNivelAcesso deve retornar null
        public void PrimeiroUsoDoSistema_Func_ListarUsuarioNivelAcesso_DeveSerNull_Erro()
        {
            List<NivelAcesso> listaBanco = new List<NivelAcesso>();
            listaBanco.Add(new NivelAcesso { Name = "Administrador", Descricao = "Tem acesso total de todas as funcionalidades do sistema, inclusive funcionalidades de gerenciamento de usuários, senhas, acessos" });
            listaBanco.Add(new NivelAcesso { Name = "Completo", Descricao = "Tem acesso total de todas as funcionalidades do sistema, como criar, editar, excluir, ver detalhes e listar" });
            listaBanco.Add(new NivelAcesso { Name = "Basico", Descricao = "Tem acesso limitado de todas as funcionalidades do sistema, pode só ver a lista e detalhes de uma funcionalidade" });

            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Usuario>>();
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()), new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object);

            var SignInManagerMock = new SignInManager<Usuario>(new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object, _contextAccessor.Object, _userPrincipalFactory.Object, null, null, null);
            var loginNegocio = new LoginNegocio(new LoginRepositorio(SignInManagerMock),
                new UsuarioRepositorio(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())),
                _nivelAcessoRepositorioMock.Object,
                _usuarioNivelAcessoRepositorioMock.Object);

            _nivelAcessoRepositorioMock.Setup(n => n.CriarNiveisAcesso());//simula função executada
            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(n => n.ListarNivelAcesso()).Returns(listaBanco);
            _usuarioNivelAcessoRepositorioMock.Setup(a => a.ListarUsuarioNivelAcesso());

            var retornoNegocio = loginNegocio.PrimeiroUsoDoSistema();

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao buscar lista de usuários associados, entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        //A func ListarUsuario deve retornar null
        public void PrimeiroUsoDoSistema_Func_ListarUsuario_DeveSerNull_Erro()
        {
            List<NivelAcesso> listaBanco = new List<NivelAcesso>();
            listaBanco.Add(new NivelAcesso { Name = "Administrador", Descricao = "Tem acesso total de todas as funcionalidades do sistema, inclusive funcionalidades de gerenciamento de usuários, senhas, acessos" });
            listaBanco.Add(new NivelAcesso { Name = "Completo", Descricao = "Tem acesso total de todas as funcionalidades do sistema, como criar, editar, excluir, ver detalhes e listar" });
            listaBanco.Add(new NivelAcesso { Name = "Basico", Descricao = "Tem acesso limitado de todas as funcionalidades do sistema, pode só ver a lista e detalhes de uma funcionalidade" });

            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Usuario>>();
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()), new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object);
            var _usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var SignInManagerMock = new SignInManager<Usuario>(new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object, _contextAccessor.Object, _userPrincipalFactory.Object, null, null, null);
            var loginNegocio = new LoginNegocio(new LoginRepositorio(SignInManagerMock),
                _usuarioRepositorioMock.Object,
                _nivelAcessoRepositorioMock.Object,
                _usuarioNivelAcessoRepositorioMock.Object);

            IQueryable<Usuario> listaUsuarios = null;

            _usuarioRepositorioMock.Setup<IQueryable<Usuario>>(u => u.ListarUsuarios()).Returns(listaUsuarios);
            _nivelAcessoRepositorioMock.Setup(n => n.CriarNiveisAcesso());//simula função executada
            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(n => n.ListarNivelAcesso()).Returns(listaBanco);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(a => a.ListarUsuarioNivelAcesso()).Returns(new List<UsuarioNivelAcesso>());

            var retornoNegocio = loginNegocio.PrimeiroUsoDoSistema();

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao buscar lista de usuários , entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        //O retorno do ListarUsuario deve retornar lista de usuário maior que zero quando o UserName do Usuário for igual a "adm"
        public void PrimeiroUsoDoSistema_RetornoListarUsuario_DeveRetornarListaMaiorQueZero_Erro()
        {
            List<NivelAcesso> listaBanco = new List<NivelAcesso>();
            listaBanco.Add(new NivelAcesso { Name = "Administrador", Descricao = "Tem acesso total de todas as funcionalidades do sistema, inclusive funcionalidades de gerenciamento de usuários, senhas, acessos" });
            listaBanco.Add(new NivelAcesso { Name = "Completo", Descricao = "Tem acesso total de todas as funcionalidades do sistema, como criar, editar, excluir, ver detalhes e listar" });
            listaBanco.Add(new NivelAcesso { Name = "Basico", Descricao = "Tem acesso limitado de todas as funcionalidades do sistema, pode só ver a lista e detalhes de uma funcionalidade" });

            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Usuario>>();
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()), new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object);
            var _usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var SignInManagerMock = new SignInManager<Usuario>(new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object, _contextAccessor.Object, _userPrincipalFactory.Object, null, null, null);
            var loginNegocio = new LoginNegocio(new LoginRepositorio(SignInManagerMock),
                _usuarioRepositorioMock.Object,
                _nivelAcessoRepositorioMock.Object,
                _usuarioNivelAcessoRepositorioMock.Object);

            List<Usuario> listaUsuarios = new List<Usuario>();

            listaUsuarios.Add(new Usuario() { UserName = "adm" });

            _usuarioRepositorioMock.Setup<IQueryable<Usuario>>(u => u.ListarUsuarios()).Returns(listaUsuarios.AsQueryable());
            _nivelAcessoRepositorioMock.Setup(n => n.CriarNiveisAcesso());//simula função executada
            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(n => n.ListarNivelAcesso()).Returns(listaBanco);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(a => a.ListarUsuarioNivelAcesso()).Returns(new List<UsuarioNivelAcesso>());

            var retornoNegocio = loginNegocio.PrimeiroUsoDoSistema();

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Não existe nenhum usuário com perfil 'Administrador' no sistema, e ao tentar criar um usuário" +
                        " com nome do login 'adm' para uso, foi identificado que o mesmo já existe gerando o erro, entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        //A func CriarPrimeiroUsuarioComNivelAcessoDoTipoAdministrador deve retornar false
        public void PrimeiroUsoDoSistema_Func_CriarPrimeiroUsuarioComNivelAcessoDoTipoAdministrador_DeveRetornarFalse_Erro()
        {
            List<NivelAcesso> listaBanco = new List<NivelAcesso>();
            listaBanco.Add(new NivelAcesso { Name = "Administrador", Descricao = "Tem acesso total de todas as funcionalidades do sistema, inclusive funcionalidades de gerenciamento de usuários, senhas, acessos" });
            listaBanco.Add(new NivelAcesso { Name = "Completo", Descricao = "Tem acesso total de todas as funcionalidades do sistema, como criar, editar, excluir, ver detalhes e listar" });
            listaBanco.Add(new NivelAcesso { Name = "Basico", Descricao = "Tem acesso limitado de todas as funcionalidades do sistema, pode só ver a lista e detalhes de uma funcionalidade" });

            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Usuario>>();
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()), new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object);
            var _usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var SignInManagerMock = new SignInManager<Usuario>(new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object, _contextAccessor.Object, _userPrincipalFactory.Object, null, null, null);
            var loginNegocio = new LoginNegocio(new LoginRepositorio(SignInManagerMock),
                _usuarioRepositorioMock.Object,
                _nivelAcessoRepositorioMock.Object,
                _usuarioNivelAcessoRepositorioMock.Object);

            List<Usuario> listaUsuarios = new List<Usuario>();

            listaUsuarios.Add(new Usuario() { UserName = "principal" });

            _usuarioRepositorioMock.Setup<IQueryable<Usuario>>(u => u.ListarUsuarios()).Returns(listaUsuarios.AsQueryable());
            _nivelAcessoRepositorioMock.Setup(n => n.CriarNiveisAcesso());//simula função executada
            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(n => n.ListarNivelAcesso()).Returns(listaBanco);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(a => a.ListarUsuarioNivelAcesso()).Returns(new List<UsuarioNivelAcesso>());
            _usuarioNivelAcessoRepositorioMock.Setup(a => a.CriarPrimeiroUsuarioComNivelAcessoDoTipoAdministrador()).Returns(false);

            var retornoNegocio = loginNegocio.PrimeiroUsoDoSistema();

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao tentar Salvar/Editar um novo usuário do tipo 'Administrador' para tornar possível a utilização do sistema, entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        //A func CriarPrimeiroUsuarioComNivelAcessoDoTipoAdministrador deve retornar true
        public void PrimeiroUsoDoSistema_Func_CriarPrimeiroUsuarioComNivelAcessoDoTipoAdministrador_DeveRetornarTrue_Aviso()
        {
            List<NivelAcesso> listaBanco = new List<NivelAcesso>();
            listaBanco.Add(new NivelAcesso { Name = "Administrador", Descricao = "Tem acesso total de todas as funcionalidades do sistema, inclusive funcionalidades de gerenciamento de usuários, senhas, acessos" });
            listaBanco.Add(new NivelAcesso { Name = "Completo", Descricao = "Tem acesso total de todas as funcionalidades do sistema, como criar, editar, excluir, ver detalhes e listar" });
            listaBanco.Add(new NivelAcesso { Name = "Basico", Descricao = "Tem acesso limitado de todas as funcionalidades do sistema, pode só ver a lista e detalhes de uma funcionalidade" });

            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Usuario>>();
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()), new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object);
            var _usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var SignInManagerMock = new SignInManager<Usuario>(new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object, _contextAccessor.Object, _userPrincipalFactory.Object, null, null, null);
            var loginNegocio = new LoginNegocio(new LoginRepositorio(SignInManagerMock),
                _usuarioRepositorioMock.Object,
                _nivelAcessoRepositorioMock.Object,
                _usuarioNivelAcessoRepositorioMock.Object);

            List<Usuario> listaUsuarios = new List<Usuario>();

            listaUsuarios.Add(new Usuario() { UserName = "principal" });

            _usuarioRepositorioMock.Setup<IQueryable<Usuario>>(u => u.ListarUsuarios()).Returns(listaUsuarios.AsQueryable());
            _nivelAcessoRepositorioMock.Setup(n => n.CriarNiveisAcesso());//simula função executada
            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(n => n.ListarNivelAcesso()).Returns(listaBanco);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(a => a.ListarUsuarioNivelAcesso()).Returns(new List<UsuarioNivelAcesso>());
            _usuarioNivelAcessoRepositorioMock.Setup(a => a.CriarPrimeiroUsuarioComNivelAcessoDoTipoAdministrador()).Returns(true);

            var retornoNegocio = loginNegocio.PrimeiroUsoDoSistema();

            Assert.AreEqual(Tipo.Aviso, retornoNegocio.Tipo);
            Assert.AreEqual("***ATENÇÃO: Como é a primeira vez que o sistema está sendo utilizado, foi criado um usuário" +
                            " administrador para utilizar o sistema. \n\n O Login é  'adm' e a senha provisória é 'Aaa111*', que deve ser mudada para uma senha de sua escolha.", retornoNegocio.Mensagem);
        }

        [Test]
        //o sucesso significa que não é a primeira vez que o sistema está em uso ,pois já existe usuário com perfil administrador.
        public void PrimeiroUsoDoSistema_Sucesso()
        {
            List<NivelAcesso> listaBanco = new List<NivelAcesso>();
            listaBanco.Add(new NivelAcesso {Id = "fsdfsd000", Name = "Administrador", Descricao = "Tem acesso total de todas as funcionalidades do sistema, inclusive funcionalidades de gerenciamento de usuários, senhas, acessos" });
            listaBanco.Add(new NivelAcesso {Id = "fsdfsd111", Name = "Completo", Descricao = "Tem acesso total de todas as funcionalidades do sistema, como criar, editar, excluir, ver detalhes e listar" });
            listaBanco.Add(new NivelAcesso {Id = "fsdfsd222", Name = "Basico", Descricao = "Tem acesso limitado de todas as funcionalidades do sistema, pode só ver a lista e detalhes de uma funcionalidade" });

            List<UsuarioNivelAcesso> listaBancoUsuarioNivelAcesso = new List<UsuarioNivelAcesso>();
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "74Ret6", RoleId = "fsdfsd000" });

            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Usuario>>();
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDBContext>()), new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object);
            var _usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var SignInManagerMock = new SignInManager<Usuario>(new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null).Object, _contextAccessor.Object, _userPrincipalFactory.Object, null, null, null);
            var loginNegocio = new LoginNegocio(new LoginRepositorio(SignInManagerMock),
                _usuarioRepositorioMock.Object,
                _nivelAcessoRepositorioMock.Object,
                _usuarioNivelAcessoRepositorioMock.Object);

            List<Usuario> listaUsuarios = new List<Usuario>();

            listaUsuarios.Add(new Usuario() { UserName = "principal" });

            _usuarioRepositorioMock.Setup<IQueryable<Usuario>>(u => u.ListarUsuarios()).Returns(listaUsuarios.AsQueryable());
            _nivelAcessoRepositorioMock.Setup(n => n.CriarNiveisAcesso());//simula função executada
            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(n => n.ListarNivelAcesso()).Returns(listaBanco);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(a => a.ListarUsuarioNivelAcesso()).Returns(listaBancoUsuarioNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup(a => a.CriarPrimeiroUsuarioComNivelAcessoDoTipoAdministrador()).Returns(true);

            var retornoNegocio = loginNegocio.PrimeiroUsoDoSistema();

            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            
        }

        #endregion

    }
}
