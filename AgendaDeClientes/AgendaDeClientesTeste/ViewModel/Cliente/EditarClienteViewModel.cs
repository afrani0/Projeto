using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.ViewModel.Cliente
{
    public class EditarClienteViewModel : _GenericClienteViewModel
    {
        public int ClienteId { get; set; }
        public int EnderecoId { get; set; }
        [Required(ErrorMessage = "Campo não pode ser vazio.")]
        [DataType(DataType.Upload)]
        public IFormFile Foto { get; set; }
        [Required(ErrorMessage = "Campo não pode ser vazio.")]
        public bool Ativo { get; set; }
    }
}
