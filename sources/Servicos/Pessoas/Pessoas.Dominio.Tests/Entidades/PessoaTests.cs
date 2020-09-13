using System;
using System.Collections.Generic;
using Bogus;
using Flunt.Notifications;
using Flunt.Validations;
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
                DataDeNascimento = _faker.Date.Past().Date
            };

            //When
            var pessoa = new Pessoa(pessoaEsperada.Nome, pessoaEsperada.DataDeNascimento);

            //Then
            Assert.Equal(pessoaEsperada.Nome, pessoa.Nome);
            Assert.Equal(pessoaEsperada.DataDeNascimento, pessoa.DataDeNascimento);
        }

        [Fact]
        public void NaoDeveCriarPessoaComNomeInvalido()
        {
            //Given
            string nomeInvalido = null;

            //When
            var pessoa = new Pessoa(nomeInvalido, _faker.Date.Past().Date);

            //Then
            Assert.NotEmpty(pessoa.Notifications);
            Assert.True(pessoa.Invalid);
        }

        [Fact]
        public void NaoDeveCriarPessoaComDataDeNascimentoInvalido()
        {
            //Given
            DateTime dataDeNascimentoInvalido = _faker.Date.Future().Date;

            //When
            var pessoa = new Pessoa(_faker.Person.FullName, dataDeNascimentoInvalido);

            //Then
            Assert.NotEmpty(pessoa.Notifications);
            Assert.True(pessoa.Invalid);
        }
    }
}
