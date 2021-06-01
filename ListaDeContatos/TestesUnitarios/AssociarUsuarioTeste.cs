using ListaDeContatos.DTO;
using ListaDeContatos.Enumerador;
using ListaDeContatos.Models;
using ListaDeContatos.Negocio.Implementacao;
using ListaDeContatos.Repositorio.Implementacao;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;


namespace TestesUnitarios
{
    [TestFixture]
    class AssociarUsuarioTeste
    {



        //ListarUsuariosComNivelAcesso
        /**
         
        - fun ListarUsuariosComOuSemNivelAcesso não pode ser nula => ok
        - sucesso ao listar => ok
          
         */
        #region ListarUsuariosComOuSemNivelAcesso()

        [Test]
        //fun ListarUsuariosComOuSemNivelAcesso deve ser nula
        public void AssociarUsuario_Func_ListarUsuariosComOuSemNivelAcesso_DeveRetornarNull_Erro()
        {
            var _userStoreMock = Mock.Of<IUserStore<Usuario>>();
            var _userManager = new UserManager<Usuario>(_userStoreMock, null, null, null, null, null, null, null, null);
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, new NivelAcessoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            _usuarioNivelAcessoRepositorioMock.Setup(a => a.ListarUsuariosComOuSemNivelAcesso());

            var retornoNegocio = _associarUsuarioNegocio.ListarUsuariosComOuSemNivelAcesso();

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao buscar lista de usuários que tenham associado algum 'perfil / nível de acesso' , entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        public void AssociarUsuario_Sucesso()
        {
            var listaUsario = new List<UsuarioNivelAcessoDTO>();
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Administrador", NomeUsuario = "Carlos", RoleId = "f678sd6f78sfa", UserId = "OOPsd6f78sfa" });
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Basico", NomeUsuario = "Rosana", RoleId = "99rrd6f78sfa", UserId = "OO88rrf78sfa" });

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, new NivelAcessoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));


            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcessoDTO>>(l => l.ListarUsuariosComOuSemNivelAcesso()).Returns(listaUsario);

            var retornoNegocio = _associarUsuarioNegocio.ListarUsuariosComOuSemNivelAcesso();

            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual("OOPsd6f78sfa", ((List<UsuarioNivelAcessoDTO>)retornoNegocio.Objeto).Find(x => x.NomeUsuario == "Carlos").UserId);
            Assert.AreEqual(2, ((List<UsuarioNivelAcessoDTO>)retornoNegocio.Objeto).Count);
        }

        #endregion

        //EditarNivelAcessoDoUsuario
        /**
         - parâmetro usuarioId não pode ser nulo ou vazio => ok
         - parâmetro usuarioId não deve ser zero => ok
         - parâmetro NiveAcessoId não pode ser nulo ou vazio => ok
         - parâmetro NiveAcessoId não deve ser zero => ok

         - busca na fun ListarNivelAcesso não pode ser null => ok
         - resultado da busca da fun ListarNivelAcesso deve conter o NivelAcesso com Name == "Administrador". => ok
         - pesquisar fun ListarUsuarioNivelAcesso não deve retornar null  => ok      
         - resultado da pesquisar fun ListarUsuarioNivelAcesso (que tem somente usuários atrelados ao perfil do tipo 'Administrador') se a lista retornar menos que 2 
            E o parâmetro 'niveAcessoId/roleId' for diferente do ID que representa 'Administrador' no banco 
            E se a mesma lista ListarUsuarioNivelAcesso que só tem tipo 'Administrador' contiver o id do usuário que está sendo editado, então não deve permitir edição
            => ok
         - fun SalvarEditarUsuarioNivelAcesso não deve retornar false => ok
         - sucesso ao editar usuário => ok
         */
        #region EditarNivelAcessoDoUsuario(string usuarioId, string NiveAcessoId )

