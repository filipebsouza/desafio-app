using System;
using System.Collections.Generic;
using Bogus;
using Pontos.Dominio.Entidades;
using Xunit;

namespace Pontos.Dominio.Tests.Servicos
{
    public class PontosPorPretendentesTests
    {
        private readonly Faker _faker;

        public PontosPorPretendentesTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveMarcarZeroPontosQuandoPessoasEhInvalido()
        {
            //Given
            List<Pessoa> pessoasInvalidas = null;

            //When
            var pontos = new PontosPorPretendentes(pessoasInvalidas);

            //Then
            Assert.Equal(0, pontos.TotalDePontos);
        }

        [Fact]
        public void DeveMarcarZeroPontosQuandoPessoasForDeTipoDiferenteDePretendente()
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
            var pontos = new PontosPorPretendentes(pessoas);

            //Then
            Assert.Equal(0, pontos.TotalDePontos);
        }

        [Fact]
        public void DeveMarcarZeroPontosQuandoNaoHaPretendentes()
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
                    TipoDaPessoaEnum.Dependente
                ),
            };

            //When
            var pontos = new PontosPorPretendentes(pessoas);

            //Then
            Assert.Equal(0, pontos.TotalDePontos);
        }

        [Theory]
        [InlineData(45)]
        [InlineData(46)]
        [InlineData(77)]
        public void DeveMarcarTresPontosQuandoPretendenteTemIdadeIgualOuAcimaDe45Anos(int idade)
        {
            //Given
            var pessoas = new List<Pessoa>
            {
                new Pessoa(
                    _faker.Random.Guid(),
                    _faker.Person.FullName,
                    DateTime.Now.AddYears(-idade).Date,
                    TipoDaPessoaEnum.Pretendente
                ),
            };

            //When
            var pontos = new PontosPorPretendentes(pessoas);

            //Then
            Assert.Equal(3, pontos.TotalDePontos);
        }


        [Theory]
        [InlineData(30)]
        [InlineData(40)]
        [InlineData(44)]
        public void DeveMarcarDoisPontosQuandoPretendenteTemIdadeMaiorIgualAh30AnosEMenorIgualAh44Anos(int idade)
        {
            //Given
            var pessoas = new List<Pessoa>
            {
                new Pessoa(
                    _faker.Random.Guid(),
                    _faker.Person.FullName,
                    DateTime.Now.AddYears(-idade).Date,
                    TipoDaPessoaEnum.Pretendente
                ),
            };

            //When
            var pontos = new PontosPorPretendentes(pessoas);

            //Then
            Assert.Equal(2, pontos.TotalDePontos);
        }

        [Theory]
        [InlineData(29)]
        [InlineData(15)]
        [InlineData(1)]
        public void DeveMarcarUmPontoQuandoPretendenteTemIdadeMenorQue30Anos(int idade)
        {
            //Given
            var pessoas = new List<Pessoa>
            {
                new Pessoa(
                    _faker.Random.Guid(),
                    _faker.Person.FullName,
                    DateTime.Now.AddYears(-idade).Date,
                    TipoDaPessoaEnum.Pretendente
                ),
            };

            //When
            var pontos = new PontosPorPretendentes(pessoas);

            //Then
            Assert.Equal(1, pontos.TotalDePontos);
        }
    }
}