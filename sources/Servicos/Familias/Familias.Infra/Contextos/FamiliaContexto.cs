using Microsoft.EntityFrameworkCore;
using Familias.Dominio.Entidades;
using Familias.Infra.Contextos.MetodosDeExtensao;

namespace Familias.Infra.Contextos
{
    public class FamiliaContexto : DbContext
    {
        public FamiliaContexto(DbContextOptions<FamiliaContexto> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Familia>(familia =>
            {
                familia.HasKey(e => e.Id);
                familia.Property(e => e.Id)
                    .HasValueGenerator<AutoIncrementoParaBancoEmMemoria<Familia>>();
                familia.Ignore(e => e.Notificacoes);
                familia.Ignore(e => e.Notifications);
            });

            modelBuilder.Entity<RendaPorPessoa>(rendaPorPessoa =>
            {
                rendaPorPessoa.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Pessoa>(pessoa =>
            {
                pessoa.HasKey(e => e.Id);
            });
        }

        public DbSet<Familia> Familias { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<RendaPorPessoa> Rendas { get; set; }
    }
}