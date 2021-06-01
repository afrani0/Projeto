using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Exceptions
{
    public class DataNascimentoMaiorOuIgualException : Exception
    {
        public DataNascimentoMaiorOuIgualException(string message) : base (message)
        {

        }
    }
}