        //parâmetro usuarioId deve ser nulo ou vazio
        [Test]
        public void EditarNivelAcessoDoUsuario_usuarioIdDeveSerNullOuVazio_Erro()
        {
            string usuarioId = null;

            var listaUsario = new List<UsuarioNivelAcessoDTO>();
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Administrador", NomeUsuario = "Carlos", RoleId = "f678sd6f78sfa", UserId = "OOPsd6f78sfa" });
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Basico", NomeUsuario = "Rosana", RoleId = "99rrd6f78sfa", UserId = "OO88rrf78sfa" });

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, new NivelAcessoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));


            //_usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcessoDTO>>(l => l.ListarUsuariosComOuSemNivelAcesso()).Returns(listaUsario);

            var retornoNegocio = _associarUsuarioNegocio.EditarNivelAcessoDoUsuario(usuarioId, null);


            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Id do Usuário não pode ser nulo ou vazio, entre em contato com o Administrador", retornoNegocio.Mensagem);

            usuarioId = "";

            retornoNegocio = null;
            retornoNegocio = _associarUsuarioNegocio.EditarNivelAcessoDoUsuario(usuarioId, null);


            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Id do Usuário não pode ser nulo ou vazio, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }

        //parâmetro usuarioId deve ser zero

        [Test]
        public void EditarNivelAcessoDoUsuario_usuarioIdDeveSerZero_Erro()
        {
            string usuarioId = "0";

            var listaUsario = new List<UsuarioNivelAcessoDTO>();
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Administrador", NomeUsuario = "Carlos", RoleId = "f678sd6f78sfa", UserId = "OOPsd6f78sfa" });
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Basico", NomeUsuario = "Rosana", RoleId = "99rrd6f78sfa", UserId = "OO88rrf78sfa" });

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, new NivelAcessoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));


            //_usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcessoDTO>>(l => l.ListarUsuariosComOuSemNivelAcesso()).Returns(listaUsario);

            var retornoNegocio = _associarUsuarioNegocio.EditarNivelAcessoDoUsuario(usuarioId, null);


            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Id do Usuário não pode ser zero, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }

        //parâmetro NiveAcessoId deve ser nulo ou vazio

        [Test]
        public void EditarNivelAcessoDoUsuario_nivelAcessoIddDeveSerNullOuVazio_Erro()
        {
            string usuarioId = "sdf41741fds4wRK";
            string nivelAcessoId = null;

            var listaUsario = new List<UsuarioNivelAcessoDTO>();
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Administrador", NomeUsuario = "Carlos", RoleId = "f678sd6f78sfa", UserId = "OOPsd6f78sfa" });
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Basico", NomeUsuario = "Rosana", RoleId = "99rrd6f78sfa", UserId = "OO88rrf78sfa" });

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, new NivelAcessoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));


            //_usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcessoDTO>>(l => l.ListarUsuariosComOuSemNivelAcesso()).Returns(listaUsario);

            var retornoNegocio = _associarUsuarioNegocio.EditarNivelAcessoDoUsuario(usuarioId, nivelAcessoId);


            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Id do Nível Acesso não pode ser nulo ou vazio, entre em contato com o Administrador", retornoNegocio.Mensagem);

            nivelAcessoId = "";

            retornoNegocio = null;
            retornoNegocio = _associarUsuarioNegocio.EditarNivelAcessoDoUsuario(usuarioId, nivelAcessoId);


            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Id do Nível Acesso não pode ser nulo ou vazio, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }

        // parâmetro NiveAcessoId deve ser zero

        [Test]
        public void EditarNivelAcessoDoUsuario_niveAcessoIdDeveSerZero_Erro()
        {
            string usuarioId = "sdf41741fds4wRK";
            string nivelAcessoId = "0";

            var listaUsario = new List<UsuarioNivelAcessoDTO>();
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Administrador", NomeUsuario = "Carlos", RoleId = "f678sd6f78sfa", UserId = "OOPsd6f78sfa" });
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Basico", NomeUsuario = "Rosana", RoleId = "99rrd6f78sfa", UserId = "OO88rrf78sfa" });

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, new NivelAcessoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));


            //_usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcessoDTO>>(l => l.ListarUsuariosComOuSemNivelAcesso()).Returns(listaUsario);

            var retornoNegocio = _associarUsuarioNegocio.EditarNivelAcessoDoUsuario(usuarioId, nivelAcessoId);


            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Id do Nível Acesso não pode ser zero, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }

        //busca na fun ListarNivelAcesso deve ser null
        [Test]
        public void EditarNivelAcessoDoUsuario_Func_ListarNivelAcesso_deveRetornaNull_Erro()
        {
            string usuarioId = "sdf41741fds4wRK";
            string nivelAcessoId = "Uh88)jd&dhA";

            var listaUsario = new List<UsuarioNivelAcessoDTO>();
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Administrador", NomeUsuario = "Carlos", RoleId = "f678sd6f78sfa", UserId = "OOPsd6f78sfa" });
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Basico", NomeUsuario = "Rosana", RoleId = "99rrd6f78sfa", UserId = "OO88rrf78sfa" });

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, _nivelAcessoRepositorioMock.Object);

            List<NivelAcesso> listaBanco = null;

            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBanco);

            var retornoNegocio = _associarUsuarioNegocio.EditarNivelAcessoDoUsuario(usuarioId, nivelAcessoId);


            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao buscar lista de 'perfil / nível de acesso' para edição, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }

        //resultado da busca na fun ListarNivelAcesso com Name == "Administrador" deve ser null

        [Test]
        public void EditarNivelAcessoDoUsuario_Result_Func_ListarNivelAcesso_deveRetornaNull_Erro()
        {
            string usuarioId = "sdf41741fds4wRK";
            string nivelAcessoId = "Uh88)jd&dhA";

            var listaUsario = new List<UsuarioNivelAcessoDTO>();
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Administrador", NomeUsuario = "Carlos", RoleId = "f678sd6f78sfa", UserId = "OOPsd6f78sfa" });
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Basico", NomeUsuario = "Rosana", RoleId = "99rrd6f78sfa", UserId = "OO88rrf78sfa" });

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, _nivelAcessoRepositorioMock.Object);

            List<NivelAcesso> listaBanco = new List<NivelAcesso>();
            listaBanco.Add(new NivelAcesso() { Name = "Basico", Id = "111abc" });


            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBanco);

            var retornoNegocio = _associarUsuarioNegocio.EditarNivelAcessoDoUsuario(usuarioId, nivelAcessoId);


            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao buscar 'perfil / nível de acesso' do tipo Administrador, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }

        /* pesquisar fun ListarUsuarioNivelAcesso deve retornar null
        */
        [Test]
        public void EditarNivelAcessoDoUsuario_Func_ListarUsuarioNivelAcesso_deveRetornaNull_Erro()
        {
            string usuarioId = "sdf41741fds4wRK";
            string nivelAcessoId = "Uh88)jd&dhA";

            var listaUsario = new List<UsuarioNivelAcessoDTO>();
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Administrador", NomeUsuario = "Carlos", RoleId = "f678sd6f78sfa", UserId = "OOPsd6f78sfa" });
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Basico", NomeUsuario = "Rosana", RoleId = "99rrd6f78sfa", UserId = "OO88rrf78sfa" });

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, _nivelAcessoRepositorioMock.Object);

            List<NivelAcesso> listaBancoNivelAcesso = new List<NivelAcesso>();
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Basico", Id = "111abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Completo", Id = "222abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Administrador", Id = "333abc" });

            List<UsuarioNivelAcesso> listaBanco = null;


            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBancoNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(u => u.ListarUsuarioNivelAcesso()).Returns(listaBanco);

            var retornoNegocio = _associarUsuarioNegocio.EditarNivelAcessoDoUsuario(usuarioId, nivelAcessoId);


            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao buscar lista de Usuários com seu respectivo 'perfil / nível de acesso', entre em contato com o Administrador", retornoNegocio.Mensagem);

        }

        /* resultado da pesquisar fun ListarUsuarioNivelAcesso (que tem somente usuários atrelados ao perfil do tipo 'Administrador') 
         se a lista retornar menos que 2 E o parâmetro 'niveAcessoId/roleId' for diferente do ID que representa 'Administrador' no banco E se a 
         mesma lista ListarUsuarioNivelAcesso que só tem tipo 'Administrador' contiver o id do usuário que está sendo editado, deve retornar aviso
        */
        [Test]
        public void EditarNivelAcessoDoUsuario_Result_Func_ListarUsuarioNivelAcesso_deveSerVerdadeiro_Aviso()
        {
            string usuarioId = "sdf41741fds4wRK";
            string nivelAcessoId = "222abc";

            var listaUsario = new List<UsuarioNivelAcessoDTO>();
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Administrador", NomeUsuario = "Carlos", RoleId = "f678sd6f78sfa", UserId = "OOPsd6f78sfa" });
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Basico", NomeUsuario = "Rosana", RoleId = "99rrd6f78sfa", UserId = "OO88rrf78sfa" });

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, _nivelAcessoRepositorioMock.Object);

            List<NivelAcesso> listaBancoNivelAcesso = new List<NivelAcesso>();
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Basico", Id = "111abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Completo", Id = "222abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Administrador", Id = "333abc" });

            List<UsuarioNivelAcesso> listaBancoUsuarioNivelAcesso = new List<UsuarioNivelAcesso>();
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "sdf41741fds4wRK", RoleId = "333abc" });

            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBancoNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(u => u.ListarUsuarioNivelAcesso()).Returns(listaBancoUsuarioNivelAcesso);

            var retornoNegocio = _associarUsuarioNegocio.EditarNivelAcessoDoUsuario(usuarioId, nivelAcessoId);


            Assert.AreEqual(Tipo.Aviso, retornoNegocio.Tipo);
            Assert.AreEqual("Aviso: Não foi permitindo edição pois tem somente 1 usuário no sistema com perfil 'administrador'", retornoNegocio.Mensagem);

        }

        //fun SalvarEditarUsuarioNivelAcesso deve retornar false
        [Test]
        public void EditarNivelAcessoDoUsuario_Func_SalvarEditarUsuarioNivelAcesso_deveSerFalso_Erro()
        {

            string usuarioId = "sdf41741fds4wRK";
            string nivelAcessoId = "222abc";

            var listaUsario = new List<UsuarioNivelAcessoDTO>();
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Administrador", NomeUsuario = "Carlos", RoleId = "f678sd6f78sfa", UserId = "OOPsd6f78sfa" });
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Basico", NomeUsuario = "Rosana", RoleId = "99rrd6f78sfa", UserId = "OO88rrf78sfa" });

            //var dbContext = new Mock<DbContextOptions<ApplicationDBContext>>();
            //dbContext.Setup(m => m.ContextType).Returns(ContextType.Object);

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
           

            //_applicationDBContextMock.Setup<IDbContextTransaction>(a => a.Database.BeginTransaction()).Returns(transacao.Object);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, _nivelAcessoRepositorioMock.Object);

            var transacao = new Mock<IDbContextTransaction>();


            List<NivelAcesso> listaBancoNivelAcesso = new List<NivelAcesso>();
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Basico", Id = "111abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Completo", Id = "222abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Administrador", Id = "333abc" });

            List<UsuarioNivelAcesso> listaBancoUsuarioNivelAcesso = new List<UsuarioNivelAcesso>();
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "sdf41741fds4wRK", RoleId = "333abc" });
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "Oi90I741fds4wRK", RoleId = "333abc" });

            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBancoNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(u => u.ListarUsuarioNivelAcesso()).Returns(listaBancoUsuarioNivelAcesso);

            var usuarioNivelAcesso = new UsuarioNivelAcesso() { UserId = usuarioId, RoleId = nivelAcessoId };
            _usuarioNivelAcessoRepositorioMock.Setup(u => u.SalvarEditarUsuarioNivelAcesso(usuarioNivelAcesso)).Returns(false);


            var retornoNegocio = _associarUsuarioNegocio.EditarNivelAcessoDoUsuario(usuarioId, nivelAcessoId);


            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao tentar salvar a edição do Usuário com o 'Perfil / Nível Acesso' associado, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }

        [Test]
        public void EditarNivelAcessoDoUsuario_Sucesso()
        {
            var listaUsario = new List<UsuarioNivelAcessoDTO>();
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Administrador", NomeUsuario = "Carlos", RoleId = "f678sd6f78sfa", UserId = "OOPsd6f78sfa" });
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Basico", NomeUsuario = "Rosana", RoleId = "99rrd6f78sfa", UserId = "OO88rrf78sfa" });

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);


           var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, _nivelAcessoRepositorioMock.Object);

           

            List<NivelAcesso> listaBancoNivelAcesso = new List<NivelAcesso>();
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Basico", Id = "111abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Completo", Id = "222abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Administrador", Id = "333abc" });

            List<UsuarioNivelAcesso> listaBancoUsuarioNivelAcesso = null;

            //Condições possíveis de acontecer e que ao mesmo tempo atendem os critérios permitidos para a edição de um Usuário:
            //-lista de usuários atrelados a perfil 'Administrador' maior que 1
            listaBancoUsuarioNivelAcesso = new List<UsuarioNivelAcesso>();
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "sdf41741fds4wRK", RoleId = "333abc" });
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "Oi90I741fds4wRK", RoleId = "333abc" });

            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBancoNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(u => u.ListarUsuarioNivelAcesso()).Returns(listaBancoUsuarioNivelAcesso);

            _usuarioNivelAcessoRepositorioMock.Setup(u => u.SalvarEditarUsuarioNivelAcesso(It.IsAny<UsuarioNivelAcesso>())).Returns(true);

            string usuarioId = "sdf41741fds4wRK";
            string nivelAcessoId = "222abc";

            var retornoNegocio = _associarUsuarioNegocio.EditarNivelAcessoDoUsuario(usuarioId, nivelAcessoId);

            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual("Edição de Usuário associado com 'Perfil / Nível Acesso' salvo com sucesso", retornoNegocio.Mensagem);
            //- lista de usuários atrelados a perfil 'Administrador' menor que 2 E parâmetro niveAcessoId igual ao ID que representa 'Administrador' na tabela do banco NivelAcesso.

            listaBancoUsuarioNivelAcesso = new List<UsuarioNivelAcesso>();
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "sdf41741fds4wRK", RoleId = "333abc" });
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "Oi90I741fds4wRK", RoleId = "222abc" });

            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBancoNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(u => u.ListarUsuarioNivelAcesso()).Returns(listaBancoUsuarioNivelAcesso);

            _usuarioNivelAcessoRepositorioMock.Setup(u => u.SalvarEditarUsuarioNivelAcesso(It.IsAny<UsuarioNivelAcesso>())).Returns(true);

            retornoNegocio = null;
            usuarioId = "sdf41741fds4wRK";
            nivelAcessoId = "333abc";

            retornoNegocio = _associarUsuarioNegocio.EditarNivelAcessoDoUsuario(usuarioId, nivelAcessoId);

            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual("Edição de Usuário associado com 'Perfil / Nível Acesso' salvo com sucesso", retornoNegocio.Mensagem);

            //-lista de usuários atrelados a perfil 'Administrador' menor que 2 E parâmetro niveAcessoId diferente ao ID que representa 'Administrador' na tabela do banco NivelAcesso E parâmetro usuarioId não existe na lista de usuários atrelados ao perfil 'Administrador'

            listaBancoUsuarioNivelAcesso = new List<UsuarioNivelAcesso>();
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "sdf41741fds4wRK", RoleId = "111abc" });
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "Oi90I741fds4wRK", RoleId = "333abc" });

            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBancoNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(u => u.ListarUsuarioNivelAcesso()).Returns(listaBancoUsuarioNivelAcesso);

            _usuarioNivelAcessoRepositorioMock.Setup(u => u.SalvarEditarUsuarioNivelAcesso(It.IsAny<UsuarioNivelAcesso>())).Returns(true);

            retornoNegocio = null;
            usuarioId = "sdf41741fds4wRK";
            nivelAcessoId = "222abc";

            retornoNegocio = _associarUsuarioNegocio.EditarNivelAcessoDoUsuario(usuarioId, nivelAcessoId);

            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual("Edição de Usuário associado com 'Perfil / Nível Acesso' salvo com sucesso", retornoNegocio.Mensagem);

            //- lista de usuários atrelados a perfil 'Administrador' igual a 2 E parâmetro niveAcessoId diferente ao ID que representa 'Administrador' na tabela do banco NivelAcesso E parâmetro usuarioId existe na lista de usuários atrelados ao perfil 'Administrador'

            listaBancoUsuarioNivelAcesso = new List<UsuarioNivelAcesso>();
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "sdf41741fds4wRK", RoleId = "333abc" });
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "Oi90I741fds4wRK", RoleId = "333abc" });

            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBancoNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(u => u.ListarUsuarioNivelAcesso()).Returns(listaBancoUsuarioNivelAcesso);

            _usuarioNivelAcessoRepositorioMock.Setup(u => u.SalvarEditarUsuarioNivelAcesso(It.IsAny<UsuarioNivelAcesso>())).Returns(true);

            retornoNegocio = null;
            usuarioId = "sdf41741fds4wRK";
            nivelAcessoId = "222abc";

            retornoNegocio = _associarUsuarioNegocio.EditarNivelAcessoDoUsuario(usuarioId, nivelAcessoId);

            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual("Edição de Usuário associado com 'Perfil / Nível Acesso' salvo com sucesso", retornoNegocio.Mensagem);

        }
        #endregion


        //ExcluirNivelAcessoDoUsuario
        /**
          - parâmetro usuarioId não pode ser nulo ou vazio => ok
          - parâmetro usuarioId deve ser zero => ok

          - busca na fun ListarNivelAcesso não pode ser null => ok
          - resultado da busca da fun ListarNivelAcesso deve conter o NivelAcesso com Name == "Administrador". => ok
          - pesquisar fun ListarUsuarioNivelAcesso não deve retornar null  => ok 

         - resultado da func ListarUsuarioNivelAcesso não pode excluir se o usuário tem o 'perfil/ nível acessso' como 'Administrador' e existe 
            somente 1 usuário no sistema com perfil 'Administrador' => ok

         - caso o usuário selecionado não tenha associação com Nível Acesso/Perfil, então pode ser que essa associação já tenha sido excluida, assim
            se a listaUsuarioNivelAcesso não tiver o UsuarioId, então deve retornar um aviso => ok


         - fun ExcluirNivelAcesso não deve retornar false => ok
         - sucesso ao editar usuário => ok
         */
        #region ExcluirNivelAcessoDoUsuario(string usuarioId)

        //parâmetro usuarioId deve ser nulo ou vazio
        [Test]
        public void ExcluirNivelAcessoDoUsuario_usuarioIdDeveSerNullOuVazio_Erro()
        {
            string usuarioId = null;

            var listaUsario = new List<UsuarioNivelAcessoDTO>();
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Administrador", NomeUsuario = "Carlos", RoleId = "f678sd6f78sfa", UserId = "OOPsd6f78sfa" });
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Basico", NomeUsuario = "Rosana", RoleId = "99rrd6f78sfa", UserId = "OO88rrf78sfa" });

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, new NivelAcessoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));


            //_usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcessoDTO>>(l => l.ListarUsuariosComOuSemNivelAcesso()).Returns(listaUsario);

            var retornoNegocio = _associarUsuarioNegocio.ExcluirNivelAcessoDoUsuario(usuarioId);


            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Id do Usuário não pode ser nulo ou vazio, entre em contato com o Administrador", retornoNegocio.Mensagem);

            usuarioId = "";

            retornoNegocio = null;
            retornoNegocio = _associarUsuarioNegocio.ExcluirNivelAcessoDoUsuario(usuarioId);


            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Id do Usuário não pode ser nulo ou vazio, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }

        //parâmetro usuarioId deve ser zero
        [Test]
        public void ExcluirNivelAcessoDoUsuario_usuarioIdDeveSerZero_Erro()
        {
            string usuarioId = "0";

            var listaUsario = new List<UsuarioNivelAcessoDTO>();
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Administrador", NomeUsuario = "Carlos", RoleId = "f678sd6f78sfa", UserId = "OOPsd6f78sfa" });
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Basico", NomeUsuario = "Rosana", RoleId = "99rrd6f78sfa", UserId = "OO88rrf78sfa" });

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, new NivelAcessoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));


            //_usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcessoDTO>>(l => l.ListarUsuariosComOuSemNivelAcesso()).Returns(listaUsario);

            var retornoNegocio = _associarUsuarioNegocio.ExcluirNivelAcessoDoUsuario(usuarioId);

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Id do Usuário não pode ser zero, entre em contato com o Administrador", retornoNegocio.Mensagem);


        }

        //func ListarNivelAcesso não pode vir nula
        [Test]
        public void ExcluirNivelAcessoDoUsuario_func_ListarNivelAcesso_DeveSerNull_Erro()
        {
            string usuarioId = "A41df0TTR74aa5c";

            var listaUsario = new List<UsuarioNivelAcessoDTO>();
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Administrador", NomeUsuario = "Carlos", RoleId = "f678sd6f78sfa", UserId = "OOPsd6f78sfa" });
            listaUsario.Add(new UsuarioNivelAcessoDTO() { NomeNivelAcesso = "Basico", NomeUsuario = "Rosana", RoleId = "99rrd6f78sfa", UserId = "OO88rrf78sfa" });

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _nivelAcessoMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, _nivelAcessoMock.Object);

            _nivelAcessoMock.Setup(n => n.ListarNivelAcesso());
            //_usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcessoDTO>>(l => l.ListarUsuariosComOuSemNivelAcesso()).Returns(listaUsario);

            var retornoNegocio = _associarUsuarioNegocio.ExcluirNivelAcessoDoUsuario(usuarioId);

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao buscar lista de 'perfil / nível de acesso' para exclusão, entre em contato com o Administrador", retornoNegocio.Mensagem);


        }

        //resultado da busca na fun ListarNivelAcesso com Name == "Administrador" deve ser null
        [Test]
        public void ExcluirNivelAcessoDoUsuario_Result_Func_ListarNivelAcesso_deveRetornaNull_Erro()
        {
            string usuarioId = "sdf41741fds4wRK";

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, _nivelAcessoRepositorioMock.Object);

            List<NivelAcesso> listaBancoNivelAcesso = new List<NivelAcesso>();
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Basico", Id = "111abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Completo", Id = "222abc" });
            //listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Administrador", Id = "333abc" });


            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBancoNivelAcesso);

            var retornoNegocio = _associarUsuarioNegocio.ExcluirNivelAcessoDoUsuario(usuarioId);


            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao buscar 'perfil / nível de acesso' do tipo Administrador, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }

        /* pesquisar fun ListarUsuarioNivelAcesso deve retornar null
        */
        [Test]
        public void ExcluirNivelAcessoDoUsuario_Func_ListarUsuarioNivelAcesso_deveRetornaNull_Erro()
        {
            string usuarioId = "sdf41741fds4wRK";

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, _nivelAcessoRepositorioMock.Object);

            List<NivelAcesso> listaBancoNivelAcesso = new List<NivelAcesso>();
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Basico", Id = "111abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Completo", Id = "222abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Administrador", Id = "333abc" });

            List<UsuarioNivelAcesso> listaBanco = null;


            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBancoNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(u => u.ListarUsuarioNivelAcesso()).Returns(listaBanco);

            var retornoNegocio = _associarUsuarioNegocio.ExcluirNivelAcessoDoUsuario(usuarioId);


            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao buscar lista de Usuários com seu respectivo 'perfil / nível de acesso' para exclusão, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }
                
        /* Resultado da pesquisar fun ListarUsuarioNivelAcesso (que tem somente usuários atrelados ao perfil do tipo 'Administrador') se a lista retornar 1 ou menos 
            E se a mesma lista ListarUsuarioNivelAcesso que só tem tipo 'Administrador' contiver o id do usuário que está sendo editado, deve retornar aviso
        */
        [Test]
        public void ExcluirNivelAcessoDoUsuario_Result_Func_ListarUsuarioNivelAcesso_deveSerVerdadeiro_Aviso()
        {
            string usuarioId = "sdf41741fds4wRK";

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, _nivelAcessoRepositorioMock.Object);

            List<NivelAcesso> listaBancoNivelAcesso = new List<NivelAcesso>();
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Basico", Id = "111abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Completo", Id = "222abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Administrador", Id = "333abc" });

            List<UsuarioNivelAcesso> listaBancoUsuarioNivelAcesso = new List<UsuarioNivelAcesso>();
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "sdf41741fds4wRK", RoleId = "333abc" });

            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBancoNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(u => u.ListarUsuarioNivelAcesso()).Returns(listaBancoUsuarioNivelAcesso);

            var retornoNegocio = _associarUsuarioNegocio.ExcluirNivelAcessoDoUsuario(usuarioId);


            Assert.AreEqual(Tipo.Aviso, retornoNegocio.Tipo);
            Assert.AreEqual("Aviso: Não foi permitindo exclusão pois tem somente 1 usuário no sistema com perfil 'administrador'", retornoNegocio.Mensagem);

        }

        //O usuário selecionado não tem associação com Nível Acesso/Perfil, então deve retornar null
        [Test]
        public void ExcluirNivelAcessoDoUsuario_listaUsuarioNivelAcesso_deveSerNula_Aviso()
        {
            string usuarioId = "sdf41741fds4wRK";

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, _nivelAcessoRepositorioMock.Object);

            List<NivelAcesso> listaBancoNivelAcesso = new List<NivelAcesso>();
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Basico", Id = "111abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Completo", Id = "222abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Administrador", Id = "333abc" });

            List<UsuarioNivelAcesso> listaBancoUsuarioNivelAcesso = new List<UsuarioNivelAcesso>();
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "999D9941fds4wRK", RoleId = "333abc" });

            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBancoNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(u => u.ListarUsuarioNivelAcesso()).Returns(listaBancoUsuarioNivelAcesso);

            var retornoNegocio = _associarUsuarioNegocio.ExcluirNivelAcessoDoUsuario(usuarioId);


            Assert.AreEqual(Tipo.Aviso, retornoNegocio.Tipo);
            Assert.AreEqual("Aviso: Usuário não tem 'perfil / nível de acesso' associado para ser excluído", retornoNegocio.Mensagem);

        }

        //fun SalvarEditarUsuarioNivelAcesso deve retornar false
        [Test]
        public void ExcluirNivelAcessoDoUsuario_Func_SalvarEditarUsuarioNivelAcesso_deveSerFalso_Erro()
        {
            string usuarioId = "sdf41741fds4wRK";

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, _nivelAcessoRepositorioMock.Object);

            List<NivelAcesso> listaBancoNivelAcesso = new List<NivelAcesso>();
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Basico", Id = "111abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Completo", Id = "222abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Administrador", Id = "333abc" });

            List<UsuarioNivelAcesso> listaBancoUsuarioNivelAcesso = new List<UsuarioNivelAcesso>();
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "sdf41741fds4wRK", RoleId = "333abc" });
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "99Rf991fds4wRK", RoleId = "333abc" });

            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBancoNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(u => u.ListarUsuarioNivelAcesso()).Returns(listaBancoUsuarioNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup(u => u.ExcluirUsuarioNivelAcesso(It.IsAny<UsuarioNivelAcesso>())).Returns(false);

            var retornoNegocio = _associarUsuarioNegocio.ExcluirNivelAcessoDoUsuario(usuarioId);


            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao tentar excluir o Usuário com o 'Perfil / Nível Acesso' associado, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }

        [Test]
        public void ExcluirNivelAcessoDoUsuario_Sucesso()
        {
            string usuarioId = "sdf41741fds4wRK";

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _nivelAcessoRepositorioMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, _nivelAcessoRepositorioMock.Object);

            List<NivelAcesso> listaBancoNivelAcesso = new List<NivelAcesso>();
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Basico", Id = "111abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Completo", Id = "222abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Administrador", Id = "333abc" });

            List<UsuarioNivelAcesso> listaBancoUsuarioNivelAcesso = null;

            //- lista de usuários atrelados a perfil 'Administrador' maior que 1

            listaBancoUsuarioNivelAcesso = new List<UsuarioNivelAcesso>();
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "sdf41741fds4wRK", RoleId = "333abc" });
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "99Rf991fds4wRK", RoleId = "333abc" });

            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBancoNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(u => u.ListarUsuarioNivelAcesso()).Returns(listaBancoUsuarioNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup(u => u.ExcluirUsuarioNivelAcesso(It.IsAny<UsuarioNivelAcesso>())).Returns(true);

            var retornoNegocio = _associarUsuarioNegocio.ExcluirNivelAcessoDoUsuario(usuarioId);


            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual("Usuário associado com 'Perfil / Nível Acesso' excluído com sucesso", retornoNegocio.Mensagem);

            //- lista de usuários atrelados a perfil 'Administrador' menor que 2 E parâmetro usuarioId não existe na lista de usuários atrelados ao perfil 'Administrador'

            listaBancoUsuarioNivelAcesso = new List<UsuarioNivelAcesso>();
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "sdf41741fds4wRK", RoleId = "111abc" });
            listaBancoUsuarioNivelAcesso.Add(new UsuarioNivelAcesso() { UserId = "99Rf991fds4wRK", RoleId = "333abc" });

            _nivelAcessoRepositorioMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBancoNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup<List<UsuarioNivelAcesso>>(u => u.ListarUsuarioNivelAcesso()).Returns(listaBancoUsuarioNivelAcesso);
            _usuarioNivelAcessoRepositorioMock.Setup(u => u.ExcluirUsuarioNivelAcesso(It.IsAny<UsuarioNivelAcesso>())).Returns(true);

            retornoNegocio = null;
            retornoNegocio = _associarUsuarioNegocio.ExcluirNivelAcessoDoUsuario(usuarioId);


            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual("Usuário associado com 'Perfil / Nível Acesso' excluído com sucesso", retornoNegocio.Mensagem);

        }

        #endregion

        //ListarNiveisAcesso
        /*
        - fun ListarNiveisAcesso não pode ser nula => ok
        - sucesso ao listar => ok
        */
        #region ListarNiveisAcesso()
        [Test]
        //fun ListarNiveisAcesso deve ser nula
        public void ListarNiveisAcesso_Func_ListarNivelAcesso_DeveRetornarNull_Erro()
        {
            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager).Object, new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())).Object);

            var retornoNegocio = _associarUsuarioNegocio.ListarNiveisAcesso();

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao buscar lista de 'perfil / nível de acesso' para edição, entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        public void ListarNiveisAcesso_Sucesso()
        {
            List<NivelAcesso> listaBancoNivelAcesso = new List<NivelAcesso>();
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Basico", Id = "111abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Completo", Id = "222abc" });
            listaBancoNivelAcesso.Add(new NivelAcesso() { Name = "Administrador", Id = "333abc" });

            var _userManager = new UserManager<Usuario>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
            var _usuarioNivelAcessoRepositorioMock = new Mock<UsuarioNivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()), _userManager);
            var _nivelAcessoMock = new Mock<NivelAcessoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _associarUsuarioNegocio = new AssociarUsuarioNegocio(_usuarioNivelAcessoRepositorioMock.Object, _nivelAcessoMock.Object);


            _nivelAcessoMock.Setup<List<NivelAcesso>>(l => l.ListarNivelAcesso()).Returns(listaBancoNivelAcesso);

            var retornoNegocio = _associarUsuarioNegocio.ListarNiveisAcesso();

            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual("222abc", ((List<NivelAcesso>)retornoNegocio.Objeto).Find(x => x.Name == "Completo").Id);
            Assert.AreEqual(3, ((List<NivelAcesso>)retornoNegocio.Objeto).Count);
        }
        #endregion
    }
}
