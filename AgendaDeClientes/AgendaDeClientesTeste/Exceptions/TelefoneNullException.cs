using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Exceptions
{
    public class TelefoneNullException : Exception
    {
        public TelefoneNullException(string message) : base(message)
        {
                
        }
    }
}
