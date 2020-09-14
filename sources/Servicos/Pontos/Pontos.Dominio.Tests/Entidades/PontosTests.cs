using System;
using System.Collections.Generic;
using Bogus;
using Pontos.Dominio.Entidades;
using Xunit;

namespace Pontos.Dominio.Tests.Servicos
{
    public class PontosTests
    {
        private readonly Faker _faker;

        public PontosTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void NaoDevePontuarQuandoPessoasERendaPorPessoasForInvalido()
        {
            //Given
            var familia = new Familia(
                _faker.Random.Guid(),
                null,
                null,
                0
            );

            //When
            var pontos = new Entidades.Pontos(familia);

            //Then            
            Assert.Equal(0, pontos.TotalDePontos);
        }

        [Fact]
        public void DevePontuarDezPontos_QuandoHouverPretendenteAcimaDe45Anos_ERendaTotalForMenorIgualAh900Reis_PossuirUmDependeteMenorDeIdade()
        {
            //Given
            var pessoaId1 = _faker.Random.Guid();
            var pessoaId2 = _faker.Random.Guid();
            var familia = new Familia(
                _faker.Random.Guid(),
                new List<Pessoa>
                {
                    new Pessoa(
                        pessoaId1,
                        _faker.Person.FullName,
                        DateTime.Now.AddYears(-60).Date,
                        TipoDaPessoaEnum.Pretendente
                    ),
                    new Pessoa(
                        pessoaId2,
                        _faker.Person.FullName,
                        DateTime.Now.AddYears(-13).Date,
                        TipoDaPessoaEnum.Dependente
                    ),
                },
                new List<RendaPorPessoa>
                {
                    new RendaPorPessoa(
                        _faker.Random.Guid(),
                        pessoaId1,
                        500
                    )
                },
                0
            );

            //When
            var pontos = new Entidades.Pontos(familia);

            //Then            
            Assert.Equal(10, pontos.TotalDePontos);
        }
    }
}