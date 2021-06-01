using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Exceptions
{
    public class FotoNullException : Exception
    {
        public FotoNullException(string message) : base (message)
        {
                
        }
    }
}
