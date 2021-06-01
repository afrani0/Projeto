using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using ListaDeContatos.ViewModels;
using ListaDeContatos.Dominio;
using Microsoft.AspNetCore.Identity;

namespace ListaDeContatos.Models
{
    public class ApplicationDBContext : IdentityDbContext<Usuario, NivelAcesso, string , IdentityUserClaim<string>, UsuarioNivelAcesso, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<NivelAcesso> NiveisAcessos { get; set; }
        public DbSet<UsuarioNivelAcesso> UsuariosNivelAcessos { get; set; }

        public DbSet<ListaDeContatos.Dominio.Regiao> Regiao { get; set; }
        public DbSet<Contato> Contatos { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Usuário
            
            modelBuilder.Entity<Usuario>().Property(p => p.Nome).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Usuario>().Property(p => p.SobreNome).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Usuario>().Property(p => p.UserName).HasMaxLength(256).IsRequired();
            modelBuilder.Entity<Usuario>().Property(p => p.Email).HasMaxLength(256).IsRequired();
            modelBuilder.Entity<Usuario>().Property(p => p.PrimeiroAcesso).HasDefaultValue(true);
            modelBuilder.Entity<Usuario>().HasIndex(p => p.UserName).IsUnique(); 

            //Contato
            
            modelBuilder.Entity<Contato>().Property(p => p.Nome).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Contato>().Property(p => p.Telefone).IsRequired().HasMaxLength(11);
            modelBuilder.Entity<Contato>().Property(p => p.TelefoneRecado).HasMaxLength(11);
            modelBuilder.Entity<Contato>().Property(p => p.DataNascimento).IsRequired();
            modelBuilder.Entity<Contato>().Property(p => p.Sexo).IsRequired();
            modelBuilder.Entity<Contato>().Property(p => p.RG).IsRequired().HasMaxLength(11);
            modelBuilder.Entity<Contato>().Property(p => p.Estado).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Contato>().Property(p => p.Cidade).IsRequired().HasMaxLength(40);
            modelBuilder.Entity<Contato>().Property(p => p.Bairro).IsRequired().HasMaxLength(40);
            modelBuilder.Entity<Contato>().Property(p => p.Cep).HasMaxLength(8);
            modelBuilder.Entity<Contato>().Property(p => p.Rua).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Contato>().Property(p => p.Numero).IsRequired().HasMaxLength(99999);
            modelBuilder.Entity<Contato>().Property(p => p.Complemento).IsRequired().HasMaxLength(20);

            modelBuilder.Entity<Usuario>(b =>
                {
                    b.HasMany(e => e.UsuarioNivelAcessos).WithOne(e => e.Usuario).HasForeignKey(rc => rc.UserId).IsRequired();

                }
            );

            modelBuilder.Entity<NivelAcesso>(b =>
            {
                b.HasMany(e => e.UsuarioNivelAcessos).WithOne(e => e.NivelAcesso).HasForeignKey(rc => rc.RoleId).IsRequired();
                
            }
            );

            modelBuilder.Entity<UsuarioNivelAcesso>(c =>
            {
                // Primary key
                c.HasKey(r => new { r.UserId, r.RoleId });

            }
            );

            modelBuilder.Entity<UsuarioNivelAcesso>().HasIndex(p => p.UserId).IsUnique();
                       
        }
    }
}
