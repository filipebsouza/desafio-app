using System.Collections.Generic;
using System.Threading.Tasks;
using Base.Dominio;
using Bogus;
using Flunt.Notifications;
using Moq;
using Pessoas.Dominio.Contratos;
using Pessoas.Dominio.Dtos;
using Pessoas.Dominio.Entidades;
using Pessoas.Dominio.Repositorios;
using Pessoas.Dominio.Servicos;
using Xunit;

namespace Pessoas.Dominio.Tests.Servicos
{
    public class ArmazenadorDePessoaTests
    {
        private readonly Faker _faker;
        private readonly Mock<INotificadorBase> _notificadorMock;
        private readonly Mock<IPessoaRepositorio> _pessoaRepositorioMock;
        private readonly ArmazenadorDePessoa _armanezadorDePessoa;

        public ArmazenadorDePessoaTests()
        {
            _faker = new Faker();
            _notificadorMock = new Mock<INotificadorBase>();
            _pessoaRepositorioMock = new Mock<IPessoaRepositorio>();
            _armanezadorDePessoa = new ArmazenadorDePessoa(
                _notificadorMock.Object,
                _pessoaRepositorioMock.Object
            );
        }

        [Fact]
        public async Task DeveArmazenarPessoa()
        {
            //Given
            var dto = new PessoaDto
            {
                Nome = _faker.Person.FullName,
                DataDeNascimento = _faker.Date.Past().Date
            };

            //When
            var retorno = await _armanezadorDePessoa.Armazenar(dto);

            //Then
            _pessoaRepositorioMock.Verify(repositorio => repositorio.Incluir(It.IsAny<Pessoa>()), Times.Once);
            _pessoaRepositorioMock.Verify(repositorio => repositorio.Salvar(), Times.Once);
            _notificadorMock.Verify(notificador => notificador.Notificar(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _notificadorMock.Verify(notificador => notificador.Notificar(It.IsAny<IReadOnlyCollection<Notification>>()), Times.Never);
            Assert.NotNull(retorno);
        }

        [Fact]
        public async Task NaoDeveArmazenarPessoaQuandoDtoForInvalido()
        {
            //Given
            PessoaDto dtoInvalido = null;

            //When
            var retorno = await _armanezadorDePessoa.Armazenar(dtoInvalido);

            //Then
            _pessoaRepositorioMock.Verify(repositorio => repositorio.Incluir(It.IsAny<Pessoa>()), Times.Never);
            _pessoaRepositorioMock.Verify(repositorio => repositorio.Salvar(), Times.Never);
            _notificadorMock.Verify(notificador => notificador.Notificar(It.IsAny<string>(), PessoaDicionarioDeMensagensDeValidacao.MensagemDtoInvalido));
            Assert.Null(retorno);
        }

        [Fact]
        public async Task NaoDeveArmazenarPessoaQuandoObjetoPessoaForInvalido()
        {
            //Given
            string nomePessoaInvalido = null;
            var dto = new PessoaDto
            {
                Nome = nomePessoaInvalido,
                DataDeNascimento = _faker.Date.Past().Date
            };

            //When
            var retorno = await _armanezadorDePessoa.Armazenar(dto);

            //Then
            _pessoaRepositorioMock.Verify(repositorio => repositorio.Incluir(It.IsAny<Pessoa>()), Times.Never);
            _pessoaRepositorioMock.Verify(repositorio => repositorio.Salvar(), Times.Never);
            _notificadorMock.Verify(notificador => notificador.Notificar(It.IsAny<IReadOnlyCollection<Notification>>()), Times.Once);
            Assert.Null(retorno);
        }
    }
}