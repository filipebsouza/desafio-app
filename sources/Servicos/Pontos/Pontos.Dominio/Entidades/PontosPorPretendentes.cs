using System.Collections.Generic;
using System.Linq;
using Base.Dominio;

namespace Pontos.Dominio.Entidades
{
    public class PontosPorPretendentes : EntidadeBase
    {
        private int quantidadeDePontos;
        protected PontosPorPretendentes() { }
        public PontosPorPretendentes(List<Pessoa> pessoas)
        {
            quantidadeDePontos = 0;

            if (pessoas == null || pessoas.Count < 0) return;

            CalcularPontosDePretendente(pessoas);
        }

        private void CalcularPontosDePretendente(List<Pessoa> pessoas)
        {
            var pretendentes = pessoas.Where(pessoa =>
               pessoa.TipoDaPessoa == TipoDaPessoaEnum.Pretendente
            ).ToList();

            if (!pretendentes.Any())
                quantidadeDePontos += 0;
            else if (pretendentes.Any(p => p.Idade >= 45))
                quantidadeDePontos += 3;
            else if (pretendentes.Any(p => p.Idade < 30))
                quantidadeDePontos += 1;
            else
                quantidadeDePontos += 2;
        }

        public int TotalDePontos => quantidadeDePontos;
    }
}