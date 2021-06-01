using AgendaDeClientes.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NUnitTestAgendaDeCliente
{
    [TestFixture]
    public class UtilTest
    {
        #region SalvarArquivo
        [Test]
        public void SalvarArquivoUtil_Sucesso()
        {
            var manipulacaoDeArquivo = new Mock<ManipulacaoDeArquivo>();
            manipulacaoDeArquivo.Setup<string>(m => m.SalvarArquivo(It.IsAny<IFormFile>())).Returns("c://");

            var file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Arquivo para teste")),0,0,"Test","Test.txt");

            Assert.That(() => manipulacaoDeArquivo.Object.SalvarArquivo(file), Throws.Nothing);
        }
        #endregion

        #region ExcluirArquivo
        [Test]
        public void ExcluirArquivoUtil_Sucesso()
        {
            var manipulacaoDeArquivo = new Mock<ManipulacaoDeArquivo>();
            manipulacaoDeArquivo.Setup(m => m.ExcluirArquivo(It.IsAny<string>()));

            var file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Arquivo para teste")), 0, 0, "Test", "Test.txt");

            Assert.That(() => manipulacaoDeArquivo.Object.ExcluirArquivo("c://"), Throws.Nothing);
        }
        
        #endregion
    }
}
