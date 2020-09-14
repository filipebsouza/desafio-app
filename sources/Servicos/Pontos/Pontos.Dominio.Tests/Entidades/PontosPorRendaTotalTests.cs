using System.Collections.Generic;
using Bogus;
using Pontos.Dominio.Entidades;
using Xunit;

namespace Pontos.Dominio.Tests.Servicos
{
    public class PontosPorRendaTotalTests
    {
        private readonly Faker _faker;

        public PontosPorRendaTotalTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveMarcarZeroPontosQuandoRendaPorPessoasEhInvalido()
        {
            //Given
            List<RendaPorPessoa> rendaPorPessoasInvalidas = null;

            //When
            var pontos = new Entidades.PontosPorRendaTotal(rendaPorPessoasInvalidas);

            //Then
            Assert.Equal(0, pontos.TotalDePontos);
        }

        [Theory]                
        [InlineData(550, 550, 5500)]
        [InlineData(1000, 500, 500.01)]
        public void NaoDeveMarcarPontoQuandoRendaTotalForMaiorQue2000Reias(decimal renda1, decimal renda2, decimal renda3)
        {
            //Given
            var rendaPorPessoas = new List<RendaPorPessoa>();
            rendaPorPessoas.Add(new RendaPorPessoa(_faker.Random.Guid(), _faker.Random.Guid(), renda1));
            rendaPorPessoas.Add(new RendaPorPessoa(_faker.Random.Guid(), _faker.Random.Guid(), renda2));
            rendaPorPessoas.Add(new RendaPorPessoa(_faker.Random.Guid(), _faker.Random.Guid(), renda3));

            //When
            var pontos = new Entidades.PontosPorRendaTotal(rendaPorPessoas);

            //Then
            Assert.Equal(0, pontos.TotalDePontos);
        }

        [Theory]
        [InlineData(300, 300, 300)]
        [InlineData(100, 100, 40)]
        [InlineData(300, 300, 299.99)]
        public void DeveMarcarCincoPontosQuandoRendaTotalForAte900Reias(decimal renda1, decimal renda2, decimal renda3)
        {
            //Given
            var rendaPorPessoas = new List<RendaPorPessoa>();
            rendaPorPessoas.Add(new RendaPorPessoa(_faker.Random.Guid(), _faker.Random.Guid(), renda1));
            rendaPorPessoas.Add(new RendaPorPessoa(_faker.Random.Guid(), _faker.Random.Guid(), renda2));
            rendaPorPessoas.Add(new RendaPorPessoa(_faker.Random.Guid(), _faker.Random.Guid(), renda3));

            //When
            var pontos = new Entidades.PontosPorRendaTotal(rendaPorPessoas);

            //Then
            Assert.Equal(5, pontos.TotalDePontos);
        }

        [Theory]
        [InlineData(300, 300, 300.01)]
        [InlineData(500, 500, 500)]
        [InlineData(44.55, 456.66, 541.47)]
        [InlineData(500, 500, 499.99)]
        public void DeveMarcarTresPontosQuandoRendaTotalForEntre900_01ReiasE1500Reias(decimal renda1, decimal renda2, decimal renda3)
        {
            //Given
            var rendaPorPessoas = new List<RendaPorPessoa>();
            rendaPorPessoas.Add(new RendaPorPessoa(_faker.Random.Guid(), _faker.Random.Guid(), renda1));
            rendaPorPessoas.Add(new RendaPorPessoa(_faker.Random.Guid(), _faker.Random.Guid(), renda2));
            rendaPorPessoas.Add(new RendaPorPessoa(_faker.Random.Guid(), _faker.Random.Guid(), renda3));

            //When
            var pontos = new Entidades.PontosPorRendaTotal(rendaPorPessoas);

            //Then
            Assert.Equal(3, pontos.TotalDePontos);
        }

        [Theory]
        [InlineData(666.66, 666.66, 666.67)]
        [InlineData(500, 500, 500.01)]
        [InlineData(550, 550, 550)]
        [InlineData(1000, 500, 100)]
        public void DeveMarcarUmPontoQuandoRendaTotalForEntre1500_01ReiasE2000Reias(decimal renda1, decimal renda2, decimal renda3)
        {
            //Given
            var rendaPorPessoas = new List<RendaPorPessoa>();
            rendaPorPessoas.Add(new RendaPorPessoa(_faker.Random.Guid(), _faker.Random.Guid(), renda1));
            rendaPorPessoas.Add(new RendaPorPessoa(_faker.Random.Guid(), _faker.Random.Guid(), renda2));
            rendaPorPessoas.Add(new RendaPorPessoa(_faker.Random.Guid(), _faker.Random.Guid(), renda3));

            //When
            var pontos = new Entidades.PontosPorRendaTotal(rendaPorPessoas);

            //Then
            Assert.Equal(3, pontos.TotalDePontos);
        }
    }
}