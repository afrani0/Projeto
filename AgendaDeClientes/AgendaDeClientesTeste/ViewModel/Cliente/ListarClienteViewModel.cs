using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.ViewModel.Cliente
{
    public class ListarClienteViewModel
    {
        public int ClienteId { get; set; }
        public int EnderecoId { get; set; }
        public string URL { get; set; }
        public DateTime? Atualizacao { get; set; }
        public bool Ativo { get; set; }
    }
}
