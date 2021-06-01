using ListaDeContatos.Enumerador;
using ListaDeContatos.Models;
using ListaDeContatos.Negocio.Implementacao;
using ListaDeContatos.Repositorio.Implementacao;
using ListaDeContatos.Repositorio.Interface;
using ListaDeContatos.Util;
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
    public class RegistroTeste
    {
        private object respostaNegocio;
        #region ListarUsuarios()
        [Test]
        //Função ListarUsuario deve retornar null
        public void ListarUsuarios_Fun_ListarUsuariosDeveSerNull_Erro()
        {
            var UserStoreMock = Mock.Of<IUserStore<Usuario>>();
            var mockUsuarioRepositorio = new Mock<UsuarioRepositorio>(
                new UserManager<Usuario>(UserStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var registroTeste = new RegistroNegocio(mockUsuarioRepositorio.Object);

            //var lista = new List<Usuario>();

            //lista.Add(new Usuario() { Nome = "Ana" });
            //lista.Add(new Usuario() { Nome = "Sintia" });

            //var listaIQueriable = lista.AsQueryable();

            IQueryable<Usuario> listaVazia = null;

            mockUsuarioRepositorio.Setup<IQueryable<Usuario>>(l => l.ListarUsuarios()).Returns(listaVazia);

            var retornoLista = registroTeste.ListarUsuarios().Objeto;

            Assert.IsNull(retornoLista);
        }

        [Test]
        public void ListarUsuarios_sucesso()
        {
            var UserStoreMock = Mock.Of<IUserStore<Usuario>>();
            var mockUsuarioRepositorio = new Mock<UsuarioRepositorio>(
                new UserManager<Usuario>(UserStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var registroTeste = new RegistroNegocio(mockUsuarioRepositorio.Object);

            var lista = new List<Usuario>();

            lista.Add(new Usuario() { Nome = "Ana" });
            lista.Add(new Usuario() { Nome = "Sintia" });

            var listaIQueriable = lista.AsQueryable();

            mockUsuarioRepositorio.Setup<IQueryable<Usuario>>(l => l.ListarUsuarios()).Returns(listaIQueriable);

            var retornoLista = ((IQueryable<Usuario>)registroTeste.ListarUsuarios().Objeto).ToList();

            Assert.That(() => registroTeste.ListarUsuarios(), Throws.Nothing);
            Assert.Contains("Ana", retornoLista.Select(n => n.Nome).ToList());
            Assert.Contains("Sintia", retornoLista.Select(n => n.Nome).ToList());
            Assert.AreEqual(2, ((IQueryable<Usuario>)registroTeste.ListarUsuarios().Objeto).Count());
        }

        #endregion

        #region BuscarUsuarioPorId(string Id)
        [Test]
        public void BuscarUsuarioPorId_IdDeveSerNuloOuVazio_Erro()
        {
            string id = "";

            var userStoreMock = Mock.Of<IUserStore<Usuario>>();
            var registroNegocio = new RegistroNegocio(new UsuarioRepositorio(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            var retorno = registroNegocio.BuscarUsuarioPorId(id).Result;
            Assert.AreEqual(ListaDeContatos.Enumerador.Tipo.Erro, retorno.Tipo);            
            Assert.AreEqual("Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador", retorno.Mensagem);

            id = null;
            retorno = null;
            retorno = registroNegocio.BuscarUsuarioPorId(id).Result;
            Assert.AreEqual(ListaDeContatos.Enumerador.Tipo.Erro, retorno.Tipo);
            Assert.AreEqual("Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador", retorno.Mensagem);
        }

        [Test]
        public void BuscarUsuarioPorId_IdDeveSerZero_Erro()
        {
            string id = "0";

            var userStoreMock = Mock.Of<IUserStore<Usuario>>();
            var registroNegocio = new RegistroNegocio(new UsuarioRepositorio(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            var retorno = registroNegocio.BuscarUsuarioPorId(id).Result;
            Assert.AreEqual(ListaDeContatos.Enumerador.Tipo.Erro, retorno.Tipo);
            Assert.AreEqual("Erro: Id não pode ser zero, entre em contato com o Administrador", retorno.Mensagem);

            //Assert.That(async () => await registroNegocio.BuscarUsuarioPorId(id), new RespostaNegocio());
            //Assert.Throws<Exception>(() => registroNegocio.BuscarUsuarioPorId(id));
        }

        [Test]
        public void BuscarUsuarioPorId_Aviso()
        {
            string id = "00Fdpp52aop";

            List<Usuario> lista = new List<Usuario>();

            lista.Add(new Usuario() { Id = "00Fdpp52as" });
            lista.Add(new Usuario() { Id = "7Wfdpp52as" });

            var userStoreMock = Mock.Of<IUserStore<Usuario>>();
            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);


            //Quando precisar fazer mock de um método assincrono, em 'Returns' deve utilizar o método 'Task.FromResult()' e no 'Setup' não inserir nada 
            usuarioRepositorioMock.Setup(u => u.BuscarUsuarioPorId(id)).Returns(Task.FromResult(lista.Where(l => l.Id.Contains(id)).FirstOrDefault()));

            var retorno = registroNegocio.BuscarUsuarioPorId(id).Result;
            Assert.AreEqual("Aviso: Não foi encontrado nenhum usuário com o id especificado, entre em contato com o Administrador", retorno.Mensagem);
            Assert.AreEqual(ListaDeContatos.Enumerador.Tipo.Aviso, retorno.Tipo);
            Assert.IsFalse(string.IsNullOrEmpty(retorno.Mensagem));
            Assert.AreEqual(null, ((Usuario)retorno.Objeto));

        }

        [Test]
        public void BuscarUsuarioPorId_SucessoAsync()
        {
            string id = "00Fdpp52as";

            List<Usuario> lista = new List<Usuario>();

            lista.Add(new Usuario() { Id = "00Fdpp52as" });
            lista.Add(new Usuario() { Id = "7Wfdpp52as" });

            var userStoreMock = Mock.Of<IUserStore<Usuario>>();
            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);


            //Quando precisar fazer mock de um método assincrono, em 'Returns' deve utilizar o método 'Task.FromResult()' e no 'Setup' não inserir nada 
            usuarioRepositorioMock.Setup(u => u.BuscarUsuarioPorId(id)).Returns(Task.FromResult(lista.Where(l => l.Id.Contains(id)).FirstOrDefault()));

            var retorno = registroNegocio.BuscarUsuarioPorId(id).Result;
            Assert.AreEqual(ListaDeContatos.Enumerador.Tipo.Sucesso, retorno.Tipo);
            Assert.IsTrue(string.IsNullOrEmpty(retorno.Mensagem));
            Assert.AreEqual(new Usuario() { Id = "00Fdpp52as" }.Id, ((Usuario)retorno.Objeto).Id);

            //Assert.That(async () => await registroNegocio.BuscarUsuarioPorId(id), new RespostaNegocio());
            //Assert.Throws<Exception>(() => registroNegocio.BuscarUsuarioPorId(id));
        }

        #endregion DeletarUsuario(Usuario usuario)

        #region DeletarUsuario(Usuario usuario)
        [Test]
        //parâmetro usuário deve ser null
        public void DeletarUsuario_UsuarioDeveSerNull_Erro()
        {
            Usuario usuario = null;
            var userStoreMock = Mock.Of<IUserStore<Usuario>>();

            var usuarioRepositorio = new UsuarioRepositorio(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var registroNegocio = new RegistroNegocio(usuarioRepositorio);

            var resultadoUsuario = registroNegocio.DeletarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Erro, resultadoUsuario.Tipo);
            Assert.AreEqual("Erro: O Usuário não pode ser nulo, entre em contato com o Administrador", resultadoUsuario.Mensagem);
        }

        [Test]
        //parâmetro id do usuário deve ser null ou vazio
        public void DeletarUsuario_UsuarioIdDeveSerNull_Erro()
        {
            Usuario usuario = new Usuario() { Id = "" };
            var userStoreMock = Mock.Of<IUserStore<Usuario>>();

            var usuarioRepositorio = new UsuarioRepositorio(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var registroNegocio = new RegistroNegocio(usuarioRepositorio);

            var resultadoUsuario = registroNegocio.DeletarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Erro, resultadoUsuario.Tipo);
            Assert.AreEqual("Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador", resultadoUsuario.Mensagem);

            usuario.Id = null;
            resultadoUsuario = null;
            resultadoUsuario = registroNegocio.DeletarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Erro, resultadoUsuario.Tipo);
            Assert.AreEqual("Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador", resultadoUsuario.Mensagem);
        }

        [Test]
        //função BuscarUsuarioPorId do repositório deve retornar null
        public void DeletarUsuario_Func_BuscarUsuarioPorId_DeveRetornarNull_Erro()
        {
            //lista de usuários no banco
            List<Usuario> lista = new List<Usuario>();
            lista.Add(new Usuario() { Id = "u7iop73" });
            lista.Add(new Usuario() { Id = "u7iop75" });

            //var responseTask = System.Threading.Tasks.Task.FromResult(new IdentityError());
            var identityResult = new IdentityResult();
            //identityResult.Errors = new IdentityError();


            Usuario usuario = new Usuario() { Id = "u7iop74" };
            var userStoreMock = Mock.Of<IUserStore<Usuario>>();

            var usuarioRepositorio = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var registroNegocio = new RegistroNegocio(usuarioRepositorio.Object);

            usuarioRepositorio.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(usuario.Id)).ReturnsAsync(lista.Where(l => l.Id.Contains(usuario.Id)).FirstOrDefault());

            //usuarioRepositorio.Setup(u => u.DeletarUsuario(usuario)).ReturnsAsync(identityResult);

            var resultadoUsuario = registroNegocio.DeletarUsuario(usuario).Result;


            Assert.AreEqual(Tipo.Aviso, resultadoUsuario.Tipo);
            Assert.AreEqual("Aviso: O usuário pode ter sido excluído anteriormente ou não existe no sistema", resultadoUsuario.Mensagem);
        }

        [Test]
        //função BuscarRolesPorUsuario do repositório deve retornar null
        public void DeletarUsuario_Func_BuscarRolesPorUsuario_DeveRetornarNull_Erro()
        {
            //lista de usuários no banco
            List<Usuario> lista = new List<Usuario>();
            lista.Add(new Usuario() { Id = "u7iop73" });
            lista.Add(new Usuario() { Id = "u7iop74" });

            //var responseTask = System.Threading.Tasks.Task.FromResult(new IdentityError());
            var identityResult = new IdentityResult();
            //identityResult.Errors = new IdentityError();


            Usuario usuario = new Usuario() { Id = "u7iop74" };
            var userStoreMock = Mock.Of<IUserStore<Usuario>>();

            var usuarioRepositorio = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var registroNegocio = new RegistroNegocio(usuarioRepositorio.Object);

            usuarioRepositorio.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(usuario.Id)).ReturnsAsync(lista.Where(l => l.Id.Contains(usuario.Id)).FirstOrDefault());
            usuarioRepositorio.Setup(u => u.BuscarRolesPorUsuario(usuario));

            var resultadoUsuario = registroNegocio.DeletarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Erro, resultadoUsuario.Tipo);
            Assert.AreEqual("Erro: Não foi possível excluir usuário, porque antes ocorreu um erro ao tentar buscar a permissão de acesso do usuário, entre em contato com o Administrador", resultadoUsuario.Mensagem);
        }

        [Test]
        // A lista retornada da func BuscarRolesPorUsuario deve ser maior que zero e conter a Role 'Administrador' e
        // a função BuscarUsuarioPorRole do repositório deve retornar null
        public void DeletarUsuario_Func_BuscarUsuarioPorRole_DeveRetornarNull_Erro()
        {
            //lista de usuários no banco
            List<Usuario> lista = new List<Usuario>();
            lista.Add(new Usuario() { Id = "u7iop73" });
            Usuario usuario = new Usuario() { Id = "u7iop74" };
            lista.Add(usuario);

            //lista de roles no banco
            List<string> listaRoles = new List<string>();
            listaRoles.Add("Administrador");
            listaRoles.Add("Básico");

            //var responseTask = System.Threading.Tasks.Task.FromResult(new IdentityError());
            var identityResult = new IdentityResult();
            //identityResult.Errors = new IdentityError();


            var userStoreMock = Mock.Of<IUserStore<Usuario>>();

            var usuarioRepositorio = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var registroNegocio = new RegistroNegocio(usuarioRepositorio.Object);

            usuarioRepositorio.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(usuario.Id)).ReturnsAsync(lista.Where(l => l.Id.Contains(usuario.Id)).FirstOrDefault());
            usuarioRepositorio.Setup<Task<IList<string>>>(u => u.BuscarRolesPorUsuario(usuario)).ReturnsAsync(listaRoles);


            usuarioRepositorio.Setup(u => u.BuscarUsuarioPorRole("Administrador"));


            var resultadoUsuario = registroNegocio.DeletarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Erro, resultadoUsuario.Tipo);
            Assert.AreEqual("Erro: Não foi possível excluir usuário, porque antes ocorreu um erro ao tentar buscar usuário para a permissão 'Administrador', entre em contato com o Administrador", resultadoUsuario.Mensagem);
        }

        [Test]
        // A função BuscarUsuarioPorRole do repositório deve retornar uma Lista de Usuarios com 1 ou 0 usuários.
        public void DeletarUsuario_Func_BuscarUsuarioPorRole_DeveRetornarListUsuarioMenorIgual1_Erro()
        {
            //lista de usuários no banco
            IList<Usuario> lista = new List<Usuario>();
            Usuario usuario = new Usuario() { Id = "u7iop74", Nome = "Regina", UserName = "Reg" };
            lista.Add(usuario);
            lista.Add(new Usuario() { Id = "u7iop77", Nome = "Sandra", UserName = "Sand" });


            //lista de roles no banco
            List<string> listaRoles = new List<string>();
            listaRoles.Add("Administrador");
            listaRoles.Add("Básico");

            //var responseTask = System.Threading.Tasks.Task.FromResult(new IdentityError());
            var identityResult = new IdentityResult();
            //identityResult.Errors = new IdentityError();

            var userStoreMock = Mock.Of<IUserStore<Usuario>>();

            var usuarioRepositorio = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var registroNegocio = new RegistroNegocio(usuarioRepositorio.Object);

            usuarioRepositorio.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(usuario.Id)).ReturnsAsync(lista.Where(l => l.Id.Contains(usuario.Id)).FirstOrDefault());
            usuarioRepositorio.Setup<Task<IList<string>>>(u => u.BuscarRolesPorUsuario(usuario)).ReturnsAsync(listaRoles);

            IList<Usuario> listaComUmUsuario = lista.Where(l => l.Nome.Contains("Regina")).ToList();
            usuarioRepositorio.Setup<Task<IList<Usuario>>>(u => u.BuscarUsuarioPorRole("Administrador")).Returns(Task.FromResult(listaComUmUsuario));


            var resultadoUsuario = registroNegocio.DeletarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Aviso, resultadoUsuario.Tipo);
            Assert.AreEqual("Atenção: Não é permitido exclusão do usuário ' " + usuario.UserName + " '. O sistema não pode ficar sem ao menos 1 usuário com permissão 'Administrador'", resultadoUsuario.Mensagem);
        }

        [Test]
        // A função DeletarUsuario do repositório deve retornar IdentityResult.Success com valor false.
        public void DeletarUsuario_Func_DeletarUsuario_DeveRetornarIdentityResultFalse_Erro()
        {
            //lista de usuários no banco
            IList<Usuario> lista = new List<Usuario>();
            Usuario usuario = new Usuario() { Id = "u7iop74", Nome = "Regina", UserName = "Reg" };
            lista.Add(usuario);
            lista.Add(new Usuario() { Id = "u7iop77", Nome = "Sandra", UserName = "Sand" });


            //lista de roles no banco
            List<string> listaRoles = new List<string>();
            listaRoles.Add("Administrador");
            listaRoles.Add("Básico");

            var userStoreMock = Mock.Of<IUserStore<Usuario>>();

            var usuarioRepositorio = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var registroNegocio = new RegistroNegocio(usuarioRepositorio.Object);

            usuarioRepositorio.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(usuario.Id)).ReturnsAsync(lista.Where(l => l.Id.Contains(usuario.Id)).FirstOrDefault());
            usuarioRepositorio.Setup<Task<IList<string>>>(u => u.BuscarRolesPorUsuario(usuario)).ReturnsAsync(listaRoles);
            usuarioRepositorio.Setup<Task<IList<Usuario>>>(u => u.BuscarUsuarioPorRole("Administrador")).Returns(Task.FromResult(lista));


            //para simular o retorno de identityresult com Succeeded igual a true, deve ser feito da forma abaixo
            //var responseTask = System.Threading.Tasks.Task.FromResult(IdentityResult.Success);
            //usuarioRepositorio.Setup(u => u.RemoverRolesPorUsuario(usuario, listaRoles)).Returns(Task.FromResult(IdentityResult.Success));
            //var identityResult = new IdentityResult();
            //identityresult com Succeeded igual a false
            usuarioRepositorio.Setup<bool>(u => u.DeletarUsuario(usuario, listaRoles)).Returns(false);

            var resultadoUsuario = registroNegocio.DeletarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Erro, resultadoUsuario.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao tentar excluir o usuário, entre em contato com o Administrador", resultadoUsuario.Mensagem);
        }

        [Test]
        public void DeletarUsuario_Sucesso()
        {
            //lista de usuários no banco
            IList<Usuario> lista = new List<Usuario>();
            Usuario usuario = new Usuario() { Id = "u7iop74", Nome = "Regina", UserName = "Reg" };
            lista.Add(usuario);
            lista.Add(new Usuario() { Id = "u7iop77", Nome = "Sandra", UserName = "Sand" });


            //lista de roles no banco
            List<string> listaRoles = new List<string>();
            listaRoles.Add("Administrador");
            listaRoles.Add("Básico");

            var userStoreMock = Mock.Of<IUserStore<Usuario>>();

            var usuarioRepositorio = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var registroNegocio = new RegistroNegocio(usuarioRepositorio.Object);

            usuarioRepositorio.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(usuario.Id)).ReturnsAsync(lista.Where(l => l.Id.Contains(usuario.Id)).FirstOrDefault());
            usuarioRepositorio.Setup<Task<IList<string>>>(u => u.BuscarRolesPorUsuario(usuario)).ReturnsAsync(listaRoles);
            usuarioRepositorio.Setup<Task<IList<Usuario>>>(u => u.BuscarUsuarioPorRole("Administrador")).Returns(Task.FromResult(lista));


            //para simular o retorno de identityresult com Succeeded igual a true, deve ser feito da forma abaixo            
            //usuarioRepositorio.Setup(u => u.RemoverRolesPorUsuario(usuario, listaRoles)).Returns(Task.FromResult(IdentityResult.Success));
            //var identityResult = new IdentityResult();
            //identityresult com Succeeded igual a true
            usuarioRepositorio.Setup(u => u.DeletarUsuario(usuario, listaRoles)).Returns(true);

            var resultadoUsuario = registroNegocio.DeletarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Sucesso, resultadoUsuario.Tipo);
            Assert.AreEqual("Usuário excluído com sucesso", resultadoUsuario.Mensagem);
        }


        #endregion

        #region BuscarRolesPorUsuario(Usuario usuario)

        //parâmetro usuário deve ser null
        [Test]
        public void BuscarRolesPorUsuario_UsuarioDeveSerNull_Erro()
        {
            Usuario usuario = null;
            var userStoreMock = Mock.Of<IUserStore<Usuario>>();
            var registroNegocio = new RegistroNegocio(new UsuarioRepositorio(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            var retorno = registroNegocio.BuscarRolesPorUsuario(usuario).Result;
            Assert.AreEqual(ListaDeContatos.Enumerador.Tipo.Erro, retorno.Tipo);
            Assert.AreEqual("Erro: Usuário não pode ser nulo, entre em contato com o Administrador", retorno.Mensagem);

        }

        //parâmetro usuário deve ser null
        [Test]
        public void BuscarRolesPorUsuario_UsuarioIdDeveSerNull_Erro()
        {
            Usuario usuario = new Usuario() { Id = null };
            var userStoreMock = Mock.Of<IUserStore<Usuario>>();
            var registroNegocio = new RegistroNegocio(new UsuarioRepositorio(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            var retorno = registroNegocio.BuscarRolesPorUsuario(usuario).Result;
            Assert.AreEqual(ListaDeContatos.Enumerador.Tipo.Erro, retorno.Tipo);
            Assert.AreEqual("Erro: Id do Usuário não pode ser nulo ou vazio, entre em contato com o Administrador", retorno.Mensagem);

            usuario.Id = "";

            retorno = registroNegocio.BuscarRolesPorUsuario(usuario).Result;
            Assert.AreEqual(ListaDeContatos.Enumerador.Tipo.Erro, retorno.Tipo);
            Assert.AreEqual("Erro: Id do Usuário não pode ser nulo ou vazio, entre em contato com o Administrador", retorno.Mensagem);

        }

        // a Funcão BuscarUsuarioPorId deve retornar null
        [Test]
        public void BuscarRolesPorUsuario_Func_BuscarUsuarioPorId_DeveSerNull_Aviso()
        {
            Usuario usuario = new Usuario() { Id = "4df1df4" };
            var userStoreMock = Mock.Of<IUserStore<Usuario>>();
            var usuarioRepositorio = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var registroNegocio = new RegistroNegocio(usuarioRepositorio.Object);

            usuarioRepositorio.Setup(u => u.BuscarUsuarioPorId(usuario.Id));

            var retorno = registroNegocio.BuscarRolesPorUsuario(usuario).Result;
            Assert.AreEqual(ListaDeContatos.Enumerador.Tipo.Aviso, retorno.Tipo);
            Assert.AreEqual("Aviso: O usuário pode ter sido excluído anteriormente ou não existe no sistema", retorno.Mensagem);

        }

        // a Funcão BuscarRolesPorUsuario deve retornar null
        [Test]
        public void BuscarRolesPorUsuario_Func_BuscarRolesPorUsuario_DeveSerNull_Erro()
        {
            Usuario usuario = new Usuario() { Id = "4df1df4" };
            var userStoreMock = Mock.Of<IUserStore<Usuario>>();
            var usuarioRepositorio = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var registroNegocio = new RegistroNegocio(usuarioRepositorio.Object);

            usuarioRepositorio.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(usuario.Id)).Returns(Task.FromResult(usuario));
            usuarioRepositorio.Setup(u => u.BuscarRolesPorUsuario(usuario));

            var retorno = registroNegocio.BuscarRolesPorUsuario(usuario).Result;
            Assert.AreEqual(ListaDeContatos.Enumerador.Tipo.Erro, retorno.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao buscar as permissões do usuário, entre em contato com o Administrador", retorno.Mensagem);

        }

        [Test]
        public void BuscarRolesPorUsuario_Sucesso()
        {
            Usuario usuario = new Usuario() { Id = "4df1df4" };
            var userStoreMock = Mock.Of<IUserStore<Usuario>>();
            var usuarioRepositorio = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(userStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var registroNegocio = new RegistroNegocio(usuarioRepositorio.Object);

            usuarioRepositorio.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(usuario.Id)).Returns(Task.FromResult(usuario));
            var lista = new List<string>();
            lista.Add("Administrador");
            lista.Add("Basico");
            var listaRoles = (IList<string>)lista;

            usuarioRepositorio.Setup<Task<IList<string>>>(u => u.BuscarRolesPorUsuario(usuario)).ReturnsAsync(listaRoles);

            var retorno = registroNegocio.BuscarRolesPorUsuario(usuario).Result;
            Assert.AreEqual(ListaDeContatos.Enumerador.Tipo.Sucesso, retorno.Tipo);
            Assert.AreEqual(2, ((List<string>)retorno.Objeto).Count);
            Assert.Contains("Basico", ((List<string>)retorno.Objeto));
            Assert.Contains("Administrador", ((List<string>)retorno.Objeto));
            Assert.IsFalse(((List<string>)retorno.Objeto).Contains("Completo"));
            //Assert.IsTrue(((List<string>)retorno.Objeto).Contains("Administrador"));

        }

        #endregion

        #region CriarUsuario(Usuario usuario, string senha)
        [Test]
        // usuário deve ser null
        public void CriarUsuario_UsuarioDeveSerNull_Erro()
        {
            Usuario usuario = null;
            var senha = "";

            var UserStoreMock = Mock.Of<IUserStore<Usuario>>();
            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(UserStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);

            //usuarioRepositorioMock.Setup(c => c.CriarUsuario(usuario, senha));

            var retornoResposta = registroNegocio.CriarUsuario(usuario, senha).Result;

            Assert.AreEqual(Tipo.Erro, retornoResposta.Tipo);
            Assert.AreEqual("Erro: Usuário não pode ser nulo, entre em contato com o Administrador", retornoResposta.Mensagem);
        }

        [Test]
        // senha deve ser null ou vazia
        public void CriarUsuario_SenhaDeveSerNullOuVazio_Erro()
        {
            Usuario usuario = new Usuario() { Id = "87df87s6" };
            var senha = "";

            var UserStoreMock = Mock.Of<IUserStore<Usuario>>();
            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(UserStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);

            //usuarioRepositorioMock.Setup(c => c.CriarUsuario(usuario, senha));

            var retornoResposta = registroNegocio.CriarUsuario(usuario, senha).Result;

            Assert.AreEqual(Tipo.Erro, retornoResposta.Tipo);
            Assert.AreEqual("Erro: Senha não pode ser nula ou vazia, entre em contato com o Administrador", retornoResposta.Mensagem);

            senha = null;
            retornoResposta = null;

            retornoResposta = registroNegocio.CriarUsuario(usuario, senha).Result;

            Assert.AreEqual(Tipo.Erro, retornoResposta.Tipo);
            Assert.AreEqual("Erro: Senha não pode ser nula ou vazia, entre em contato com o Administrador", retornoResposta.Mensagem);
        }

        //função BuscarUsuarioPorNomeUsuario deve retornar usuário igual a null /////
        [Test]
        public void CriarUsuario_Fun_BuscarUsuarioPorNomeUsuario_DeveRetornarNull_Aviso()
        {
            //simulando usuário que já existe no banco
            List<Usuario> lista = new List<Usuario>();
            var usuarioExistente = new Usuario{ Id = "fd74s7", UserName = "adm" };
            lista.Add(usuarioExistente);

            //simulando novo usuário
            Usuario usuarioNovo = new Usuario() { Id = "87df87s6", UserName = "adm" };
            var senha = "414fdd";

            var UserStoreMock = Mock.Of<IUserStore<Usuario>>();
            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(UserStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);

            usuarioRepositorioMock.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorNomeUsuario(usuarioNovo.UserName)).Returns(Task.FromResult(lista.Where(l => l.UserName == usuarioNovo.UserName).FirstOrDefault()));
            usuarioRepositorioMock.Setup<Task<IdentityResult>>(c => c.CriarUsuario(usuarioNovo, senha)).ReturnsAsync(IdentityResult.Success);//usando 'IdentityResult.Success' retorna true

            var retornoResposta = registroNegocio.CriarUsuario(usuarioNovo, senha).Result;

            Assert.AreEqual(Tipo.Aviso, retornoResposta.Tipo);
            Assert.AreEqual("Aviso: '" + usuarioNovo.UserName + " ' já existe no sistema, deve ser digitado outro 'Nome Usuário'", retornoResposta.Mensagem);
        }

        [Test]
        // função CriarUsuario deve retornar IdentityResult igual a false e ter o campo 'errors' diferente de null
        public void CriarUsuario_Fun_CriarUsuario_DeveRetornarIdentityResultFalseEDiferenteNull_Erro()
        {
            //simulando usuário que já existe no banco
            List<Usuario> lista = new List<Usuario>();
            var usuarioExistente = new Usuario { Id = "fd74s7", UserName = "adm" };
            lista.Add(usuarioExistente);

            //simulando novo usuário
            Usuario usuarioNovo = new Usuario() { Id = "87df87s6", UserName = "cas" };
            var senha = "414fdd";

            var UserStoreMock = Mock.Of<IUserStore<Usuario>>();
            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(UserStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);

            var identity = new IdentityResult();

            usuarioRepositorioMock.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorNomeUsuario(usuarioNovo.UserName)).Returns(Task.FromResult(lista.Where(l => l.UserName == usuarioNovo.UserName).FirstOrDefault()));
            usuarioRepositorioMock.Setup<Task<IdentityResult>>(c => c.CriarUsuario(usuarioNovo, senha)).ReturnsAsync(identity);//usando 'new IdentityResult()' retorna false

            var retornoResposta = registroNegocio.CriarUsuario(usuarioNovo, senha).Result;

            Assert.AreEqual(Tipo.Erro, retornoResposta.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao tentar salvar o usuário, entre em contato com o Administrador", retornoResposta.Mensagem);
            Assert.IsNotNull(retornoResposta.Erros);
        }

        [Test]
        public void CriarUsuario_Sucesso()
        {
            //simulando usuário que já existe no banco
            List<Usuario> lista = new List<Usuario>();
            var usuarioExistente = new Usuario { Id = "fd74s7", UserName = "adm" };
            lista.Add(usuarioExistente);

            //simulando novo usuário
            Usuario usuarioNovo = new Usuario() { Id = "87df87s6", UserName = "cas" };
            var senha = "414fdd";

            var UserStoreMock = Mock.Of<IUserStore<Usuario>>();
            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(UserStoreMock, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);

            usuarioRepositorioMock.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorNomeUsuario(usuarioNovo.UserName)).Returns(Task.FromResult(lista.Where(l => l.UserName == usuarioNovo.UserName).FirstOrDefault()));
            usuarioRepositorioMock.Setup<Task<IdentityResult>>(c => c.CriarUsuario(usuarioNovo, senha)).ReturnsAsync(IdentityResult.Success);//usando 'IdentityResult.Success' retorna true

            var retornoResposta = registroNegocio.CriarUsuario(usuarioNovo, senha).Result;

            Assert.AreEqual(Tipo.Sucesso, retornoResposta.Tipo);
            Assert.AreEqual("Usuário salvo com sucesso", retornoResposta.Mensagem);
        }

        #endregion

        #region EditarUsuario(Usuario usuario)
        //parâmetro usuário deve ser null
        [Test]
        public void EditarUsuario_UsuarioDeveSerNull_Erro()
        {

            Usuario usuario = null;

            var userStore = new Mock<IUserStore<Usuario>>();

            var registroNegocio = new RegistroNegocio(new UsuarioRepositorio(new UserManager<Usuario>(userStore.Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            var respostaNegocio = registroNegocio.EditarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Erro, respostaNegocio.Tipo);
            Assert.AreEqual("Erro: Usuário não pode ser nulo, entre em contato com o Administrador", respostaNegocio.Mensagem);
        }

        //em Usuário a propriedade Id deve ser null ou vazio
        [Test]
        public void EditarUsuario_UsuarioIdDeveSerNullOuVazio_Erro()
        {

            Usuario usuario = new Usuario() { Id = "" };

            var userStore = new Mock<IUserStore<Usuario>>();

            var registroNegocio = new RegistroNegocio(new UsuarioRepositorio(new UserManager<Usuario>(userStore.Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            var respostaNegocio = registroNegocio.EditarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Erro, respostaNegocio.Tipo);
            Assert.AreEqual("Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador", respostaNegocio.Mensagem);

            usuario = new Usuario() { Id = null };

            respostaNegocio = null;
            respostaNegocio = registroNegocio.EditarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Erro, respostaNegocio.Tipo);
            Assert.AreEqual("Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador", respostaNegocio.Mensagem);
        }

        //usuário não deve existir
        [Test]
        public void EditarUsuario_Fun_BuscarUsuarioPorId_DeveRetornarNull_Erro()
        {
            //simula usuários no banco de dados
            List<Usuario> listaBanco = new List<Usuario>();
            listaBanco.Add(new Usuario() { Id = "f1ds81d44" });
            listaBanco.Add(new Usuario() { Id = "f1ds81dRR" });

            Usuario usuario = new Usuario() { Id = "f1ds81dD" };

            var userStore = new Mock<IUserStore<Usuario>>();

            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(userStore.Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);
            //não deve encontrar nenhum usuário no banco
            usuarioRepositorioMock.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(usuario.Id)).Returns(Task.FromResult(listaBanco.Where(l => l.Id == usuario.Id).FirstOrDefault()));

            var respostaNegocio = registroNegocio.EditarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Erro, respostaNegocio.Tipo);
            Assert.AreEqual("Erro: O usuário não existe no sistema", respostaNegocio.Mensagem);
        }

        //listaUsuarios deve ser null
        [Test]
        public void EditarUsuario_Fun_ListaUsuarios_DeveRetornarNull_Erro()
        {
            //simula usuários no banco de dados
            List<Usuario> listaBanco = new List<Usuario>();
            listaBanco.Add(new Usuario() { Id = "f1ds81d44" });
            listaBanco.Add(new Usuario() { Id = "f1ds81dRR" });

            Usuario usuario = new Usuario() { Id = "f1ds81d44" };

            var userStore = new Mock<IUserStore<Usuario>>();

            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(userStore.Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);
            //não deve encontrar nenhum usuário no banco
            usuarioRepositorioMock.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(usuario.Id)).Returns(Task.FromResult(listaBanco.Where(l => l.Id == usuario.Id).FirstOrDefault()));
            IQueryable<Usuario> listaNula = null;
            usuarioRepositorioMock.Setup(l => l.ListarUsuarios()).Returns(listaNula);

            var respostaNegocio = registroNegocio.EditarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Erro, respostaNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao buscar lista de usuários, entre em contato com o Administrador", respostaNegocio.Mensagem);
        }

        //no objeto usuário, deve ter a propriedade 'nome usuario' com mesmo valor que um usuário já cadastrado no banco
        [Test]
        public void EditarUsuario_DeveExistirOutroUsuarioComMesmoNomeUsuario_Erro()
        {
            //simula usuários no banco de dados
            List<Usuario> listaBanco = new List<Usuario>();
            listaBanco.Add(new Usuario() { Id = "f1ds81d44", UserName = "cers" });
            listaBanco.Add(new Usuario() { Id = "f1ds811p", UserName = "adm" });
            listaBanco.Add(new Usuario() { Id = "f1ds81dD", UserName = "vit" });

            Usuario usuario = new Usuario() { Id = "f1ds81dD", UserName = "adm" };

            var userStore = new Mock<IUserStore<Usuario>>();

            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(userStore.Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);
            usuarioRepositorioMock.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(usuario.Id)).Returns(Task.FromResult(listaBanco.Where(l => l.Id == usuario.Id).FirstOrDefault()));

            usuarioRepositorioMock.Setup<IQueryable<Usuario>>(l => l.ListarUsuarios()).Returns(listaBanco.AsQueryable());

            //não deve encontrar nenhum usuário no banco
            var teste = listaBanco.Where(l => (l.UserName.ToUpper() == usuario.UserName.ToUpper()) && (l.Id.Trim() != usuario.Id.Trim())).ToList();

            var respostaNegocio = registroNegocio.EditarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Aviso, respostaNegocio.Tipo);
            Assert.AreEqual("Aviso: Já existe 'Nome Usuário' com esse valor, só pode ser inserido 'Nome Usuário' que ainda não exista no sistema", respostaNegocio.Mensagem);
        }

        //função EditarUsuario deve retornar IdentityResultFalse.
        [Test]
        public void EditarUsuario_Func_EditarUsuario_DeveRetornarIdentityResultFalse_Erro()
        {
            //simula usuários no banco de dados
            List<Usuario> listaBanco = new List<Usuario>();
            listaBanco.Add(new Usuario() { Id = "f1ds81d44", UserName = "cers" });
            listaBanco.Add(new Usuario() { Id = "f1ds811p", UserName = "adm" });
            listaBanco.Add(new Usuario() { Id = "f1ds81dD", UserName = "vit" });

            Usuario usuario = new Usuario() { Id = "f1ds81dD", UserName = "vit" };

            var userStore = new Mock<IUserStore<Usuario>>();

            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(userStore.Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);
            usuarioRepositorioMock.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(usuario.Id)).Returns(Task.FromResult(listaBanco.Where(l => l.Id == usuario.Id).FirstOrDefault()));

            usuarioRepositorioMock.Setup<IQueryable<Usuario>>(l => l.ListarUsuarios()).Returns(listaBanco.AsQueryable());

            usuarioRepositorioMock.Setup<Task<IdentityResult>>(u => u.EditarUsuario(It.IsAny<Usuario>())).ReturnsAsync(new IdentityResult());

            var respostaNegocio = registroNegocio.EditarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Erro, respostaNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao tentar editar o usuário, entre em contato com o Administrador", respostaNegocio.Mensagem);
        }

        [Test]
        public void EditarUsuario_Sucesso()
        {
            //simula usuários no banco de dados
            List<Usuario> listaBanco = new List<Usuario>();
            listaBanco.Add(new Usuario() { Id = "f1ds81d44", UserName = "cers" });
            listaBanco.Add(new Usuario() { Id = "f1ds811p", UserName = "adm" });
            listaBanco.Add(new Usuario() { Id = "f1ds81dD", UserName = "vit", Email = "antigo@gmail.com" });

            Usuario usuario = new Usuario() { Id = "f1ds81dD", UserName = "vit", Email = "novo@gmail.com" };

            var userStore = new Mock<IUserStore<Usuario>>();

            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(userStore.Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);
            usuarioRepositorioMock.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(usuario.Id)).Returns(Task.FromResult(listaBanco.Where(l => l.Id == usuario.Id).FirstOrDefault()));

            usuarioRepositorioMock.Setup<IQueryable<Usuario>>(l => l.ListarUsuarios()).Returns(listaBanco.AsQueryable());

            usuarioRepositorioMock.Setup<Task<IdentityResult>>(u => u.EditarUsuario(It.IsAny<Usuario>())).ReturnsAsync(IdentityResult.Success);

            //Testando edição de usuario com Id e UserName iguais - simulando modificação de outras informações menos o Username
            var respostaNegocio = registroNegocio.EditarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Sucesso, respostaNegocio.Tipo);
            Assert.AreEqual("Usuário editado com sucesso", respostaNegocio.Mensagem);

            //Testando edição de usuario com Id igual porém com  UserName diferente - simulando modificação de outras informações inclusive o Username
            usuario = null;
            usuario = new Usuario() { Id = "f1ds81dD", UserName = "diferente", Email = "novo@gmail.com" };

            respostaNegocio = null;
            respostaNegocio = registroNegocio.EditarUsuario(usuario).Result;

            Assert.AreEqual(Tipo.Sucesso, respostaNegocio.Tipo);
            Assert.AreEqual("Usuário editado com sucesso", respostaNegocio.Mensagem);


        }

        #endregion

        #region BuscarUsuarioPorRole(string role)

        [Test]
        //Role deve ser null
        public void BuscarUsuarioPorRole_RoleDeveSerNull_Erro()
        {
            string role = null;

            var registroNegocio = new RegistroNegocio(new UsuarioRepositorio(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            var resultadoNegocio = (RespostaNegocio)registroNegocio.BuscarUsuarioPorRole(role).Result;

            Assert.AreEqual(Tipo.Erro, resultadoNegocio.Tipo);
            Assert.AreEqual("Erro: papel não pode ser nulo ou vazio, entre em contato com o Administrador", resultadoNegocio.Mensagem);

            role = "";
            resultadoNegocio = null;
            resultadoNegocio = (RespostaNegocio)registroNegocio.BuscarUsuarioPorRole(role).Result;

            Assert.AreEqual(Tipo.Erro, resultadoNegocio.Tipo);
            Assert.AreEqual("Erro: papel não pode ser nulo ou vazio, entre em contato com o Administrador", resultadoNegocio.Mensagem);

        }


        [Test]
        //Função 'BuscarUsuarioPorRole' do repositório deve retornar null.
        public void BuscarUsuarioPorRole_func_BuscarUsuarioPorRoleDeveRetornarNull_Erro()
        {
            string role = "admin";

            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);

            usuarioRepositorioMock.Setup(l => l.BuscarUsuarioPorRole(role));

            var retornoNegocio = registroNegocio.BuscarUsuarioPorRole(role).Result;

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Lista de Usuários não pode ser nula, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }

        //sucesso
        [Test]
        public void BuscarUsuarioPorRole_sucesso()
        {
            var listaBanco = new List<KeyValuePair<string, Usuario>>();
            listaBanco.Add(new KeyValuePair<string, Usuario>("adm", new Usuario() { Id = "74dsf74f", UserName = "admRoger", Nome = "Roger" }));
            listaBanco.Add(new KeyValuePair<string, Usuario>("basico", new Usuario() { Id = "74dsf711", UserName = "ana2", Nome = "Ana" }));
            listaBanco.Add(new KeyValuePair<string, Usuario>("completo", new Usuario() { Id = "99dsf74f", UserName = "bruno14", Nome = "Bruno" }));
            listaBanco.Add(new KeyValuePair<string, Usuario>("basico", new Usuario() { Id = "00dsf74f", UserName = "car18", Nome = "Carla" }));

            var resultadoEsperado = listaBanco.Where(l => l.Key == "basico").Select(s => s.Value);

            string role = "basico";

            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);

            IList<Usuario> restul = listaBanco.Where(l => l.Key == role).Select(s => s.Value).ToList();

            usuarioRepositorioMock.Setup<Task<IList<Usuario>>>(l => l.BuscarUsuarioPorRole(role)).Returns(Task.FromResult(restul));

            var retornoNegocio = registroNegocio.BuscarUsuarioPorRole(role).Result;

            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual(2, ((List<Usuario>)retornoNegocio.Objeto).Count());
            Assert.AreEqual(resultadoEsperado, retornoNegocio.Objeto);

        }

        #endregion

        #region RemoverRolesPorUsuario(Usuario usuario, IList<string> roles)

        //usuario deve ser null
        [Test]
        public void RemoverRolesPorUsuario_UsuarioDeveSerNull_Erro()
        {
            Usuario usuario = null;

            var registro = new RegistroNegocio(new UsuarioRepositorio(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            var retornoNegocio = registro.RemoverRolesPorUsuario(usuario, new List<string>()).Result;

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Usuário não pode ser nulo, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }

        //id do usuário deve ser null ou vazio
        [Test]
        public void RemoverRolesPorUsuario_UsuarioIdDeveSerNullOuVazio_Erro()
        {
            var usuario = new Usuario() { Id = "" };

            var registroNegocio = new RegistroNegocio(new UsuarioRepositorio(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            var retornoNegocio = registroNegocio.RemoverRolesPorUsuario(usuario, null).Result;

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador", retornoNegocio.Mensagem);

            usuario.Id = null;
            retornoNegocio = null;
            retornoNegocio = registroNegocio.RemoverRolesPorUsuario(usuario, null).Result;

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        //lista de roles deve ser null
        [Test]
        public void RemoverRolesPorUsuario_ListaRolesDeveSerNull_Erro()
        {
            var usuario = new Usuario() { Id = "fd414EE1" };

            var registroNegocio = new RegistroNegocio(new UsuarioRepositorio(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            var retornoNegocio = registroNegocio.RemoverRolesPorUsuario(usuario, null).Result;

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Lista de Permissões não pode ser nula, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }

        //usuário não deve existir
        [Test]
        public void RemoverRolesPorUsuario_UsuarioNaoDeveExistir_Erro()
        {
            var usuario = new Usuario() { Id = "fd414EE1" };

            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);

            usuarioRepositorioMock.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(usuario.Id));

            var retornoNegocio = registroNegocio.RemoverRolesPorUsuario(usuario, new List<string>()).Result;

            Assert.AreEqual(Tipo.Aviso, retornoNegocio.Tipo);
            Assert.AreEqual("Aviso: O usuário pode ter sido excluído anteriormente ou não existe no sistema", retornoNegocio.Mensagem);
        }

        //A função 'RemoverRolesPorUsuario' do repositório deve retornar IdentityResult.Success com valor false.
        [Test]
        public void RemoverRolesPorUsuario_Func_RemoverRolesPorUsuarioDeveRetornarFalse_Erro()
        {
            var usuario = new Usuario() { Id = "fd414EE1" };

            var listaRoles = new List<string>();
            listaRoles.Add("Basico");
            listaRoles.Add("Administrador");

            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);

            usuarioRepositorioMock.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(usuario.Id)).ReturnsAsync(usuario);
            usuarioRepositorioMock.Setup(u => u.RemoverRolesPorUsuario(usuario, listaRoles)).ReturnsAsync(new IdentityResult());//retornar Success como false
            
            var retornoNegocio = registroNegocio.RemoverRolesPorUsuario(usuario, listaRoles).Result;

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao tentar remover as permissões, entre em contato com o Administrador", retornoNegocio.Mensagem);
                        
        }

        //Sucesso
        [Test]
        public void RemoverRolesPorUsuario_Sucesso()
        {
            var usuario = new Usuario() { Id = "fd414EE1" };

            var listaRoles = new List<string>();
            listaRoles.Add("Basico");
            listaRoles.Add("Administrador");

            var usuarioRepositorioMock = new Mock<UsuarioRepositorio>(new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null), new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var registroNegocio = new RegistroNegocio(usuarioRepositorioMock.Object);

            usuarioRepositorioMock.Setup<Task<Usuario>>(u => u.BuscarUsuarioPorId(usuario.Id)).ReturnsAsync(usuario);
            usuarioRepositorioMock.Setup<Task<IdentityResult>>(u => u.RemoverRolesPorUsuario(usuario, listaRoles)).ReturnsAsync(IdentityResult.Success);//retornar Success como true

            var retornoNegocio = registroNegocio.RemoverRolesPorUsuario(usuario, listaRoles).Result;

            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual("Permissões removidas com sucesso", retornoNegocio.Mensagem);
        }

        #endregion
    }
}
