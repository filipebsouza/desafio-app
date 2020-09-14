using Microsoft.EntityFrameworkCore;
using Pontos.Dominio.Entidades;
using Pontos.Infra.Contextos.MetodosDeExtensao;
using Pontos.Infra.Seeds;

namespace Pontos.Infra.Contextos
{
    public class PontoContexto : DbContext
    {
        public PontoContexto(DbContextOptions<PontoContexto> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RendaPorPessoa>(rendaPorPessoa =>
            {
                rendaPorPessoa.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Pessoa>(pessoa =>
            {
                pessoa.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Familia>(pessoa =>
            {
                pessoa.HasKey(e => e.Id);
            });

            modelBuilder.Entity<PontuacaoPorFamilia>(pessoa =>
            {
                pessoa.HasKey(e => e.Id);
                pessoa.Property(e => e.Id)
                    .HasValueGenerator<AutoIncrementoParaBancoEmMemoria<PontuacaoPorFamilia>>();
                pessoa.Ignore(e => e.Notificacoes);
                pessoa.Ignore(e => e.Notifications);
            });

            modelBuilder.Entity<Dominio.Entidades.Pontos>(pessoa =>
            {
                pessoa.HasKey(e => e.Id);
                pessoa.Property(e => e.Id)
                    .HasValueGenerator<AutoIncrementoParaBancoEmMemoria<PontuacaoPorFamilia>>();
                pessoa.Ignore(e => e.Notificacoes);
                pessoa.Ignore(e => e.Notifications);
            });

            modelBuilder.Entity<PontosPorRendaTotal>(pessoa =>
            {
                pessoa.HasKey(e => e.Id);
                pessoa.Property(e => e.Id)
                    .HasValueGenerator<AutoIncrementoParaBancoEmMemoria<PontuacaoPorFamilia>>();
                pessoa.Ignore(e => e.Notificacoes);
                pessoa.Ignore(e => e.Notifications);
            });

            modelBuilder.Entity<PontosPorPretendentes>(pessoa =>
            {
                pessoa.HasKey(e => e.Id);
                pessoa.Property(e => e.Id)
                    .HasValueGenerator<AutoIncrementoParaBancoEmMemoria<PontuacaoPorFamilia>>();
                pessoa.Ignore(e => e.Notificacoes);
                pessoa.Ignore(e => e.Notifications);
            });

            modelBuilder.Entity<PontosPorDependentes>(pessoa =>
            {
                pessoa.HasKey(e => e.Id);
                pessoa.Property(e => e.Id)
                    .HasValueGenerator<AutoIncrementoParaBancoEmMemoria<PontuacaoPorFamilia>>();
                pessoa.Ignore(e => e.Notificacoes);
                pessoa.Ignore(e => e.Notifications);
            });
        }

        public DbSet<PontuacaoPorFamilia> PontuacoesPorFamilia { get; set; }
        public DbSet<Familia> Familias { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<RendaPorPessoa> Rendas { get; set; }
        public DbSet<Dominio.Entidades.Pontos> Pontos { get; set; }
        public DbSet<PontosPorRendaTotal> PontosPorRendaTotals { get; set; }
        public DbSet<PontosPorPretendentes> PontosPorPretendentes { get; set; }
        public DbSet<PontosPorDependentes> PontosPorDependentes { get; set; }
    }
}