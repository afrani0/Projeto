using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Exceptions
{
    public class FuncSalvarArquivoNullException : Exception
    {
        public FuncSalvarArquivoNullException(string message) : base(message)
        {

        }
    }
}
