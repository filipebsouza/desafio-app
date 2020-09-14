using System.Collections.Generic;
using System.Linq;
using Base.Dominio;

namespace Pontos.Dominio.Entidades
{
    public class PontosPorRendaTotal : EntidadeBase
    {
        private int quantidadeDePontos;
        protected PontosPorRendaTotal() { }
        public PontosPorRendaTotal(List<RendaPorPessoa> rendas) : base()
        {
            quantidadeDePontos = 0;

            if (rendas == null || rendas.Count < 0) return;

            CalcularPontosDeRendaTotal(rendas);
        }

        private void CalcularPontosDeRendaTotal(List<RendaPorPessoa> rendas)
        {
            var rendaTotal = rendas.Sum(renda => renda.Valor);

            if (rendaTotal <= 900)
                quantidadeDePontos += 5;
            else if (rendaTotal > 2000)
                quantidadeDePontos += 0;
            else
                quantidadeDePontos += 3; // Há um range entre 900,01 e 900,99 tratado aqui
        }

        public int TotalDePontos => quantidadeDePontos;
    }
}
