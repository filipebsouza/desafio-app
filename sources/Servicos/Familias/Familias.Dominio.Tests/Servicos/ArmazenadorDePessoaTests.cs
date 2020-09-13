// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Base.Dominio;
// using Bogus;
// using Flunt.Notifications;
// using Moq;
// using Familias.Dominio.Contratos;
// using Familias.Dominio.Dtos;
// using Familias.Dominio.Entidades;
// using Familias.Dominio.Repositorios;
// using Familias.Dominio.Servicos;
// using Xunit;

// namespace Familias.Dominio.Tests.Servicos
// {
//     public class ArmazenadorDePessoaTests
//     {
//         private readonly Faker _faker;
//         private readonly Mock<INotificadorBase> _notificadorMock;
//         private readonly Mock<IFamiliaRepositorio> _rendaPorPessoaRepositorioMock;
//         private readonly ArmazenadorDeFamilia _armanezadorDeFamilia;

//         public ArmazenadorDePessoaTests()
//         {
//             _faker = new Faker();
//             _notificadorMock = new Mock<INotificadorBase>();
//             _rendaPorPessoaRepositorioMock = new Mock<IFamiliaRepositorio>();
//             _armanezadorDeFamilia = new ArmazenadorDeFamilia(
//                 _notificadorMock.Object,
//                 _rendaPorPessoaRepositorioMock.Object
//             );
//         }

//         [Fact]
//         public async Task DeveArmazenarFamilia()
//         {
//             //Given
//             var dto = new FamiliaDto
//             {
//                 PessoaId = _faker.Random.Guid(),
//                 NomePessoa = _faker.Person.FullName,
//                 Valor = _faker.Random.Decimal(40)
//             };

//             //When
//             var retorno = await _armanezadorDeFamilia.Armazenar(dto);

//             //Then
//             _rendaPorPessoaRepositorioMock.Verify(repositorio => repositorio.Incluir(It.IsAny<Familia>()), Times.Once);
//             _rendaPorPessoaRepositorioMock.Verify(repositorio => repositorio.Salvar(), Times.Once);
//             _notificadorMock.Verify(notificador => notificador.Notificar(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
//             _notificadorMock.Verify(notificador => notificador.Notificar(It.IsAny<IReadOnlyCollection<Notification>>()), Times.Never);
//             Assert.NotNull(retorno);
//         }

//         [Fact]
//         public async Task NaoDeveArmazenarFamiliaQuandoDtoForInvalido()
//         {
//             //Given
//             FamiliaDto dtoInvalido = null;

//             //When
//             var retorno = await _armanezadorDeFamilia.Armazenar(dtoInvalido);

//             //Then
//             _rendaPorPessoaRepositorioMock.Verify(repositorio => repositorio.Incluir(It.IsAny<Familia>()), Times.Never);
//             _rendaPorPessoaRepositorioMock.Verify(repositorio => repositorio.Salvar(), Times.Never);
//             _notificadorMock.Verify(notificador => notificador.Notificar(It.IsAny<string>(), FamiliaDicionarioDeMensagensDeValidacao.MensagemDtoInvalido));
//             Assert.Null(retorno);
//         }

//         [Fact]
//         public async Task NaoDeveArmazenarFamiliaQuandoObjetoFamiliaForInvalido()
//         {
//             //Given
//             string nomePessoaInvalido = null;
//             var dto = new FamiliaDto
//             {
//                 PessoaId = _faker.Random.Guid(),
//                 NomePessoa = nomePessoaInvalido,
//                 Valor = _faker.Random.Decimal(40)
//             };

//             //When
//             var retorno = await _armanezadorDeFamilia.Armazenar(dto);

//             //Then
//             _rendaPorPessoaRepositorioMock.Verify(repositorio => repositorio.Incluir(It.IsAny<Familia>()), Times.Never);
//             _rendaPorPessoaRepositorioMock.Verify(repositorio => repositorio.Salvar(), Times.Never);
//             _notificadorMock.Verify(notificador => notificador.Notificar(It.IsAny<IReadOnlyCollection<Notification>>()), Times.Once);
//             Assert.Null(retorno);
//         }
//     }
// }