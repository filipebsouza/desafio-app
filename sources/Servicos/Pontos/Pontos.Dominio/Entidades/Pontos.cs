using Base.Dominio;

namespace Pontos.Dominio.Entidades
{
    public class Pontos : EntidadeBase
    {
        protected Pontos() { }
        public Pontos(Familia familia) : base()
        {
            if (familia != null)
            {
                PontosPorRendaTotal = new PontosPorRendaTotal(familia.Rendas);
                PontosPorPretendentes = new PontosPorPretendentes(familia.Pessoas);
                PontosPorDependentes = new PontosPorDependentes(familia.Pessoas);
            }
        }

        public virtual PontosPorRendaTotal PontosPorRendaTotal { get; private set; }
        public virtual PontosPorPretendentes PontosPorPretendentes { get; private set; }
        public virtual PontosPorDependentes PontosPorDependentes { get; private set; }

        public int TotalDePontos => PontosPorRendaTotal.TotalDePontos + PontosPorPretendentes.TotalDePontos + PontosPorDependentes.TotalDePontos;
    }
}