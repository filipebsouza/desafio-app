using Microsoft.EntityFrameworkCore;
using Rendas.Dominio.Entidades;
using Rendas.Infra.Contextos.MetodosDeExtensao;

namespace Rendas.Infra.Contextos
{
    public class RendaPorPessoaContexto : DbContext
    {
        public RendaPorPessoaContexto(DbContextOptions<RendaPorPessoaContexto> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RendaPorPessoa>(rendaPorPessoa =>
            {
                rendaPorPessoa.HasKey(e => e.Id);
                rendaPorPessoa.Property(e => e.Id)
                    .HasValueGenerator<AutoIncrementoParaBancoEmMemoria<RendaPorPessoa>>();
                rendaPorPessoa.Ignore(e => e.Notificacoes);
                rendaPorPessoa.Ignore(e => e.Notifications);
            });
        }

        public DbSet<RendaPorPessoa> RendaPorPessoas { get; set; }
    }
}