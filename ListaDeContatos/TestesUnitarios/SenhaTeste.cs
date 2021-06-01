using ListaDeContatos.Enumerador;
using ListaDeContatos.Models;
using ListaDeContatos.Negocio.Implementacao;
using ListaDeContatos.Repositorio.Implementacao;
using ListaDeContatos.Util;
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
    class SenhaTeste
    {

        //Criar Nova Senha Para determinado usuário

        /*
         *
         - Id de usuário não pode ser null e nem vazio
         - Id não pode ser igual a zero
         - func BuscarUsuarioPorId não pode ser null
         - func GeraSenhaAleatoria não pode retornar nova senha vazia ou nula

         - func MudarSenhaUsandoPerfilAdministrador deve salvar a edição do 'Usuário' com nova senha e modificar com o parâmetro 'PrimeiroAcesso' para 'true/1'
        */

        [Test]
        //parâmetro Id de usuário deve ser null ou vazio
        public void NovaSenha_IdDeveSerNullOuVazio_Erro()
        {
            string UsuarioId = null;

            var usuarioRepositorio = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);

            var _senhaNegocio = new SenhaNegocio(new UsuarioRepositorio(usuarioRepositorio, new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())), new SenhaUtil());

            var retornoNegocio = _senhaNegocio.NovaSenha(UsuarioId).Result;

            Assert.AreEqual(Tipo.Erro , retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador" , retornoNegocio.Mensagem);

            UsuarioId = "";

            retornoNegocio = null;
            retornoNegocio = _senhaNegocio.NovaSenha(UsuarioId).Result;

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        //parâmetro Id de usuário deve ser igual a zero
        public void NovaSenha_IdDeveSerZero_Erro()
        {
            string UsuarioId = "0";

            var usuarioRepositorio = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);

            var _senhaNegocio = new SenhaNegocio(new UsuarioRepositorio(usuarioRepositorio, new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())), new SenhaUtil());

            var retornoNegocio = _senhaNegocio.NovaSenha(UsuarioId).Result;

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Id não pode ser zero, entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        //func BuscarUsuarioPorId deve ser null
        public void NovaSenha_Fun_BuscarUsuarioPorId_DeveRetornarNull_Erro()
        {
            var listaUsuariosBanco = new List<Usuario>();
            listaUsuariosBanco.Add(new Usuario() { Id = "24" });
            listaUsuariosBanco.Add(new Usuario() { Id = "724" });

            string UsuarioId = "846";

            var _usuarioRepositorio = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var _senhaNegocio = new SenhaNegocio(_usuarioRepositorio.Object, new SenhaUtil());
            _usuarioRepositorio.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(UsuarioId)).Returns( Task.FromResult(listaUsuariosBanco.Find(l => l.Id == UsuarioId)) );

            var retornoNegocio = _senhaNegocio.NovaSenha(UsuarioId).Result;

            Assert.AreEqual(Tipo.Aviso, retornoNegocio.Tipo);
            Assert.AreEqual("Aviso: O usuário selecionado não foi encontrado no sistema, pode ser que tenha sido excluído, caso o erro persista entre em contato com o administrador", retornoNegocio.Mensagem);
        }

        [Test]
        // func GeraSenhaAleatoria deve retornar string vazia ou nula
        public void NovaSenha_Fun_GeraSenhaAleatoria_DeveRetornarSenhaVazioOuNula_Erro()
        {
            var listaUsuariosBanco = new List<Usuario>();
            listaUsuariosBanco.Add(new Usuario() { Id = "24" });
            listaUsuariosBanco.Add(new Usuario() { Id = "724" });

            string UsuarioId = "724";

            var _usuarioRepositorio = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var _senhaUtil = new Mock<SenhaUtil>();
            var _senhaNegocio = new SenhaNegocio(_usuarioRepositorio.Object, _senhaUtil.Object);
            _usuarioRepositorio.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(UsuarioId)).Returns(Task.FromResult(listaUsuariosBanco.Find(l => l.Id == UsuarioId)));
            _senhaUtil.Setup<string>(s => s.GeraSenhaAleatoria()).Returns("");

            var retornoNegocio = _senhaNegocio.NovaSenha(UsuarioId).Result;

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Não foi possível criar nova senha para usuário selecionado, entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        //fun MudarSenhaUsandoPerfilAdministrador deve retornar IdentityResultFalse.
        public void NovaSenha_Fun_MudarSenhaUsandoPerfilAdministrador_DeveRetornar_Erro()
        {
            var listaUsuariosBanco = new List<Usuario>();
            listaUsuariosBanco.Add(new Usuario() { Id = "24" });
            listaUsuariosBanco.Add(new Usuario() { Id = "724" });

            string UsuarioId = "724";

            var _usuarioRepositorio = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var _senhaUtil = new Mock<SenhaUtil>();
            var _senhaNegocio = new SenhaNegocio(_usuarioRepositorio.Object, _senhaUtil.Object);
            _usuarioRepositorio.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(UsuarioId)).Returns(Task.FromResult(listaUsuariosBanco.Find(l => l.Id == UsuarioId)));
            _senhaUtil.Setup<string>(s => s.GeraSenhaAleatoria()).Returns("5Ui*ppo");
            _usuarioRepositorio.Setup<Task<IdentityResult>>(u => u.MudarSenhaUsandoPerfilAdministrador(It.Is<Usuario>(i => i.Id == UsuarioId), "5Ui*ppo")).ReturnsAsync(new IdentityResult());//retornar IdentityResult igual a false

            var retornoNegocio = _senhaNegocio.NovaSenha(UsuarioId).Result;

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao tentar modificar a senha, entre em contato com o Adminstrador", retornoNegocio.Mensagem);
        }

        [Test]
        public void NovaSenha_Sucesso()
        {
            var listaUsuariosBanco = new List<Usuario>();
            listaUsuariosBanco.Add(new Usuario() { Id = "24" });
            listaUsuariosBanco.Add(new Usuario() { Id = "724" });

            string UsuarioId = "724";

            var _usuarioRepositorio = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var _senhaUtil = new Mock<SenhaUtil>();
            var _senhaNegocio = new SenhaNegocio(_usuarioRepositorio.Object, _senhaUtil.Object);
            _usuarioRepositorio.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(UsuarioId)).Returns(Task.FromResult(listaUsuariosBanco.Find(l => l.Id == UsuarioId)));
            _senhaUtil.Setup<string>(s => s.GeraSenhaAleatoria()).Returns("5Ui*ppo");
            _usuarioRepositorio.Setup<Task<IdentityResult>>(u => u.MudarSenhaUsandoPerfilAdministrador(It.Is<Usuario>(i => i.Id == UsuarioId), "5Ui*ppo")).ReturnsAsync(IdentityResult.Success);//retornar IdentityResult igual a true

            var retornoNegocio = _senhaNegocio.NovaSenha(UsuarioId).Result;

            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual("Nova Senha criada com sucesso", retornoNegocio.Mensagem);
            Assert.AreEqual("5Ui*ppo", retornoNegocio.Objeto);
        }


    }
}
