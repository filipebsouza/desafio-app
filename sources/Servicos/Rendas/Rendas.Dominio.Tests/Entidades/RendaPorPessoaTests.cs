using Bogus;
using Xunit;
using Rendas.Dominio.Entidades;

namespace Rendas.Dominio.Tests
{
    public class RendaPorPessoaTests
    {
        private readonly Faker _faker;

        public RendaPorPessoaTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveCriarRendaPorPessoa()
        {
            //Given
            var rendaPorPessoaEsperada = new
            {
                PessoaId = _faker.Random.Guid(),
                NomeDaPessoa = _faker.Person.FullName,
                Valor = _faker.Random.Decimal(50)
            };

            //When
            var rendaPorPessoa = new RendaPorPessoa(
                rendaPorPessoaEsperada.PessoaId,
                rendaPorPessoaEsperada.NomeDaPessoa,
                rendaPorPessoaEsperada.Valor);

            //Then
            Assert.Equal(rendaPorPessoaEsperada.PessoaId, rendaPorPessoa.PessoaId);
            Assert.Equal(rendaPorPessoaEsperada.NomeDaPessoa, rendaPorPessoa.NomePessoa);
            Assert.Equal(rendaPorPessoaEsperada.Valor, rendaPorPessoa.Valor);
        }

        [Fact]
        public void NaoDeveCriarRendaPorPessoaComNomeInvalido()
        {
            //Given
            string nomeInvalido = null;

            //When
            var rendaPorPessoa = new RendaPorPessoa(
                _faker.Random.Guid(),
                nomeInvalido,
                _faker.Random.Decimal(78));

            //Then
            Assert.NotEmpty(rendaPorPessoa.Notificacoes);
            Assert.True(rendaPorPessoa.Invalid);
        }


        [Fact]
        public void NaoDeveCriarRendaPorValorMenorIgualAhZero()
        {
            //Given
            decimal valorInvalido = _faker.Random.Decimal(-67, 0);

            //When
            var rendaPorPessoa = new RendaPorPessoa(
                _faker.Random.Guid(),
                _faker.Person.FullName,
                valorInvalido);

            //Then
            Assert.NotEmpty(rendaPorPessoa.Notificacoes);
            Assert.True(rendaPorPessoa.Invalid);
        }
    }
}
