using Bogus;
using Xunit;
using Familias.Dominio.Entidades;
using Familias.Dominio.Dtos;
using System.Collections.Generic;

namespace Familias.Dominio.Tests
{
    public class FamiliaTests
    {
        private readonly Faker _faker;

        public FamiliaTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveCriarFamilia()
        {
            //Given
            var pessoaId = _faker.Random.Guid();
            var familiaEsperada = new
            {
                Pessoas = new List<Pessoa>
                {
                    new Pessoa(pessoaId, _faker.Person.FullName, _faker.Date.Past(), "Cônjuge")
                },
                Rendas = new List<RendaPorPessoa>
                {
                    new RendaPorPessoa(_faker.Random.Guid(), pessoaId, _faker.Random.Decimal(30))
                }
            };

            //When
            var familia = new Familia(
                familiaEsperada.Pessoas,
                familiaEsperada.Rendas
            );

            //Then
            Assert.NotNull(familia.Pessoas);
            Assert.NotNull(familia.Rendas);
            Assert.Equal(StatusDaFamiliaEnum.CadastroCompleto, familia.Status);
        }

        [Fact]
        public void NaoDeveCriarFamiliaComPessoasInvalidas()
        {
            //Given
            List<Pessoa> pessoasInvalidas = null;

            //When
            var familia = new Familia(
                pessoasInvalidas,
                new List<RendaPorPessoa>
                {
                    new RendaPorPessoa(_faker.Random.Guid(), _faker.Random.Guid(), _faker.Random.Decimal(30))
                }
            );

            //Then
            Assert.NotEmpty(familia.Notificacoes);
            Assert.True(familia.Invalid);
        }


        [Fact]
        public void NaoDeveCriarFamiliaComRendaPorPessoasInvalidas()
        {
            //Given
            List<RendaPorPessoa> rendaPorPessoasInvalidas = null;

            //When
            var familia = new Familia(
                new List<Pessoa>
                {
                    new Pessoa(_faker.Random.Guid(), _faker.Person.FullName, _faker.Date.Past(), "Cônjuge")
                },
                rendaPorPessoasInvalidas
            );

            //Then
            Assert.NotEmpty(familia.Notificacoes);
            Assert.True(familia.Invalid);
        }
    }
}
