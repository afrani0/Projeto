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
using System.Text;
using System.Threading.Tasks;

namespace TestesUnitarios
{
    [TestFixture]
    class PrimeiroAcessoTeste
    {

        #region ConcluirPrimeiroAcesso()

        [Test]
        public void ConcluirPrimeiroAcesso_IdDeveSerNullOuVazio_Erro()
        {
            UsuarioRepositorio _usuarioRepositorio = null;
            LoginRepositorio _loginRepositorio = null;
            var primeiroAcesso = new PrimeiroAcessoNegocio(_usuarioRepositorio, _loginRepositorio);

            var retornoPrimeiroAcesso = primeiroAcesso.ConcluirPrimeiroAcesso(null, null, null, null).Result;

            Assert.AreEqual(Tipo.Erro , retornoPrimeiroAcesso.Tipo);
            Assert.AreEqual("Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador", retornoPrimeiroAcesso.Mensagem);

            retornoPrimeiroAcesso = null;
            retornoPrimeiroAcesso = primeiroAcesso.ConcluirPrimeiroAcesso("", null, null, null).Result;

            Assert.AreEqual(Tipo.Erro, retornoPrimeiroAcesso.Tipo);
            Assert.AreEqual("Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador", retornoPrimeiroAcesso.Mensagem);
        }

        [Test]
        public void ConcluirPrimeiroAcesso_SenhaAtualDeveSerNullOuVazio_Erro()
        {
            UsuarioRepositorio _usuarioRepositorio = null;
            LoginRepositorio _loginRepositorio = null;
            var primeiroAcesso = new PrimeiroAcessoNegocio(_usuarioRepositorio, _loginRepositorio);

            var retornoPrimeiroAcesso = primeiroAcesso.ConcluirPrimeiroAcesso("fds5416", null, null, null).Result;

            Assert.AreEqual(Tipo.Erro, retornoPrimeiroAcesso.Tipo);
            Assert.AreEqual("Erro: Senha Atual não pode ser nulo ou vazio, entre em contato com o Administrador", retornoPrimeiroAcesso.Mensagem);

            retornoPrimeiroAcesso = null;
            retornoPrimeiroAcesso = primeiroAcesso.ConcluirPrimeiroAcesso("fds5416", "", null, null).Result;

            Assert.AreEqual(Tipo.Erro, retornoPrimeiroAcesso.Tipo);
            Assert.AreEqual("Erro: Senha Atual não pode ser nulo ou vazio, entre em contato com o Administrador", retornoPrimeiroAcesso.Mensagem);
        }

        [Test]
        public void ConcluirPrimeiroAcesso_NovaSenhaDeveSerNullOuVazio_Erro()
        {
            UsuarioRepositorio _usuarioRepositorio = null;
            LoginRepositorio _loginRepositorio = null;
            var primeiroAcesso = new PrimeiroAcessoNegocio(_usuarioRepositorio, _loginRepositorio);

            var retornoPrimeiroAcesso = primeiroAcesso.ConcluirPrimeiroAcesso("fds5416", "Abc123*", null, null).Result;

            Assert.AreEqual(Tipo.Erro, retornoPrimeiroAcesso.Tipo);
            Assert.AreEqual("Erro: Nova Senha não pode ser nulo ou vazio, entre em contato com o Administrador", retornoPrimeiroAcesso.Mensagem);

            retornoPrimeiroAcesso = null;
            retornoPrimeiroAcesso = primeiroAcesso.ConcluirPrimeiroAcesso("fds5416", "Abc123", "", null).Result;

            Assert.AreEqual(Tipo.Erro, retornoPrimeiroAcesso.Tipo);
            Assert.AreEqual("Erro: Nova Senha não pode ser nulo ou vazio, entre em contato com o Administrador", retornoPrimeiroAcesso.Mensagem);
        }

