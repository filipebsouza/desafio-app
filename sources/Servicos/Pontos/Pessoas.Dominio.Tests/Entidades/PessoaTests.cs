using System;
using Bogus;
using Xunit;
using Pessoas.Dominio.Entidades;

namespace Pessoas.Dominio.Tests
{
    public class PessoaTests
    {
        private readonly Faker _faker;

        public PessoaTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveCriarPessoa()
        {
            //Given
            var pessoaEsperada = new
            {
                Nome = _faker.Person.FullName,
                DataDeNascimento = _faker.Date.Past().Date,
                TipoDaPessoa = _faker.PickRandom<TipoDaPessoaEnum>()
            };

            //When
            var pessoa = new Pessoa(pessoaEsperada.Nome, pessoaEsperada.DataDeNascimento, pessoaEsperada.TipoDaPessoa);

            //Then
            Assert.Equal(pessoaEsperada.Nome, pessoa.Nome);
            Assert.Equal(pessoaEsperada.DataDeNascimento, pessoa.DataDeNascimento);
            Assert.Equal(pessoaEsperada.TipoDaPessoa, pessoa.TipoDaPessoa);
        }

        [Fact]
        public void NaoDeveCriarPessoaComNomeInvalido()
        {
            //Given
            string nomeInvalido = null;

            //When
            var pessoa = new Pessoa(nomeInvalido, _faker.Date.Past().Date, _faker.PickRandom<TipoDaPessoaEnum>());

            //Then
            Assert.NotEmpty(pessoa.Notificacoes);
            Assert.True(pessoa.Invalid);
        }

        [Fact]
        public void NaoDeveCriarPessoaComDataDeNascimentoInvalido()
        {
            //Given
            DateTime dataDeNascimentoInvalido = _faker.Date.Future().Date;

            //When
            var pessoa = new Pessoa(_faker.Person.FullName, dataDeNascimentoInvalido, _faker.PickRandom<TipoDaPessoaEnum>());

            //Then
            Assert.NotEmpty(pessoa.Notificacoes);
            Assert.True(pessoa.Invalid);
        }
    }
}
