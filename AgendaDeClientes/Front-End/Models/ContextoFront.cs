using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Front_End.Models;

namespace Front_End.Models
{
    public class ContextoFront : DbContext
    {
        public DbSet<Cliente> Clientes;

        public ContextoFront(DbContextOptions<ContextoFront> options) : base (options)
        {

        }

        public DbSet<Front_End.Models.Cliente> Cliente { get; set; }
    }
}
