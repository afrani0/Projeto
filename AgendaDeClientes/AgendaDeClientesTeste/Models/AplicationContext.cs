using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeClientes.Models
{
    public class AplicationContext : DbContext
    {

        public AplicationContext(DbContextOptions<AplicationContext> options) : base (options)
        {

        }
        public DbSet<Cliente> Clientes;
        public DbSet<Endereco> Enderecos;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        //mapeamento dos modelos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Cliente
            modelBuilder.Entity<Cliente>()
                .Property(c => c.Nome).HasMaxLength(50);
            modelBuilder.Entity<Cliente>()
                .Property(c => c.Telefone).HasMaxLength(11);
            modelBuilder.Entity<Cliente>()
                .Property(c => c.TelefoneRecado).HasMaxLength(11).IsRequired(false);
            modelBuilder.Entity<Cliente>()
                .Property<DateTime>(c => c.DataNascimento);
            modelBuilder.Entity<Cliente>()
                .Property<DateTime?>(c => c.Atualizacao).IsRequired(false).HasDefaultValue(null);
            modelBuilder.Entity<Cliente>()
                .Property(c => c.URL).HasMaxLength(250).IsRequired(true).HasDefaultValue(true);
            modelBuilder.Entity<Cliente>()
                .Property(c => c.Ativo).IsRequired(true);

            //definir relacionamento 1 para 1 - Cliente
            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Endereco)
                .WithOne(e => e.Cliente);
                
            #endregion

            #region Endereco
            modelBuilder.Entity<Endereco>()
                .Property(e => e.Cidade).HasMaxLength(30);
            modelBuilder.Entity<Endereco>()
                .Property(e => e.Bairro).HasMaxLength(30);
            modelBuilder.Entity<Endereco>()
                .Property(e => e.Rua).HasMaxLength(50);
            modelBuilder.Entity<Endereco>()
                .Property(e => e.Numero);
            modelBuilder.Entity<Endereco>()
                .Property(e => e.Complemento).HasMaxLength(20);

            //definir relacionamento 1 para 1 - Endereco
            modelBuilder.Entity<Endereco>()
                .HasOne(c => c.Cliente)
                .WithOne(e => e.Endereco)
                .HasForeignKey<Cliente>();

            #endregion
        }
    }
}
