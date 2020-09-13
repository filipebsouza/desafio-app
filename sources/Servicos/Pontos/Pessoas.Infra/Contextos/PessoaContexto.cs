using Microsoft.EntityFrameworkCore;
using Pessoas.Dominio.Entidades;
using Pessoas.Infra.Contextos.MetodosDeExtensao;
using Pessoas.Infra.Seeds;

namespace Pessoas.Infra.Contextos
{
    public class PessoaContexto : DbContext
    {
        public PessoaContexto(DbContextOptions<PessoaContexto> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pessoa>(pessoa =>
            {
                pessoa.HasKey(e => e.Id);
                pessoa.Property(e => e.Id)
                    .HasValueGenerator<AutoIncrementoParaBancoEmMemoria<Pessoa>>();
                pessoa.Ignore(e => e.Notificacoes);
                pessoa.Ignore(e => e.Notifications);
            });
        }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}