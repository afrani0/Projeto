using AgendaDeClientes.Models;
using AgendaDeClientes.ViewModel.Cliente;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.ViewModel.Cliente
{
    public class AdicionarClienteViewModel : _GenericClienteViewModel
    {
        [Required(ErrorMessage = "Campo não pode ser vazio.")]
        [DataType(DataType.Upload)]
        public IFormFile Foto { get; set; }
    }
}
