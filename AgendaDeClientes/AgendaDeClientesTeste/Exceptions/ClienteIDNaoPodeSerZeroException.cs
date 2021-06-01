using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Exceptions
{
    public class ClienteIDNaoPodeSerZeroException : Exception
    {
        public ClienteIDNaoPodeSerZeroException(string message) : base(message)
        {
                
        }
    }
}
