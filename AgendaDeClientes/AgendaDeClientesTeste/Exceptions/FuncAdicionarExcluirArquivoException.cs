using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Exceptions
{
    public class FuncAdicionarExcluirArquivoException : Exception
    {
        public FuncAdicionarExcluirArquivoException(string message) : base(message)
        {

        }
    }
}
