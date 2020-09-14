using System.Collections.Generic;
using System.Linq;
using Bogus;
using Pontos.Dominio.Entidades;
using Xunit;

namespace Pontos.Dominio.Tests.Servicos
{
    public class PontuacaoPorFamiliaTests
    {
        private readonly Faker _faker;

        public PontuacaoPorFamiliaTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveCriarPontuacaoPorFamilia()
        {
            //Given
            var pessoas = new List<Pessoa>
            {
                new Pessoa(
                    _faker.Random.Guid(),
                    _faker.Person.FullName,
                    _faker.Date.Past().Date,
                    _faker.PickRandom<TipoDaPessoaEnum>()
                )
            };
            var rendaPorPessoas = new List<RendaPorPessoa>
            {
                new RendaPorPessoa(
                    _faker.Random.Guid(),
                    pessoas.FirstOrDefault().Id,
                    _faker.Random.Decimal(40)
                )
            };
            var familia = new Familia(
                _faker.Random.Guid(),
                pessoas,
                rendaPorPessoas,
                0
            );

            //When
            var pontuacaoPorFamilia = new PontuacaoPorFamilia(familia);

            //Then
            Assert.True(pontuacaoPorFamilia.Valid);
            Assert.True(pontuacaoPorFamilia.NaoPossuiNotificacoes);
        }

        [Fact]
        public void NaoDeveCriarPontuacaoPorFamiliaPoisPessoasSaoInvalidas()
        {
            //Given
            List<Pessoa> pessoasInvalidas = null;
            var rendaPorPessoas = new List<RendaPorPessoa>
            {
                new RendaPorPessoa(
                    _faker.Random.Guid(),
                    _faker.Random.Guid(),
                    _faker.Random.Decimal(40)
                )
            };
            var familia = new Familia(
                _faker.Random.Guid(),
                pessoasInvalidas,
                rendaPorPessoas,
                0
            );

            //When
            var pontuacaoPorFamilia = new PontuacaoPorFamilia(familia);

            //Then
            Assert.True(pontuacaoPorFamilia.Invalid);
            Assert.True(pontuacaoPorFamilia.PossuiNotificacoes);
        }

        [Fact]
        public void NaoDeveCriarPontuacaoPorFamiliaPoisRendaPorPessoasSaoInvalidas()
        {
            //Given
            var pessoas = new List<Pessoa>
            {
                new Pessoa(
                    _faker.Random.Guid(),
                    _faker.Person.FullName,
                    _faker.Date.Past().Date,
                    _faker.PickRandom<TipoDaPessoaEnum>()
                )
            };
            List<RendaPorPessoa> rendaPorPessoasInvalidas = null;
            var familia = new Familia(
                _faker.Random.Guid(),
                pessoas,
                rendaPorPessoasInvalidas,
                0
            );

            //When
            var pontuacaoPorFamilia = new PontuacaoPorFamilia(familia);

            //Then
            Assert.True(pontuacaoPorFamilia.Invalid);
            Assert.True(pontuacaoPorFamilia.PossuiNotificacoes);
        }
    }
}