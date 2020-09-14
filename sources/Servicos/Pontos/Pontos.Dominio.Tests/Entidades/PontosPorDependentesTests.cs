using System;
using System.Collections.Generic;
using Bogus;
using Pontos.Dominio.Entidades;
using Xunit;

namespace Pontos.Dominio.Tests.Servicos
{
    public class PontosPorDependentesTests
    {
        private readonly Faker _faker;

        public PontosPorDependentesTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveMarcarZeroPontosQuandoPessoasEhInvalido()
        {
            //Given
            List<Pessoa> pessoasInvalidas = null;

            //When
            var pontos = new PontosPorDependentes(pessoasInvalidas);

            //Then
            Assert.Equal(0, pontos.TotalDePontos);
        }

        [Fact]
        public void DeveMarcarZeroPontosQuandoPessoasForDeTipoDiferenteDeDependente()
        {
            //Given
            var pessoas = new List<Pessoa>
            {
                new Pessoa(
                    _faker.Random.Guid(),
                    _faker.Person.FullName,
                    _faker.Date.Past().Date,
                    TipoDaPessoaEnum.Conjuge
                )
            };

            //When
            var pontos = new PontosPorDependentes(pessoas);

            //Then
            Assert.Equal(0, pontos.TotalDePontos);
        }

        [Fact]
        public void DeveMarcarZeroPontosQuandoNaoHaDependentes()
        {
            //Given
            var pessoas = new List<Pessoa>
            {
                new Pessoa(
                    _faker.Random.Guid(),
                    _faker.Person.FullName,
                    _faker.Date.Past().Date,
                    TipoDaPessoaEnum.Conjuge
                ),
                new Pessoa(
                    _faker.Random.Guid(),
                    _faker.Person.FullName,
                    _faker.Date.Past().Date,
                    TipoDaPessoaEnum.Pretendente
                ),
            };

            //When
            var pontos = new PontosPorDependentes(pessoas);

            //Then
            Assert.Equal(0, pontos.TotalDePontos);
        }

        [Theory]
        [InlineData(18)]
        [InlineData(23)]
        [InlineData(45)]
        public void NaoDeveMarcarPontoQuandoDependenteTiver18AnosOuMais(int idade)
        {
            //Given
            var pessoas = new List<Pessoa>
            {
                new Pessoa(
                    _faker.Random.Guid(),
                    _faker.Person.FullName,
                    DateTime.Now.AddYears(-idade).Date,
                    TipoDaPessoaEnum.Dependente
                ),
            };

            //When
            var pontos = new PontosPorDependentes(pessoas);

            //Then
            Assert.Equal(0, pontos.TotalDePontos);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(6)]
        public void DeveMarcarTresPontosQuandoHouverTresOuMaisDependentes(int quantidadeDeDependentes)
        {
            //Given
            var pessoas = new List<Pessoa>();
            for (var indice = 0; indice < quantidadeDeDependentes; indice++)
            {
                pessoas.Add(new Pessoa(
                    _faker.Random.Guid(),
                    _faker.Person.FullName,
                    DateTime.Now.AddYears(-13).Date,
                    TipoDaPessoaEnum.Dependente
                ));
            }

            //When
            var pontos = new PontosPorDependentes(pessoas);

            //Then
            Assert.Equal(3, pontos.TotalDePontos);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void DeveMarcarDoisPontosQuandoHouverUmOuDoisDependentes(int quantidadeDeDependentes)
        {
            //Given
            var pessoas = new List<Pessoa>();
            for (var indice = 0; indice < quantidadeDeDependentes; indice++)
            {
                pessoas.Add(new Pessoa(
                    _faker.Random.Guid(),
                    _faker.Person.FullName,
                    DateTime.Now.AddYears(-13).Date,
                    TipoDaPessoaEnum.Dependente
                ));
            }

            //When
            var pontos = new PontosPorDependentes(pessoas);

            //Then
            Assert.Equal(2, pontos.TotalDePontos);
        }
    }
}
