using Microsoft.EntityFrameworkCore;
using React_Arquivos_Backend.Models;

namespace React_Arquivos_Backend.ManagementAplicationDB
{
    public class DBAplication : DbContext
    {
        public DbSet<Arquivo> Arquivos { get; set; }

        public DBAplication(DbContextOptions<DBAplication> options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Arquivo
            modelBuilder.Entity<Arquivo>().ToTable("Arquivo");
            modelBuilder.Entity<Arquivo>().HasKey(p => p.ArquivoId);
            modelBuilder.Entity<Arquivo>().Property(p => p.Nome).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Arquivo>().Property(p => p.CaminhoImg).HasMaxLength(300).IsRequired();
            modelBuilder.Entity<Arquivo>().Property(p => p.Descricao).HasMaxLength(300).IsRequired();
            modelBuilder.Entity<Arquivo>().Property(p => p.Tamanho).HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Arquivo>().Property(p => p.Formato).HasMaxLength(20).IsRequired();
        }
    }
}
