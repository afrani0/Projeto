using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Dominio
{
    public class Cidade
    {
        public string Nome { get; set; }
        public Microrregiao Microrregiao { get; set; }

    }

    public class Microrregiao
    {
        public string Nome { get; set; }
    }
}
