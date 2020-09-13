using System.Collections.Generic;
using System.Threading.Tasks;
using Base.Dominio;
using Bogus;
using Flunt.Notifications;
using Moq;
using Rendas.Dominio.Contratos;
using Rendas.Dominio.Dtos;
using Rendas.Dominio.Entidades;
using Rendas.Dominio.Repositorios;
using Rendas.Dominio.Servicos;
using Xunit;

namespace Rendas.Dominio.Tests.Servicos
{
    public class ArmazenadorDePessoaTests
    {
        private readonly Faker _faker;
        private readonly Mock<INotificadorBase> _notificadorMock;
        private readonly Mock<IRendaPorPessoaRepositorio> _rendaPorPessoaRepositorioMock;
        private readonly ArmazenadorDeRendaPorPessoa _armanezadorDeRendaPorPessoa;

        public ArmazenadorDePessoaTests()
        {
            _faker = new Faker();
            _notificadorMock = new Mock<INotificadorBase>();
            _rendaPorPessoaRepositorioMock = new Mock<IRendaPorPessoaRepositorio>();
            _armanezadorDeRendaPorPessoa = new ArmazenadorDeRendaPorPessoa(
                _notificadorMock.Object,
                _rendaPorPessoaRepositorioMock.Object
            );
        }

        [Fact]
        public async Task DeveArmazenarRendaPorPessoa()
        {
            //Given
            var dto = new RendaPorPessoaDto
            {
                PessoaId = _faker.Random.Guid(),
                NomePessoa = _faker.Person.FullName,
                Valor = _faker.Random.Decimal(40)
            };

            //When
            var retorno = await _armanezadorDeRendaPorPessoa.Armazenar(dto);

            //Then
            _rendaPorPessoaRepositorioMock.Verify(repositorio => repositorio.Incluir(It.IsAny<RendaPorPessoa>()), Times.Once);
            _rendaPorPessoaRepositorioMock.Verify(repositorio => repositorio.Salvar(), Times.Once);
            _notificadorMock.Verify(notificador => notificador.Notificar(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _notificadorMock.Verify(notificador => notificador.Notificar(It.IsAny<IReadOnlyCollection<Notification>>()), Times.Never);
            Assert.NotNull(retorno);
        }

        [Fact]
        public async Task NaoDeveArmazenarRendaPorPessoaQuandoDtoForInvalido()
        {
            //Given
            RendaPorPessoaDto dtoInvalido = null;

            //When
            var retorno = await _armanezadorDeRendaPorPessoa.Armazenar(dtoInvalido);

            //Then
            _rendaPorPessoaRepositorioMock.Verify(repositorio => repositorio.Incluir(It.IsAny<RendaPorPessoa>()), Times.Never);
            _rendaPorPessoaRepositorioMock.Verify(repositorio => repositorio.Salvar(), Times.Never);
            _notificadorMock.Verify(notificador => notificador.Notificar(It.IsAny<string>(), RendaPorPessoaDicionarioDeMensagensDeValidacao.MensagemDtoInvalido));
            Assert.Null(retorno);
        }

        [Fact]
        public async Task NaoDeveArmazenarRendaPorPessoaQuandoObjetoRendaPorPessoaForInvalido()
        {
            //Given
            string nomePessoaInvalido = null;
            var dto = new RendaPorPessoaDto
            {
                PessoaId = _faker.Random.Guid(),
                NomePessoa = nomePessoaInvalido,
                Valor = _faker.Random.Decimal(40)
            };

            //When
            var retorno = await _armanezadorDeRendaPorPessoa.Armazenar(dto);

            //Then
            _rendaPorPessoaRepositorioMock.Verify(repositorio => repositorio.Incluir(It.IsAny<RendaPorPessoa>()), Times.Never);
            _rendaPorPessoaRepositorioMock.Verify(repositorio => repositorio.Salvar(), Times.Never);
            _notificadorMock.Verify(notificador => notificador.Notificar(It.IsAny<IReadOnlyCollection<Notification>>()), Times.Once);
            Assert.Null(retorno);
        }
    }
}