using ListaDeContatos.Enumerador;
using ListaDeContatos.Models;
using ListaDeContatos.Negocio.Implementacao;
using ListaDeContatos.Repositorio.Implementacao;
using ListaDeContatos.Util;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestesUnitarios
{
    [TestFixture]
    class ContatosTest
    {

        //Listar Contatos
        /*
         - lista não pode ser nula => ok
         - sucesso ao listar => ok         
         */
        #region ListarContatos()

        [Test]
        //
        public void ListarContatos_Func_ListarContatos_Erro()
        {

            var _contatoRepositorioMock = new Mock<ContatoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            _contatoRepositorioMock.Setup(l => l.ListarContatos());

            var contatoNegocio = new ContatoNegocio(_contatoRepositorioMock.Object);

            var retornoNegocio = contatoNegocio.ListarContatos();

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao buscar lista de contatos, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }

        [Test]
        //
        public void ListarContatos_Sucesso()
        {
            var contato1 = new Contato() { Telefone = "74555588749" };
            var contato2 = new Contato() { Telefone = "41529566899" };

            //lista de contatos no banco
            List<Contato> listaContatos = new List<Contato>();
            listaContatos.Add(contato1);
            listaContatos.Add(contato2);

            var _contatoRepositorioMock = new Mock<ContatoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            _contatoRepositorioMock.Setup<List<Contato>>(l => l.ListarContatos()).Returns(listaContatos);

            var contatoNegocio = new ContatoNegocio(_contatoRepositorioMock.Object);

            var retornoNegocio = contatoNegocio.ListarContatos();

            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual(listaContatos.Count, ((List<Contato>)retornoNegocio.Objeto).Count);
            var listaRetornoContatos = ((List<Contato>)retornoNegocio.Objeto);
            Assert.AreEqual(contato1, listaRetornoContatos.Find(x => x.Telefone == "74555588749"));
            Assert.AreEqual(contato2, listaRetornoContatos.Find(x => x.Telefone == "41529566899"));

        }

        #endregion


        //Criar/Salvar Contatos
        /*
         - contato não pode ser null => ok
         - contato telefone não pode ser null ou vazio => ok
         - contato não pode ser salvo quando já existir um outro contato com mesmo telefone no banco => ok         
         - data de nascimento não pode ser maior que data de hoje => ok
        - sucesso criar contato => ok
        */
        #region SalvarContatos(Contato contato)

        [Test]
        //Contato devem ser nulo
        public void CriarContatos_ContatoDeveSerNull_Erro()
        {
            Contato contato = null;

            var contatoNegocio = new ContatoNegocio(new ContatoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            var retornoNegocio = contatoNegocio.CriarContato(contato);

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Contato não pode ser nulo, entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        //a propriedade Telefone do objeto Contato devem ser nulo ou vazio
        public void CriarContatos_ContatoTelefoneDeveSerNullOuVazio_Erro()
        {
            Contato contato = new Contato() { Telefone = null };

            var contatoNegocio = new ContatoNegocio(new ContatoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            var retornoNegocio = contatoNegocio.CriarContato(contato);

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Telefone não pode ser nulo ou vazio, entre em contato com o Administrador", retornoNegocio.Mensagem);

            contato = null;
            contato = new Contato() { Telefone = "  " };

            retornoNegocio = null;
            retornoNegocio = contatoNegocio.CriarContato(contato);

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Telefone não pode ser nulo ou vazio, entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        //Função ListarContatos deve retornar null
        public void CriarContatos_Fun_ListarContatos_DeveRetornarNull_Erro()
        {
            //novo contato
            Contato contato = new Contato() { Telefone = "41529566887" };

            var _contatoRepositorioMock = new Mock<ContatoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            _contatoRepositorioMock.Setup(l => l.ListarContatos());

            var contatoNegocio = new ContatoNegocio(_contatoRepositorioMock.Object);

            var retornoNegocio = contatoNegocio.CriarContato(contato);

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Ocorreu um erro ao buscar lista de contatos, entre em contato com o Administrador", retornoNegocio.Mensagem);


        }

        [Test]
        //o telefone do contato a ser salvo deve ser igual a um telefone de um contato já existente no banco
        public void CriarContatos_ContatoTelefoneJaDeveExistir_Aviso()
        {
            //novo contato
            Contato contato = new Contato() { Telefone = "41529566887" };

            //lista de contatos no banco
            List<Contato> listaContatos = new List<Contato>();
            listaContatos.Add(new Contato() { Telefone = "74555588749" });
            listaContatos.Add(new Contato() { Telefone = "41529566887" });

            var _contatoRepositorioMock = new Mock<ContatoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            _contatoRepositorioMock.Setup<List<Contato>>(l => l.ListarContatos()).Returns(listaContatos);

            var contatoNegocio = new ContatoNegocio(_contatoRepositorioMock.Object);

            var retornoNegocio = contatoNegocio.CriarContato(contato);

            Assert.AreEqual(Tipo.Aviso, retornoNegocio.Tipo);
            Assert.AreEqual("Aviso: Já existe 'Telefone' com esse valor, só pode ser inserido 'Telefone' que ainda não exista no sistema", retornoNegocio.Mensagem);

        }

        [Test]
        //a data de nascimento do contato deve ser maior que a data de hoje
        public void CriarContatos_DataNascimentoDeveSerMaiorQueAtual_Aviso()
        {
            //novo contato
            Contato contato = new Contato() { Telefone = "41529566887", DataNascimento = DateTime.Now.AddDays(3) };

            //lista de contatos no banco
            List<Contato> listaContatos = new List<Contato>();
            listaContatos.Add(new Contato() { Telefone = "74555588749" });
            listaContatos.Add(new Contato() { Telefone = "41529566899" });

            var _contatoRepositorioMock = new Mock<ContatoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            _contatoRepositorioMock.Setup<List<Contato>>(l => l.ListarContatos()).Returns(listaContatos);

            var contatoNegocio = new ContatoNegocio(_contatoRepositorioMock.Object);

            var retornoNegocio = contatoNegocio.CriarContato(contato);

            Assert.AreEqual(Tipo.Aviso, retornoNegocio.Tipo);
            Assert.AreEqual("Aviso: A 'Data de Nascimento' não pode ser maior que a data de hoje", retornoNegocio.Mensagem);

        }

        [Test]
        public void CriarContatos_Sucesso()
        {
            //novo contato
            Contato contato = new Contato() { Telefone = "41529566887", DataNascimento = new DateTime(2005, 05, 12) };

            //lista de contatos no banco
            List<Contato> listaContatos = new List<Contato>();
            listaContatos.Add(new Contato() { Telefone = "74555588749" });
            listaContatos.Add(new Contato() { Telefone = "41529566899" });

            var _contatoRepositorioMock = new Mock<ContatoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            _contatoRepositorioMock.Setup<List<Contato>>(l => l.ListarContatos()).Returns(listaContatos);
            _contatoRepositorioMock.Setup(l => l.AdicionarContato(contato));

            var contatoNegocio = new ContatoNegocio(_contatoRepositorioMock.Object);

            var retornoNegocio = contatoNegocio.CriarContato(contato);

            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual("Contato salvo com sucesso", retornoNegocio.Mensagem);

        }

        #endregion


        //Editar Contatos
        /*
        - contato não pode ser null => ok
        - contato id não pode ser zero => ok
        - contato telefone não pode ser null ou vazio => ok
        - data de nascimento não pode ser maior que data de hoje => ok
        - contato editado deve existir no banco => ok
        - contato não pode ser editado se o numero de telefone for mudado para um numero de telefone que já existe no banco => ok
        
        - sucesso editar contato => ok   
        */
        #region EditarContatos(Contato contato)

        [Test]
        //O parâmetro Contato deve ser null
        public void EditarContatos_ContatoDeveSerNull_Erro()
        {
            Contato contato = null;

            var _contatoNegocio = new ContatoNegocio(new ContatoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));
            var retornoNegocio = _contatoNegocio.EditarContato(contato);

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: O Contato não pode ser nulo, entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        //O id do parâmetro Contato deve ser zero
        public void EditarContatos_ContatoIdDeveSerNullOuVazio_Erro()
        {
            Contato contato = new Contato() { ContatoId = 0 };

            var _contatoNegocio = new ContatoNegocio(new ContatoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));
            var retornoNegocio = _contatoNegocio.EditarContato(contato);

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: O Id do Contato não pode ser zero, entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        //O telefone do parâmetro Contato deve ser null ou vazio
        public void EditarContatos_ContatoTelefoneDeveSerNullOuVazio_Erro()
        {
            Contato contato = new Contato() { ContatoId = 12, Telefone = "" };

            var _contatoNegocio = new ContatoNegocio(new ContatoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            var retornoNegocio = _contatoNegocio.EditarContato(contato);

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Telefone não pode ser nulo ou vazio, entre em contato com o Administrador", retornoNegocio.Mensagem);

            contato = null;
            contato = new Contato() { ContatoId = 12 , Telefone = null };
            retornoNegocio = null;
            retornoNegocio = _contatoNegocio.EditarContato(contato);

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: Telefone não pode ser nulo ou vazio, entre em contato com o Administrador", retornoNegocio.Mensagem);
        }

        [Test]
        //A data de nascimento deve ser maior que a data de hoje
        public void EditarContatos_ContatoDataNascimentoDeveSerMaiorQueDataAtual_Aviso()
        {
            Contato contato = new Contato() { ContatoId = 12, Telefone = "4295874455", DataNascimento = DateTime.Now.Date.AddDays(2) };

            var _contatoNegocio = new ContatoNegocio(new ContatoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));
            var retornoNegocio = _contatoNegocio.EditarContato(contato);

            Assert.AreEqual(Tipo.Aviso, retornoNegocio.Tipo);
            Assert.AreEqual("Aviso: A 'Data de Nascimento' não pode ser maior que a data de hoje", retornoNegocio.Mensagem);
        }

        [Test]
        //O Contato que está sendo editado não deve existir no banco
        public void EditarContatos_Func_BuscarContatoPorId_DeveRetornarContatoNull_Erro()
        {
            //lista de contatos no banco
            List<Contato> listaBanco = new List<Contato>();
            listaBanco.Add(new Contato() { ContatoId = 21, Telefone = "4295879905", DataNascimento = new DateTime(1997, 01, 18).Date });

            Contato contato = new Contato() { ContatoId = 12, Telefone = "4295874455", DataNascimento = new DateTime(2000,11,18).Date };

            var _contatoRepositorioMock = new Mock<ContatoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var _contatoNegocio = new ContatoNegocio(_contatoRepositorioMock.Object);

            _contatoRepositorioMock.Setup<Contato>(c => c.BuscarContatoPorId(contato.ContatoId)).Returns(listaBanco. Find(f => f.ContatoId ==contato.ContatoId));
            var retornoNegocio = _contatoNegocio.EditarContato(contato);

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: O contato não existe no sistema", retornoNegocio.Mensagem);
        }

        [Test]
        //O número do telefone do contato que está sendo editado deve ser mudado para um numero de telefone igual de outro contato salvo no banco
        public void EditarContatos_ContatoTelefoneJaDeveExistir_Aviso()
        {
            //contato editado
            Contato contato = new Contato() { ContatoId = 12, Telefone = "4295874455", DataNascimento = new DateTime(2000, 11, 18).Date };

            //lista de contatos no banco
            List<Contato> listaBanco = new List<Contato>();
            listaBanco.Add(new Contato() { ContatoId = 21, Telefone = "4295874455", DataNascimento = new DateTime(1997, 01, 18).Date });
            listaBanco.Add(contato);

            var _contatoRepositorioMock = new Mock<ContatoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var _contatoNegocio = new ContatoNegocio(_contatoRepositorioMock.Object);

            _contatoRepositorioMock.Setup<Contato>(c => c.BuscarContatoPorId(contato.ContatoId)).Returns(listaBanco.Find(f => f.ContatoId == contato.ContatoId));
            _contatoRepositorioMock.Setup<List<Contato>>(c => c.ListarContatos()).Returns(listaBanco);
            var retornoNegocio = _contatoNegocio.EditarContato(contato);

            Assert.AreEqual(Tipo.Aviso, retornoNegocio.Tipo);
            Assert.AreEqual("Aviso: Já existe outro contato com esse numero de telefone , só pode ser mudado para um numero de telefone que ainda não exista no sistema", retornoNegocio.Mensagem);
        }

        [Test]
        public void EditarContatos_Sucesso()
        {
            //contato editado
            Contato contato = new Contato() { ContatoId = 12, Telefone = "4295874455", DataNascimento = new DateTime(2000, 11, 18).Date };

            //lista de contatos no banco
            List<Contato> listaBanco = new List<Contato>();
            listaBanco.Add(new Contato() { ContatoId = 21, Telefone = "4295874400", DataNascimento = new DateTime(1997, 01, 18).Date });
            listaBanco.Add(contato);

            var _contatoRepositorioMock = new Mock<ContatoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var _contatoNegocio = new ContatoNegocio(_contatoRepositorioMock.Object);

            _contatoRepositorioMock.Setup<Contato>(c => c.BuscarContatoPorId(contato.ContatoId)).Returns(listaBanco.Find(f => f.ContatoId == contato.ContatoId));
            _contatoRepositorioMock.Setup<List<Contato>>(c => c.ListarContatos()).Returns(listaBanco);
            _contatoRepositorioMock.Setup(c => c.EditarContato(contato));

            var retornoNegocio = _contatoNegocio.EditarContato(contato);

            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual("Contato editado com sucesso", retornoNegocio.Mensagem);
        }

        #endregion


        //Buscar Contato
        /*
        - id não pode ser null ou vazio => ok
        - id não pode ser zero => ok
        - contato carregado não pode ser null => ok
        - sucesso buscar contato => ok
        */
        #region BuscarContato(string id)

        [Test]        
        public void BuscarContato_IdDeveSerNullOuVazio_Erro()
        {
            string ContatoId = null;

            var _ContatoNegocio = new ContatoNegocio(new ContatoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            var retornoContato = _ContatoNegocio.BuscarContato(ContatoId);

            Assert.AreEqual(Tipo.Erro, retornoContato.Tipo);
            Assert.AreEqual("Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador", retornoContato.Mensagem);

            ContatoId = "";

            _ContatoNegocio.BuscarContato(ContatoId);

            Assert.AreEqual(Tipo.Erro, retornoContato.Tipo);
            Assert.AreEqual("Erro: Id não pode ser nulo ou vazio, entre em contato com o Administrador", retornoContato.Mensagem);
        }

        [Test]
        public void BuscarContato_IdDeveSerZero_Erro()
        {
            string ContatoId = "0";

            var _ContatoNegocio = new ContatoNegocio(new ContatoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            var retornoContato = _ContatoNegocio.BuscarContato(ContatoId);

            Assert.AreEqual(Tipo.Erro, retornoContato.Tipo);
            Assert.AreEqual("Erro: Id não pode ser zero, entre em contato com o Administrador", retornoContato.Mensagem);
                        
        }

        [Test]
        //A função BuscarContatoPorId deve retornar null

        public void BuscarContato_Func_BuscarContatoPorId_DeveRetornarNull_Aviso()
        {
            var IdNaoExiste = 11;

            var Contato = new Contato() { ContatoId = 15, Nome = "Renata Silveira" };

            List<Contato> lista = new List<Contato>();
            lista.Add(Contato);
            lista.Add(new Contato() { ContatoId = 17, Nome = "Bruna Gaia" });

            var _contatoRepositorioMock = new Mock<ContatoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var _ContatoNegocio = new ContatoNegocio(_contatoRepositorioMock.Object);
            _contatoRepositorioMock.Setup<Contato>(c => c.BuscarContatoPorId(IdNaoExiste)).Returns(lista.Find(x => x.ContatoId == IdNaoExiste));

            var retornoContato = _ContatoNegocio.BuscarContato(Contato.ContatoId.ToString());

            Assert.AreEqual(Tipo.Aviso, retornoContato.Tipo );
            Assert.AreEqual("Aviso: O contato pode ter sido excluído anteriormente ou não existe no sistema", retornoContato.Mensagem);
            
        }

        [Test]
        //O objeto Contato deve ser nulo
        public void BuscarContato_Sucesso()
        {
            var Contato = new Contato() { ContatoId = 15, Nome = "Renata Silveira" };

            List<Contato> lista = new List<Contato>();
            lista.Add(Contato);
            lista.Add(new Contato() {  ContatoId = 17, Nome = "Bruna Gaia"});

            var _contatoRepositorioMock = new Mock<ContatoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));

            var _ContatoNegocio = new ContatoNegocio(_contatoRepositorioMock.Object);
            _contatoRepositorioMock.Setup<Contato>(c => c.BuscarContatoPorId(Contato.ContatoId)).Returns(lista.Find(x => x.ContatoId == Contato.ContatoId));

            var retornoContato = _ContatoNegocio.BuscarContato(Contato.ContatoId.ToString());

            Assert.AreEqual(Tipo.Sucesso, Tipo.Sucesso);
            Assert.AreEqual(Contato.Nome, ((Contato)retornoContato.Objeto).Nome);
            Assert.AreEqual(Contato.ContatoId, ((Contato)retornoContato.Objeto).ContatoId);
        }

        #endregion


        //Deletar Contatos
        /*
        - id não pode ser null ou vazio
        - contato a ser deletado deve existir no banco

        - sucesso ao excluir contato
        
         */
        #region DeletarContatos(string id)
        [Test]
        public void DeletarContato_IdDeveSerZero_Erro()
        {
            int id = 0;
            var _ContatoNegocio = new ContatoNegocio(new ContatoRepositorio(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>())));

            var retornoNegocio = _ContatoNegocio.DeletarContato(id);

            Assert.AreEqual(Tipo.Erro, retornoNegocio.Tipo);
            Assert.AreEqual("Erro: O Id do Contato não pode ser zero, entre em contato com o Administrador", retornoNegocio.Mensagem);

        }

        //A func BuscarContato deve retornar nenhum contato.
        [Test]
        public void DeletarContato_Fun_BuscarContato_DeveRetornarNull_Erro()
        {
            List<Contato> ListaBanco = new List<Contato>();
            ListaBanco.Add(new Contato() { ContatoId = 4});
            ListaBanco.Add(new Contato() { ContatoId = 5});

            int id = 6;
            var _ContatoRespositorio = new Mock<ContatoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _ContatoNegocio = new ContatoNegocio(_ContatoRespositorio.Object);

            _ContatoRespositorio.Setup<Contato>(c => c.BuscarContatoPorId(id)).Returns(ListaBanco.Find(f => f.ContatoId == id));

            var retornoNegocio = _ContatoNegocio.DeletarContato(id);

            Assert.AreEqual(Tipo.Aviso, retornoNegocio.Tipo);
            Assert.AreEqual("Aviso: O contato pode ter sido excluído anteriormente ou não existe no sistema", retornoNegocio.Mensagem);
        }

        [Test]
        public void DeletarContato_Sucesso()
        {

            List<Contato> ListaBanco = new List<Contato>();
            ListaBanco.Add(new Contato() { ContatoId = 4 });
            ListaBanco.Add(new Contato() { ContatoId = 5 });

            int id = 5;
            var _ContatoRespositorio = new Mock<ContatoRepositorio>(new ApplicationDBContext(new DbContextOptions<ApplicationDBContext>()));
            var _ContatoNegocio = new ContatoNegocio(_ContatoRespositorio.Object);

            _ContatoRespositorio.Setup<Contato>(c => c.BuscarContatoPorId(id)).Returns(ListaBanco.Find(f => f.ContatoId == id));

            var retornoNegocio = _ContatoNegocio.DeletarContato(id);

            Assert.AreEqual(Tipo.Sucesso, retornoNegocio.Tipo);
            Assert.AreEqual("Contato deletado com sucesso", retornoNegocio.Mensagem);

        }


        #endregion


    }
}
