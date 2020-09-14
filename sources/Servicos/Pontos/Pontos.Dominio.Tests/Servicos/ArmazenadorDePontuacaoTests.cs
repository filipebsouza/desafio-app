// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Base.Dominio;
// using Bogus;
// using Flunt.Notifications;
// using Moq;
// using Pontos.Dominio.Contratos;
// using Pontos.Dominio.Dtos;
// using Pontos.Dominio.Entidades;
// using Pontos.Dominio.Repositorios;
// using Pontos.Dominio.Servicos;
// using Xunit;

// namespace Pontos.Dominio.Tests.Servicos
// {
//     public class ArmazenadorDePontuacaoTests
//     {
//         private readonly Faker _faker;
//         private readonly Mock<INotificadorBase> _notificadorMock;
//         private readonly Mock<IPontoRepositorio> _pessoaRepositorioMock;
//         private readonly ArmazenadorDePonto _armanezadorDePonto;

//         public ArmazenadorDePontuacaoTests()
//         {
//             _faker = new Faker();
//             _notificadorMock = new Mock<INotificadorBase>();
//             _pessoaRepositorioMock = new Mock<IPontoRepositorio>();
//             _armanezadorDePonto = new ArmazenadorDePonto(
//                 _notificadorMock.Object,
//                 _pessoaRepositorioMock.Object
//             );
//         }

//         [Fact]
//         public async Task DeveArmazenarPonto()
//         {
//             //Given
//             var dto = new PontoDto
//             {
//                 Nome = _faker.Person.FullName,
//                 DataDeNascimento = _faker.Date.Past().Date
//             };

//             //When
//             var retorno = await _armanezadorDePonto.Armazenar(dto);

//             //Then
//             _pessoaRepositorioMock.Verify(repositorio => repositorio.Incluir(It.IsAny<Ponto>()), Times.Once);
//             _pessoaRepositorioMock.Verify(repositorio => repositorio.Salvar(), Times.Once);
//             _notificadorMock.Verify(notificador => notificador.Notificar(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
//             _notificadorMock.Verify(notificador => notificador.Notificar(It.IsAny<IReadOnlyCollection<Notification>>()), Times.Never);
//             Assert.NotNull(retorno);
//         }

//         [Fact]
//         public async Task NaoDeveArmazenarPontoQuandoDtoForInvalido()
//         {
//             //Given
//             PontoDto dtoInvalido = null;

//             //When
//             var retorno = await _armanezadorDePonto.Armazenar(dtoInvalido);

//             //Then
//             _pessoaRepositorioMock.Verify(repositorio => repositorio.Incluir(It.IsAny<Ponto>()), Times.Never);
//             _pessoaRepositorioMock.Verify(repositorio => repositorio.Salvar(), Times.Never);
//             _notificadorMock.Verify(notificador => notificador.Notificar(It.IsAny<string>(), PontoDicionarioDeMensagensDeValidacao.MensagemDtoInvalido));
//             Assert.Null(retorno);
//         }

//         [Fact]
//         public async Task NaoDeveArmazenarPontoQuandoObjetoPontoForInvalido()
//         {
//             //Given
//             string nomePontoInvalido = null;
//             var dto = new PontoDto
//             {
//                 Nome = nomePontoInvalido,
//                 DataDeNascimento = _faker.Date.Past().Date
//             };

//             //When
//             var retorno = await _armanezadorDePonto.Armazenar(dto);

//             //Then
//             _pessoaRepositorioMock.Verify(repositorio => repositorio.Incluir(It.IsAny<Ponto>()), Times.Never);
//             _pessoaRepositorioMock.Verify(repositorio => repositorio.Salvar(), Times.Never);
//             _notificadorMock.Verify(notificador => notificador.Notificar(It.IsAny<IReadOnlyCollection<Notification>>()), Times.Once);
//             Assert.Null(retorno);
//         }
//     }
// }