        [Test]
        public void ConcluirPrimeiroAcesso_ConfirmarNovaSenhaDeveSerNullOuVazio_Erro()
        {
            UsuarioRepositorio _usuarioRepositorio = null;
            LoginRepositorio _loginRepositorio = null;
            var primeiroAcesso = new PrimeiroAcessoNegocio(_usuarioRepositorio, _loginRepositorio);

            var retornoPrimeiroAcesso = primeiroAcesso.ConcluirPrimeiroAcesso("fds5416", "Abc123*", "Cba123*", null).Result;

            Assert.AreEqual(Tipo.Erro, retornoPrimeiroAcesso.Tipo);
            Assert.AreEqual("Erro: Confirmar Nova Senha não pode ser nulo ou vazio, entre em contato com o Administrador", retornoPrimeiroAcesso.Mensagem);

            retornoPrimeiroAcesso = null;
            retornoPrimeiroAcesso = primeiroAcesso.ConcluirPrimeiroAcesso("fds5416", "Abc123", "Cba123", "").Result;

            Assert.AreEqual(Tipo.Erro, retornoPrimeiroAcesso.Tipo);
            Assert.AreEqual("Erro: Confirmar Nova Senha não pode ser nulo ou vazio, entre em contato com o Administrador", retornoPrimeiroAcesso.Mensagem);
        }

        [Test]
        public void ConcluirPrimeiroAcesso_NovaSenhaEConfirmarNovaSenhaDevemSerDiferentes_Aviso()
        {
            UsuarioRepositorio _usuarioRepositorio = null;
            LoginRepositorio _loginRepositorio = null;
            var primeiroAcesso = new PrimeiroAcessoNegocio(_usuarioRepositorio, _loginRepositorio);

            var retornoPrimeiroAcesso = primeiroAcesso.ConcluirPrimeiroAcesso("fds5416", "Abc123*", "Cba123*", "Cba133*").Result;

            Assert.AreEqual(Tipo.Aviso, retornoPrimeiroAcesso.Tipo);
            Assert.AreEqual("Campos 'Nova Senha' e 'Confirmar Nova Senha' devem ser iguais", retornoPrimeiroAcesso.Mensagem);

        }

        [Test]
        public void ConcluirPrimeiroAcesso_NovaSenhaDeveSerIgualSenhaAtual_Aviso()
        {
            UsuarioRepositorio _usuarioRepositorio = null;
            LoginRepositorio _loginRepositorio = null;
            var primeiroAcesso = new PrimeiroAcessoNegocio(_usuarioRepositorio, _loginRepositorio);

            var retornoPrimeiroAcesso = primeiroAcesso.ConcluirPrimeiroAcesso("fds5416", "Cba123*", "Cba123*", "Cba123*").Result;

            Assert.AreEqual(Tipo.Aviso, retornoPrimeiroAcesso.Tipo);
            Assert.AreEqual("A 'Nova Senha' não pode ser igual a 'Senha Atual'", retornoPrimeiroAcesso.Mensagem);

        }

        //o retorno de usuário deve ser null
        [Test]
        public void ConcluirPrimeiroAcesso_Func_BuscarUsuarioPorId_DeveRetornarNull_Erro()
        {
            var _usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object ,null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            LoginRepositorio _loginRepositorio = null;

            var primeiroAcesso = new PrimeiroAcessoNegocio(_usuarioRepositorioMock.Object, _loginRepositorio);

            _usuarioRepositorioMock.Setup(u => u.BuscarUsuarioPorId(It.IsAny<string>()));

            var retornoPrimeiroAcesso = primeiroAcesso.ConcluirPrimeiroAcesso("fds5416", "Cba12A*", "Cba123*", "Cba123*").Result;

            Assert.AreEqual(Tipo.Erro, retornoPrimeiroAcesso.Tipo);
            Assert.AreEqual("Erro: Usuário não pode ser nulo, entre em contato com o Administrador", retornoPrimeiroAcesso.Mensagem);

        }

