using System.Collections.Generic;
using System.Linq;
using Base.Dominio.Entidades;

namespace Pontos.Dominio.Entidades
{
    public class PontosPorDependentes : EntidadeBase
    {
        private int quantidadeDePontos;
        protected PontosPorDependentes() { }
        public PontosPorDependentes(List<Pessoa> pessoas)
        {
            quantidadeDePontos = 0;

            if (pessoas == null || pessoas.Count < 0) return;

            CalcularPontosDeDependente(pessoas);
        }

        private void CalcularPontosDeDependente(List<Pessoa> pessoas)
        {
            var quantidadeDeDependentes = pessoas.Where(pessoa =>
                pessoa.TipoDaPessoa == TipoDaPessoaEnum.Dependente &&
                !pessoa.EhMaiorDeIdade
            ).Count();

            if (quantidadeDeDependentes <= 0)
                quantidadeDePontos += 0;
            else if (quantidadeDeDependentes >= 3)
                quantidadeDePontos += 3;
            else
                quantidadeDePontos += 2;
        }

        public int TotalDePontos => quantidadeDePontos;
    }
}