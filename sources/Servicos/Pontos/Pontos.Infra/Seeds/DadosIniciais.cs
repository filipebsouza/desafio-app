using System;
using System.Collections.Generic;
using Pontos.Dominio.Entidades;
using Pontos.Infra.Contextos;

namespace Pontos.Infra.Seeds
{
    public class DadosIniciais
    {
        private readonly PontoContexto _contexto;

        public DadosIniciais(PontoContexto contexto)
        {
            _contexto = contexto;
        }

        public void CriarDados()
        {
            var pessoaId = Guid.NewGuid();
            var pontuacaoPorFamilia = new List<PontuacaoPorFamilia>
            {
                new PontuacaoPorFamilia(
                    new Familia(
                        Guid.NewGuid(),
                        new List<Pessoa>
                        {
                            new Pessoa(pessoaId, "João da Silva Junior", new DateTime(1990, 08, 03), TipoDaPessoaEnum.Conjuge)
                        },
                        new List<RendaPorPessoa>
                        {
                            new RendaPorPessoa(Guid.NewGuid(), pessoaId, 50)
                        },
                        0
                    )
                )
            };

            _contexto.PontuacoesPorFamilia.AddRange(pontuacaoPorFamilia);
            _contexto.SaveChanges();
        }
    }
}