using AgendaDeClientes.Exceptions;
using AgendaDeClientes.Models;
using AgendaDeClientes.Repository.Implementation;
using AgendaDeClientes.Services;
using AgendaDeClientes.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NUnitTestAgendaDeCliente
{
    [TestFixture]
    public class ClienteTest
    {
        #region Pesquisar(Cliente cliente)
        [Test]
        public void PesquisarCliente_ListaCliente_Sucesso()
        {
            var _clienteImplementationMock = new Mock<ClienteImplementation>(new AgendaDeClientes.Models.AplicationContext(new DbContextOptions<AplicationContext>()));

            var cliente1 = new Cliente() { Nome = "Anna", Atualizacao = null, Telefone = "4195958685" };
            var cliente2 = new Cliente() { Nome = "Carla", Atualizacao = null, Telefone = "4195958684" };
            var cliente3 = new Cliente() { Nome = "Lana", Atualizacao = null, Telefone = "4195957775" };

            var _lista = new List<Cliente>();
            _lista.Add(cliente1);
            _lista.Add(cliente2);
            _lista.Add(cliente3);

            var lista = _lista.AsQueryable(); 

            _clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.Pesquisar(It.Is<Cliente>(x => x.Nome == "Carla"))).Returns(lista.Where(l => l.Nome.ToUpper().Contains("Carla".ToUpper())));
            _clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.Pesquisar(It.Is<Cliente>(x => x.Nome == "la"))).Returns(lista.Where(l => l.Nome.ToUpper().Contains("la".ToUpper())));
            _clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.Pesquisar(It.Is<Cliente>(x => x.Nome == ""))).Returns(lista.Where(l => l.Nome.ToUpper().Contains("".ToUpper())));


            var listaCliente = _clienteImplementationMock.Object.Pesquisar(new Cliente() { Nome = "Carla" }).ToList();
            Assert.AreEqual(1, listaCliente.Count);
            listaCliente = _clienteImplementationMock.Object.Pesquisar(new Cliente() { Nome = "la" }).ToList();
            Assert.AreEqual(2, listaCliente.Count);
            listaCliente = _clienteImplementationMock.Object.Pesquisar(new Cliente() { Nome = "" }).ToList();
            Assert.AreEqual(3, listaCliente.Count);
            listaCliente = _clienteImplementationMock.Object.Pesquisar(new Cliente() { Nome = "Rincon" }).ToList();
            Assert.AreEqual(0, listaCliente.Count);
        }

        #endregion

        #region Adicionar(Cliente cliente)

        //parâmetro Cliente deve ser null
        [Test]
        public void AdicionarCliente_ClienteNull_Exception()
        {
            var clienteService = new ClienteService(new ClienteImplementation(new AplicationContext(new DbContextOptions<AplicationContext>())), new ManipulacaoDeArquivo());

            Assert.That( ()=> clienteService.Adicionar(null), Throws.TypeOf<ClienteNullException>());
        }

        //cliente.Endereco deve ser null
        [Test]
        public void AdicionarCliente_EnderecoNull_Exception()
        {
            var clienteService = new ClienteService(new ClienteImplementation(new AplicationContext(new DbContextOptions<AplicationContext>())), new ManipulacaoDeArquivo());

            Assert.That(() => clienteService.Adicionar(new Cliente()), Throws.TypeOf<EnderecoNullException>());
        }

        //cliente.Foto deve ser null
        [Test]
        public void AdicionarCliente_FotoNull_Exception()
        {
            var clienteService = new ClienteService(new ClienteImplementation(new AplicationContext(new DbContextOptions<AplicationContext>())), new ManipulacaoDeArquivo());

            Assert.That(() => clienteService.Adicionar(new Cliente() {Endereco = new Endereco() }), Throws.TypeOf<FotoNullException>());
        }

        //cliente.Telefone deve ser null
        [Test]
        public void AdicionarCliente_TelefoneNull_Exception()
        {
            var clienteService = new ClienteService(new ClienteImplementation(new AplicationContext(new DbContextOptions<AplicationContext>())), new ManipulacaoDeArquivo());

            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Arquivo para teste")), 0, 0, "Teste", "teste.txt");

            Assert.That(() => clienteService.Adicionar(new Cliente() {Foto = file, Endereco = new Endereco() }), Throws.TypeOf<TelefoneNullException>());
        }

        //já deve existir telefone e ser ativo
        [Test]
        public void AdicionarCliente_TelefoneExisteEAtivo_Exception()
        {
            var mockClienteImplementation = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));
            var lista = new List<Cliente>();
            var cliente1 = new Cliente() { Nome = "Ricardo" };
            lista.Add(cliente1);
            //atenção: se for passado na função mocada um objeto cliente que não seja setado como "It.IsAny<MeuObjeto>", o mesmo só vai funcionar se usar não somente os mesmos valores do objeto mas também a mesma INSTANCIA
            mockClienteImplementation.Setup(c => c.PesquisarPorTelefone(It.IsAny<Cliente>())).Returns(lista.AsQueryable());
            var clienteService = new ClienteService(mockClienteImplementation.Object, new ManipulacaoDeArquivo());

            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Arquivo para teste")), 0, 0, "Teste", "teste.txt");

            cliente1.Foto = file;
            cliente1.Telefone = "4195859878";
            cliente1.Ativo = true;
            cliente1.Endereco = new Endereco();

            Assert.That(() => clienteService.Adicionar(cliente1), Throws.TypeOf<TelefoneJaExisteException>());
        }

        //cliente.DataNascimento deve ser maior ou igual que data atual
        [Test]
        public void AdicionarCliente_DataNascimentoMaiorOuIgual_Exception()
        {
            var mockClienteImplementation = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));
            var lista = new List<Cliente>();
            var cliente1 = new Cliente() { Nome = "Ricardo" };
            lista.Add(cliente1);

            mockClienteImplementation.Setup(c => c.PesquisarPorTelefone(new Cliente() { Nome = "Sintia"})).Returns(lista.AsQueryable());
            var clienteService = new ClienteService(mockClienteImplementation.Object, new ManipulacaoDeArquivo());

            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Arquivo para teste")), 0, 0, "Teste", "teste.txt");

            cliente1.Foto = file;
            cliente1.Telefone = "4195859878";
            cliente1.Endereco = new Endereco();

            cliente1.DataNascimento = DateTime.Now;

            Assert.That(() => clienteService.Adicionar(cliente1), Throws.TypeOf<DataNascimentoMaiorOuIgualException>());
        }

        //o método ManipulacaoDeArquivo.SalvarArquivo deve retornar null para Cliente.URL
        [Test]
        public void AdicionarCliente_FuncSalvarArquivoNull_Exception()
        {
            //mock ClienteImplementation
            var mockClienteImplementation = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));
            var lista = new List<Cliente>();
            var cliente1 = new Cliente() { Nome = "Ricardo" };
            lista.Add(cliente1);

            //mock ManipulacaoDeArquivo
            var mockManipulacaoDeArquivo = new Mock<ManipulacaoDeArquivo>();
            mockManipulacaoDeArquivo.Setup(m => m.SalvarArquivo(It.IsAny<IFormFile>()));

            mockClienteImplementation.Setup(c => c.PesquisarPorTelefone(new Cliente() { Nome = "Sintia" })).Returns(lista.AsQueryable());
            var clienteService = new ClienteService(mockClienteImplementation.Object, mockManipulacaoDeArquivo.Object);

            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Arquivo para teste")), 0, 0, "Teste", "teste.txt");

            cliente1.Foto = file;
            cliente1.Telefone = "4195859878";
            cliente1.Endereco = new Endereco();

            cliente1.DataNascimento = DateTime.Parse("13/02/2002");

            Assert.That(() => clienteService.Adicionar(cliente1), Throws.TypeOf<FuncSalvarArquivoNullException>());
        }

        //o método Adicionar deve retornar exception somente depois de concluir método ManipulacaoDeArquivo.ExcluirArquivo

        [Test]
        public void AdicionarCliente_FuncAdicionarExcluirArquivo_Exception()
        {
            //mock ClienteImplementation
            var mockClienteImplementation = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));
            var lista = new List<Cliente>();
            var cliente1 = new Cliente() { Nome = "Ricardo" };
            lista.Add(cliente1);

            //mock ManipulacaoDeArquivo
            var mockManipulacaoDeArquivo = new Mock<ManipulacaoDeArquivo>();
            mockManipulacaoDeArquivo.Setup(m => m.SalvarArquivo(It.IsAny<IFormFile>())).Returns("C://");
            mockManipulacaoDeArquivo.Setup(m => m.ExcluirArquivo(It.IsAny<string>())).Returns(false);

            mockClienteImplementation.Setup(c => c.PesquisarPorTelefone(new Cliente() { Nome = "Sintia" })).Returns(lista.AsQueryable());
            mockClienteImplementation.Setup(c => c.Adicionar(It.IsAny<Cliente>())).Throws(new Exception());
            var clienteService = new ClienteService(mockClienteImplementation.Object, mockManipulacaoDeArquivo.Object);

            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Arquivo para teste")), 0, 0, "Teste", "teste.txt");

            cliente1.Foto = file;
            cliente1.Telefone = "4195859878";
            cliente1.Endereco = new Endereco();

            cliente1.DataNascimento = DateTime.Parse("13/02/2002");

            Assert.That(() => clienteService.Adicionar(cliente1), Throws.TypeOf<FuncAdicionarExcluirArquivoException>());
        }

        [Test]
        public void AdicionarCliente_Sucesso()
        {
            //mock ClienteImplementation
            var mockClienteImplementation = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));
            var lista = new List<Cliente>();
            var cliente1 = new Cliente() { Nome = "Ricardo" };
            lista.Add(cliente1);

            //mock ManipulacaoDeArquivo
            var mockManipulacaoDeArquivo = new Mock<ManipulacaoDeArquivo>();
            mockManipulacaoDeArquivo.Setup(m => m.SalvarArquivo(It.IsAny<IFormFile>())).Returns("C://");

            mockClienteImplementation.Setup(c => c.PesquisarPorTelefone(new Cliente() { Nome = "Sintia" })).Returns(lista.AsQueryable());
            mockClienteImplementation.Setup(c => c.Adicionar(It.IsAny<Cliente>()));
            var clienteService = new ClienteService(mockClienteImplementation.Object, mockManipulacaoDeArquivo.Object);

            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Arquivo para teste")), 0, 0, "Teste", "teste.txt");

            cliente1.Foto = file;
            cliente1.Telefone = "4195859878";
            cliente1.Endereco = new Endereco();

            cliente1.DataNascimento = DateTime.Parse("13/02/2002");

            Assert.That(() => clienteService.Adicionar(cliente1), Throws.Nothing);
        }

        #endregion

        #region Pesquisar(int id)
        
        //o método Pesquisar deve ter parametro id zero
        [Test]
        public void PesquisarCliente_ClienteZero_Exception()
        {
            var clienteImplementation = new ClienteImplementation(new AplicationContext(new DbContextOptions<AplicationContext>()));

            var clienteService = new ClienteService(clienteImplementation,new ManipulacaoDeArquivo());
            int id = 0;

            Assert.That(() => clienteService.Pesquisar(id), Throws.Exception);

        }

        //não deve existir um cliente
        [Test]
        public void PesquisarCliente_ClienteNaoExiste_Exception()
        {
            var mockClienteImplementation = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));
            var clienteService = new ClienteService(mockClienteImplementation.Object, new ManipulacaoDeArquivo());

            mockClienteImplementation.Setup(c => c.Pesquisar(It.IsAny<int>()));

            Assert.That(() => clienteService.Pesquisar(4), Throws.TypeOf<ClienteNaoExisteException>());
        }
        [Test]
        public void PesquisarCliente_ClienteIDNaoPodeSerZero_Exception()
        {
            var mockClienteImplementation = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));
            var clienteService = new ClienteService(mockClienteImplementation.Object, new ManipulacaoDeArquivo());

            mockClienteImplementation.Setup(c => c.Pesquisar(It.IsAny<int>()));

            Assert.That(() => clienteService.Pesquisar(0), Throws.TypeOf<ClienteIDNaoPodeSerZeroException>());
        }
        [Test]
        public void PesquisarCliente_Cliente_Sucesso()
        {
            var mockClienteImplementation = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));
            var clienteService = new ClienteService(mockClienteImplementation.Object, new ManipulacaoDeArquivo());

            mockClienteImplementation.Setup<Cliente>(c => c.Pesquisar(4)).Returns(new Cliente());

            Assert.That(() => clienteService.Pesquisar(4), Throws.Nothing);
        }

        #endregion

        #region Inativar(int id)
        //O parâmetro do método Inativar deve ser zero
        [Test]
        public void Inativar_ClienteZero_Exception()
        {
            var clienteServices = new ClienteService(new ClienteImplementation(new AplicationContext(new DbContextOptions<AplicationContext>())), new ManipulacaoDeArquivo());

            Assert.That(() => clienteServices.Inativar(0), Throws.TypeOf<ClienteIDNaoPodeSerZeroException>());

        }
        //não deve existir um cliente
        [Test]
        public void Inativar_CLienteNaoExiste_Exception()
        {
            var _clienteImplementationMock = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));

            _clienteImplementationMock.Setup<Cliente>(c => c.Pesquisar(4)).Returns(new Cliente { ClienteId = 4, Nome = "Santos" });

            var clienteServices = new ClienteService(_clienteImplementationMock.Object, new ManipulacaoDeArquivo());

            Assert.That(() => clienteServices.Inativar(3), Throws.TypeOf<ClienteNaoExisteException>());

        }

        [Test]
        public void Inativar_Sucesso()
        {
            var _clienteImplementationMock = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));
            _clienteImplementationMock.Setup<Cliente>(c => c.Pesquisar(4)).Returns(new Cliente() { ClienteId = 4, Nome = "Joana" });

            var clienteServices = new ClienteService(_clienteImplementationMock.Object, new ManipulacaoDeArquivo());

            Assert.That(() => clienteServices.Inativar(4), Throws.Nothing);
        }

        #endregion

        #region Ativar(int id)
        //método deve ter id do cliente como zero
        [Test]
        public void Ativar_ClienteZero_Exception()
        {
            var clienteImplementation = new ClienteImplementation(new AplicationContext(new DbContextOptions<AplicationContext>()));
            var clienteService = new ClienteService(clienteImplementation, new ManipulacaoDeArquivo());

            Assert.That(() => clienteService.Ativar(0), Throws.TypeOf<ClienteIDNaoPodeSerZeroException>()); 
        }


        //Cliente não deve existir no sistema
        [Test]
        public void Ativar_ClienteNaoExiste_Exception()
        {
            var clienteImplementationMock = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));
            var clienteService = new ClienteService(clienteImplementationMock.Object, new ManipulacaoDeArquivo());

            clienteImplementationMock.Setup(c => c.Pesquisar(2)).Returns(new Cliente() { ClienteId = 2, Nome = "Moacir" });            

            Assert.That(() => clienteService.Ativar(3), Throws.TypeOf<ClienteNaoExisteException>());
        }

        //já deve existir cliente 'ativo' no sistema e com mesmo 'numero de telefone'
        [Test]
        public void Ativar_ClienteAtivoComMesmoTelefone_Exception()
        {
            var clienteImplementationMock = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));
            var clienteService = new ClienteService(clienteImplementationMock.Object, new ManipulacaoDeArquivo());
            //pesquisando cliente que vai ser reativado.
            clienteImplementationMock.Setup(c => c.Pesquisar(2)).Returns(new Cliente() { ClienteId = 2, Nome = "Moacir", Ativo = true, Telefone = "4195957474" });
            //pesquisando lista de clientes que sejam ativos que que algum ja tenha o mesmo numero de telefone
            var clienteRetorno =  new Cliente() { ClienteId = 1, Nome = "Lucrecia", Ativo = true, Telefone = "4195957474" };
            var listaClienteRetorno = new List<Cliente>();
            listaClienteRetorno.Add(clienteRetorno);
            var lista = listaClienteRetorno.AsQueryable();

            clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.PesquisarPorTelefone(It.Is<Cliente>(x => x.Ativo == true && x.Telefone == "4195957474"))).Returns(lista.Where(x => x.Ativo == true && x.Telefone == "4195957474"));

            Assert.That(() => clienteService.Ativar(2), Throws.TypeOf<ClienteAtivoComMesmoTelefoneException>());
        }

        [Test]
        public void Ativar_Sucesso()
        {
            var clienteImplementationMock = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));
            var clienteService = new ClienteService(clienteImplementationMock.Object, new ManipulacaoDeArquivo());
            //pesquisando cliente que vai ser reativado.
            clienteImplementationMock.Setup(c => c.Pesquisar(2)).Returns(new Cliente() { ClienteId = 2, Nome = "Cacia", Ativo = true, Telefone = "4195557474" });
            //pesquisando lista de clientes que sejam ativos que o numero de telefone não seja igual
            var clienteRetorno1 = new Cliente() { ClienteId = 6, Nome = "Lucrecia", Ativo = false, Telefone = "4195557474" };
            var clienteRetorno2 = new Cliente() { ClienteId = 7, Nome = "Anna", Ativo = true, Telefone = "4195956699" };
            var listaClienteRetorno = new List<Cliente>();
            listaClienteRetorno.Add(clienteRetorno1);
            listaClienteRetorno.Add(clienteRetorno2);

            var lista = listaClienteRetorno.AsQueryable();
            //cliente ativo porém numero é diferente
            clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.PesquisarPorTelefone(It.Is<Cliente>(x => x.Telefone == "4195956699"))).Returns(lista.Where(x => x.Telefone == "4195956699"));
            //mesmo numero porém não está ativo
            clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.PesquisarPorTelefone(It.Is<Cliente>(x => x.Telefone == "4195557474"))).Returns(lista.Where(x => x.Telefone == "4195557474"));

            Assert.That(() => clienteService.Ativar(2), Throws.Nothing);
        }

        #endregion

        #region Editar(Cliente cliente)

        //Cliente deve ser null
        [Test]
        public void Editar_ClienteNull_Exception()
        {
            var clienteService = new ClienteService(new ClienteImplementation(new AplicationContext(new DbContextOptions<AplicationContext>())), new ManipulacaoDeArquivo());

            Assert.That(() => clienteService.Editar(null), Throws.TypeOf<ClienteNullException>());

        }
        //Endereço deve ser null
        [Test]
        public void Editar_EnderecoNull_Exception()
        {
            var clienteService = new ClienteService(new ClienteImplementation(new AplicationContext(new DbContextOptions<AplicationContext>())), new ManipulacaoDeArquivo());
            var cliente = new Cliente() { ClienteId = 1, Nome = "Mônica" };

            Assert.That(() => clienteService.Editar(cliente), Throws.TypeOf<EnderecoNullException>());
        }
        //Cliente deve ser zero
        [Test]
        public void Editar_ClienteZero_Exception()
        {
            var clienteService = new ClienteService(new ClienteImplementation(new AplicationContext(new DbContextOptions<AplicationContext>())), new ManipulacaoDeArquivo());

            var cliente = new Cliente() { ClienteId = 0, Nome = "Mônica" };
            cliente.Endereco = new Endereco() { EnderecoId = 0 };

            Assert.That(() => clienteService.Editar(cliente), Throws.TypeOf<ClienteIDNaoPodeSerZeroException>());

        }
        //Endereço deve ser zero
        [Test]
        public void Editar_EnderecoZero_Exception()
        {
            var clienteService = new ClienteService(new ClienteImplementation(new AplicationContext(new DbContextOptions<AplicationContext>())), new ManipulacaoDeArquivo());
            var cliente = new Cliente() { ClienteId = 1, Nome = "Mônica" };
            cliente.Endereco = new Endereco() { EnderecoId = 0 };

            Assert.That(() => clienteService.Editar(cliente), Throws.TypeOf<EnderecoIdNaoPodeSerZeroException>());
        }
        //Foto deve vir vazia
        [Test]
        public void Editar_FotoNull_Exception()
        {
            var clienteService = new ClienteService(new ClienteImplementation(new AplicationContext(new DbContextOptions<AplicationContext>())),new ManipulacaoDeArquivo());
            var cliente = new Cliente() { ClienteId = 1, Nome = "Mônica" };
            cliente.Endereco = new Endereco() { EnderecoId = 2 };
            cliente.EnderecoId = cliente.Endereco.EnderecoId;

            Assert.That(() => clienteService.Editar(cliente), Throws.TypeOf<FotoNullException>());
        }

        //Telefone já deve existir em cliente ativo da base
        [Test]
        public void Editar_TelefoneExisteEAtivo_Exception()
        {
            var clienteImplementationMock = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));
            var clienteService = new ClienteService(clienteImplementationMock.Object, new ManipulacaoDeArquivo());
            
            //Base de clientes
            var cliente1 = new Cliente() { ClienteId = 2, Telefone = "45963741852", Ativo = true, Foto = new FormFile(new MemoryStream(), 0, 0, "Teste", "Teste.gif"), EnderecoId = 1 , Endereco = new Endereco() };
            //cliente que terá o numero modificado
            var cliente2 = new Cliente() { ClienteId = 3, Telefone = "45963741811", Ativo = true, Foto = new FormFile(new MemoryStream(), 0, 0, "Teste", "Teste.gif"), EnderecoId = 2, Endereco = new Endereco() };

            var lista = new List<Cliente>();
            lista.Add(cliente1);
            lista.Add(cliente2);
            var listaBase = lista.AsQueryable();

            clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.PesquisarPorTelefone(It.Is<Cliente>(i => i.Telefone == "45963741852"))).Returns(listaBase.Where(x => x.Telefone.Contains("45963741852")));
            clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.PesquisarPorTelefone(It.Is<Cliente>(i => i.Telefone == "45963741811"))).Returns(listaBase.Where(x => x.Telefone.Contains("45963741811")));

            //substituindo numero de telefone
            cliente2.Telefone = "45963741852";
            Assert.That(() => clienteService.Editar(cliente2), Throws.TypeOf<TelefoneJaExisteException>());
        }


        //Cliente.DataNascimento deve ser maior ou igual a data de hoje
        [Test]
        public void Editar_DataNascimentoMaiorOuIgual_Exception()
        {
            var clienteImplementationMock = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));

            var clienteService = new ClienteService(clienteImplementationMock.Object, new ManipulacaoDeArquivo());
            //Base de clientes
            var cliente1 = new Cliente() { ClienteId = 1, Nome = "Carol", Telefone = "41959585856", Ativo = true, Foto = new FormFile(new MemoryStream(), 0, 0, "Text", "Text.gif"), EnderecoId = 1, Endereco = new Endereco() };
            var cliente2 = new Cliente() { ClienteId = 2, Nome = "Irineu", Telefone = "41959585811", Ativo = true, Foto = new FormFile(new MemoryStream(), 0, 0, "Text", "Text.gif"), EnderecoId = 2,Endereco = new Endereco() };

            var lista = new List<Cliente>();
            lista.Add(cliente1);
            lista.Add(cliente2);
            var listaBase = lista.AsQueryable();

            clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.PesquisarPorTelefone(It.Is<Cliente>(i => i.Telefone == "41959585856"))).Returns(listaBase.Where(x => x.Telefone == "41959585856"));
            clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.PesquisarPorTelefone(It.Is<Cliente>(i => i.Telefone == "41959585811"))).Returns(listaBase.Where(x => x.Telefone == "41959585811"));

            cliente1.Nome = "Sintia";
            cliente1.DataNascimento = DateTime.Now;

            Assert.That(() => clienteService.Editar(cliente1), Throws.TypeOf<DataNascimentoMaiorOuIgualException>());
        }

        //O método ManipulacaoDeArquivo.SalvarArquivo deve retornar null para Cliente.URL
        [Test]
        public void Editar_FuncSalvarArquivoNull_Exception()
        {
            var clienteImplementationMock = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));
            var manipulacaoDeArquivoMock = new Mock<ManipulacaoDeArquivo>();
            //simula um arquivo voltando nulo
            manipulacaoDeArquivoMock.Setup(m => m.SalvarArquivo(It.IsAny<IFormFile>()));

            var clienteService = new ClienteService(clienteImplementationMock.Object, manipulacaoDeArquivoMock.Object);
            //Base de clientes
            var cliente1 = new Cliente() { ClienteId = 1, Nome = "Carol", Telefone = "41959585856", Ativo = true, Foto = new FormFile(new MemoryStream(), 0, 0, "Text", "Text.gif"),EnderecoId = 1, Endereco = new Endereco() };
            var cliente2 = new Cliente() { ClienteId = 2, Nome = "Irineu", Telefone = "41959585811", Ativo = true, Foto = new FormFile(new MemoryStream(), 0, 0, "Text", "Text.gif"),EnderecoId = 2, Endereco = new Endereco() };

            var lista = new List<Cliente>();
            lista.Add(cliente1);
            lista.Add(cliente2);
            var listaBase = lista.AsQueryable();

            clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.PesquisarPorTelefone(It.Is<Cliente>(i => i.Telefone == "41959585856"))).Returns(listaBase.Where(x => x.Telefone == "41959585856"));
            clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.PesquisarPorTelefone(It.Is<Cliente>(i => i.Telefone == "41959585811"))).Returns(listaBase.Where(x => x.Telefone == "41959585811"));

            cliente1.Nome = "Sintia";
            cliente1.DataNascimento = DateTime.Parse("1999/09/02");

            Assert.That(() => clienteService.Editar(cliente1), Throws.TypeOf<FuncSalvarArquivoNullException>());
        }
        //O método Editar deve retornar exception somente depois de concluir método ManipulacaoDeArquivo.ExcluirArquivo
        [Test]
        public void Editar_FuncAdicionar_Exception()
        {
            var clienteImplementationMock = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));
            var manipulacaoDeArquivoMock = new Mock<ManipulacaoDeArquivo>();
            //simula um arquivo voltando nulo
            manipulacaoDeArquivoMock.Setup<string>(m => m.SalvarArquivo(It.IsAny<IFormFile>())).Returns("C://");

            var clienteService = new ClienteService(clienteImplementationMock.Object, manipulacaoDeArquivoMock.Object);
            //Base de clientes
            var cliente1 = new Cliente() { ClienteId = 1, Nome = "Carol", Telefone = "41959585856", Ativo = true, Foto = new FormFile(new MemoryStream(), 0, 0, "Text", "Text.gif"), EnderecoId = 1, Endereco = new Endereco() };
            var cliente2 = new Cliente() { ClienteId = 2, Nome = "Irineu", Telefone = "41959585811", Ativo = true, Foto = new FormFile(new MemoryStream(), 0, 0, "Text", "Text.gif"), EnderecoId = 2, Endereco = new Endereco() };

            var lista = new List<Cliente>();
            lista.Add(cliente1);
            lista.Add(cliente2);
            var listaBase = lista.AsQueryable();

            clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.PesquisarPorTelefone(It.Is<Cliente>(i => i.Telefone == "41959585856"))).Returns(listaBase.Where(x => x.Telefone == "41959585856"));
            clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.PesquisarPorTelefone(It.Is<Cliente>(i => i.Telefone == "41959585811"))).Returns(listaBase.Where(x => x.Telefone == "41959585811"));

            clienteImplementationMock.Setup(c => c.Editar(It.IsAny<Cliente>())).Throws(new Exception());

            cliente1.Nome = "Sintia";
            cliente1.DataNascimento = DateTime.Parse("1999/09/02");

            Assert.That(() => clienteService.Editar(cliente1), Throws.TypeOf<FuncAdicionarExcluirArquivoException>());
        }

        //O sistema deve distinguir que o mesmo cliente da base e o editado são os mesmos, ainda que o 'Telefone' também seja igual ao da base
        [Test]
        public void Editar_MudarDadosDeOutrosCamposMenosCampoTelefone_Sucesso()
        {
            var clienteImplementationMock = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));

            var manipulacaoDeArquivoMock = new Mock<ManipulacaoDeArquivo>();
            //simula um arquivo voltando nulo
            manipulacaoDeArquivoMock.Setup<string>(m => m.SalvarArquivo(It.IsAny<IFormFile>())).Returns("C://");

            var clienteService = new ClienteService(clienteImplementationMock.Object, manipulacaoDeArquivoMock.Object);
            
            //Base de clientes
            var cliente1 = new Cliente() { ClienteId = 2, Telefone = "45963741852", Ativo = true, Foto = new FormFile(new MemoryStream(), 0, 0, "Teste", "Teste.gif"), EnderecoId = 4, Endereco = new Endereco() { EnderecoId = 4 } };
            //cliente que terá o numero modificado
            var cliente2 = new Cliente() { ClienteId = 3, Telefone = "45963741811", Ativo = true, Foto = new FormFile(new MemoryStream(), 0, 0, "Teste", "Teste.gif"), EnderecoId = 6, Endereco = new Endereco() { EnderecoId = 6 } };

            var lista = new List<Cliente>();
            lista.Add(cliente1);
            lista.Add(cliente2);
            var listaBase = lista.AsQueryable();

            clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.PesquisarPorTelefone(It.Is<Cliente>(i => i.Telefone == "45963741852"))).Returns(listaBase.Where(x => x.Telefone.Contains("45963741852")));
            clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.PesquisarPorTelefone(It.Is<Cliente>(i => i.Telefone == "45963741811"))).Returns(listaBase.Where(x => x.Telefone.Contains("45963741811")));
            //Mudança de alguns dados menos o telefone 
            cliente1.Nome = "Sintia";
            cliente1.DataNascimento = DateTime.Parse("1999/09/02");

            Assert.That(() => clienteService.Editar(cliente1), Throws.Nothing);
        }

        [Test]
        public void Editar_Sucesso()
        {
            var clienteImplementationMock = new Mock<ClienteImplementation>(new AplicationContext(new DbContextOptions<AplicationContext>()));

            var manipulacaoDeArquivoMock = new Mock<ManipulacaoDeArquivo>();
            //simula um arquivo voltando nulo
            manipulacaoDeArquivoMock.Setup<string>(m => m.SalvarArquivo(It.IsAny<IFormFile>())).Returns("C://");

            var clienteService = new ClienteService(clienteImplementationMock.Object, manipulacaoDeArquivoMock.Object);

            //Base de clientes
            var cliente1 = new Cliente() { ClienteId = 2, Telefone = "45963741852", Ativo = true, Foto = new FormFile(new MemoryStream(), 0, 0, "Teste", "Teste.gif"), EnderecoId = 4, Endereco = new Endereco() { EnderecoId = 4 } };
            //cliente que terá o numero modificado
            var cliente2 = new Cliente() { ClienteId = 3, Telefone = "45963741811", Ativo = true, Foto = new FormFile(new MemoryStream(), 0, 0, "Teste", "Teste.gif"), EnderecoId = 6, Endereco = new Endereco() { EnderecoId = 6 } };

            var lista = new List<Cliente>();
            lista.Add(cliente1);
            lista.Add(cliente2);
            var listaBase = lista.AsQueryable();

            clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.PesquisarPorTelefone(It.Is<Cliente>(i => i.Telefone == "45963741852"))).Returns(listaBase.Where(x => x.Telefone.Contains("45963741852")));
            clienteImplementationMock.Setup<IQueryable<Cliente>>(c => c.PesquisarPorTelefone(It.Is<Cliente>(i => i.Telefone == "45963741811"))).Returns(listaBase.Where(x => x.Telefone.Contains("45963741811")));
            //Mudança de alguns dados juntamente com o telefone 
            cliente1.Nome = "Sintia";
            cliente1.DataNascimento = DateTime.Parse("1999/09/02");
            cliente1.Telefone = "45999741111";

            Assert.That(() => clienteService.Editar(cliente1), Throws.Nothing);
        }

        #endregion

    }
}
