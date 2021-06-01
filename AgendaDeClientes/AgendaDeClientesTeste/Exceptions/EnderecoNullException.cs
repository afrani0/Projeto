using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Exceptions
{
    public class EnderecoNullException : Exception
    {
        public EnderecoNullException(string message) : base (message)
        {

        }
    }
}
