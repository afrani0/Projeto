using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Exceptions
{
    public class ClienteNaoExisteException : Exception
    {
        public ClienteNaoExisteException(string message) : base(message)
        {

        }
    }
}
