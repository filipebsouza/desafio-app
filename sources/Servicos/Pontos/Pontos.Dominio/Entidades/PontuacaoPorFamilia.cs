using Base.Dominio.Entidades;
using Pontos.Dominio.Contratos;

namespace Pontos.Dominio.Entidades
{
    public class PontuacaoPorFamilia : EntidadeBase
    {
        private readonly PontuacaoPorFamiliaContrato _contrato;
        protected PontuacaoPorFamilia() { }
        public PontuacaoPorFamilia(Familia familia) : base()
        {
            _contrato = new PontuacaoPorFamiliaContrato(familia);

            if (_contrato.Valid)
            {
                Familia = familia;
                Pontos = new Pontos(familia);
            }
            else
            {
                Notificar(_contrato.Notifications);
            }
        }

        public virtual Familia Familia { get; private set; }
        public virtual Pontos Pontos { get; private set; }

        public int TotalDePontos
        {
            get
            {
                if (Pontos == null) return 0;

                return Pontos.TotalDePontos;
            }
        }
    }
}