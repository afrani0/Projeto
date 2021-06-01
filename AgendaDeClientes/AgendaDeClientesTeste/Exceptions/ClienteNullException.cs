using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Exceptions
{
    public class ClienteNullException : Exception
    {
        public ClienteNullException(string message) : base(message)
        {

        }
    }
}