        // função MudarSenha deve retornar IdentityResult com propriedade 'Succeeded' igual a false e ter o campo 'errors' diferente de null
        [Test]
        public void CriarUsuario_Fun_MudarSenha_DeveRetornarIdentityResultFalseEDiferenteNull_Erro()
        {
            var _usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            LoginRepositorio _loginRepositorio = null;

            var primeiroAcesso = new PrimeiroAcessoNegocio(_usuarioRepositorioMock.Object, _loginRepositorio);
            var usuarioRetornado = new Usuario() { Id = "fsd61sd6f51", Nome = "DJ" };
            _usuarioRepositorioMock.Setup(u => u.BuscarUsuarioPorId(It.IsAny<string>())).Returns(Task.FromResult(usuarioRetornado));
            _usuarioRepositorioMock.Setup<Task<IdentityResult>>(u => u.MudarSenha(usuarioRetornado , It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new IdentityResult());//usando 'new IdentityResult()' retorna false;
            

            var retornoPrimeiroAcesso = primeiroAcesso.ConcluirPrimeiroAcesso("fds5416", "Cba12A*", "Cba123*", "Cba123*").Result;

            Assert.AreEqual(Tipo.Erro, retornoPrimeiroAcesso.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao tentar modificar a senha do usuário, entre em contato com o Administrador.", retornoPrimeiroAcesso.Mensagem);
            Assert.IsNotNull(retornoPrimeiroAcesso.Erros);
        }



        // função EditarUsuario deve retornar IdentityResult com propriedade 'Succeeded' igual a false
        [Test]
        public void CriarUsuario_Fun_EditarUsuario_DeveRetornarIdentityResultFalseEDiferenteNull_Erro()
        {

            var _usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            LoginRepositorio _loginRepositorio = null;

            var primeiroAcesso = new PrimeiroAcessoNegocio(_usuarioRepositorioMock.Object, _loginRepositorio);
            var usuarioRetornado = new Usuario() { Id = "fsd61sd6f51", Nome = "DJ" };
            _usuarioRepositorioMock.Setup(u => u.BuscarUsuarioPorId(It.IsAny<string>())).Returns(Task.FromResult(usuarioRetornado));
            _usuarioRepositorioMock.Setup<Task<IdentityResult>>(u => u.MudarSenha(usuarioRetornado, It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);//usando 'IdentityResult.Success' retorna true;
            _usuarioRepositorioMock.Setup<Task<IdentityResult>>(u => u.EditarUsuario(It.IsAny<Usuario>())).ReturnsAsync(new IdentityResult());


            var retornoPrimeiroAcesso = primeiroAcesso.ConcluirPrimeiroAcesso("fds5416", "Cba12A*", "Cba123*", "Cba123*").Result;

            Assert.AreEqual(Tipo.Erro, retornoPrimeiroAcesso.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao tentar modificar o estado do Login para deixar de ser primeiro acesso, entre em contato com o Administrador.", retornoPrimeiroAcesso.Mensagem);
        }

        [Test]
        public void CriarUsuario_Sucesso()
        {
            var _usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _loginRepositorioMock = new Mock<LoginRepositorio>(new SignInManager<Usuario>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new Mock<IHttpContextAccessor>().Object, new Mock<IUserClaimsPrincipalFactory<Usuario>>().Object, null, null, null));

            var primeiroAcesso = new PrimeiroAcessoNegocio(_usuarioRepositorioMock.Object, _loginRepositorioMock.Object);
            var usuarioRetornado = new Usuario() { Id = "fsd61sd6f51", Nome = "DJ" };
            _usuarioRepositorioMock.Setup(u => u.BuscarUsuarioPorId(It.IsAny<string>())).Returns(Task.FromResult(usuarioRetornado));
            _usuarioRepositorioMock.Setup<Task<IdentityResult>>(u => u.MudarSenha(usuarioRetornado, It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);//usando 'IdentityResult.Success' retorna true;
            _usuarioRepositorioMock.Setup<Task<IdentityResult>>(u => u.EditarUsuario(It.IsAny<Usuario>())).ReturnsAsync(IdentityResult.Success);
        
            _loginRepositorioMock.Setup<Task>(l => l.Logout());

            var retornoPrimeiroAcesso = primeiroAcesso.ConcluirPrimeiroAcesso("fds5416", "Cba12A*", "Cba123*", "Cba123*").Result;

            Assert.AreEqual(Tipo.Sucesso, retornoPrimeiroAcesso.Tipo);
            Assert.AreEqual("Senha modificada com sucesso.", retornoPrimeiroAcesso.Mensagem);
            Assert.IsNull(retornoPrimeiroAcesso.Erros);
        }

        #endregion
    }
}
