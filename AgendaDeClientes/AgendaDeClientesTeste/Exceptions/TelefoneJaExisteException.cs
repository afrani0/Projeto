using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Exceptions
{
    public class TelefoneJaExisteException : Exception
    {
        public TelefoneJaExisteException(string message) : base(message)
        {
        }
    }
}
