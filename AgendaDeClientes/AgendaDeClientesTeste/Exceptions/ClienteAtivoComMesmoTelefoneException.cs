using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Exceptions
{
    public class ClienteAtivoComMesmoTelefoneException : Exception
    {
        public ClienteAtivoComMesmoTelefoneException(string message) : base(message)
        {

        }
    }
}
