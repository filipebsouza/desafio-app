using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Rendas.Dominio.Dtos;
using Rendas.Infra.Contextos;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Rendas.API.Tests.Controllers
{
    public class RendaPorPessoaControllerTests
    {
        private readonly string _sufixoDeTeste;
        private readonly Faker _faker;
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public RendaPorPessoaControllerTests()
        {
            _sufixoDeTeste = "_TEST";
            _faker = new Faker();
            _server = new TestServer(
                new WebHostBuilder()
                    .UseStartup<Startup>()
                    .ConfigureServices(services =>
                    {
                        services.AddDbContext<RendaPorPessoaContexto>(opt => opt.UseInMemoryDatabase($"RendaPorPessoaDB{_sufixoDeTeste}"));
                    })
                );
            _client = _server.CreateClient();
            _client.BaseAddress = new Uri("http://localhost:5002");
        }

        private async Task<(HttpResponseMessage, RendaPorPessoaDto)> PostRendaPorPessoa(RendaPorPessoaDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/rendaporpessoa", data);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            return (
                response,
                JsonConvert.DeserializeObject<RendaPorPessoaDto>(responseString)
            );
        }

        private async Task SetupListarRendaPorPessoas()
        {
            var rendaPorPessoas = new List<RendaPorPessoaDto>
            {
                new RendaPorPessoaDto
                {
                    PessoaId = _faker.Random.Guid(),
                    NomePessoa = $"João da Silva Junior{_sufixoDeTeste}",
                    Valor = _faker.Random.Decimal(1)
                },
                new RendaPorPessoaDto
                {
                    PessoaId = _faker.Random.Guid(),
                    NomePessoa = $"Milena dos Santos Villamayor{_sufixoDeTeste}",
                    Valor = _faker.Random.Decimal(14)
                },
                new RendaPorPessoaDto
                {
                    PessoaId = _faker.Random.Guid(),
                    NomePessoa = $"É um teste Junior{_sufixoDeTeste}",
                    Valor = _faker.Random.Decimal(1400)
                },
                new RendaPorPessoaDto
                {
                    PessoaId = _faker.Random.Guid(),
                    NomePessoa = $"Mario Bowser Antonio{_sufixoDeTeste}",
                    Valor = _faker.Random.Decimal(50)
                },
                new RendaPorPessoaDto
                {
                    PessoaId = _faker.Random.Guid(),
                    NomePessoa = $"Marilda Souza{_sufixoDeTeste}",
                    Valor = _faker.Random.Decimal(4000)
                },
                new RendaPorPessoaDto
                {
                    PessoaId = _faker.Random.Guid(),
                    NomePessoa = $"Alguém comum{_sufixoDeTeste}",
                    Valor = _faker.Random.Decimal(900)
                },
                new RendaPorPessoaDto
                {
                    PessoaId = _faker.Random.Guid(),
                    NomePessoa = $"Fulano de Tals{_sufixoDeTeste}",
                    Valor = _faker.Random.Decimal(14)
                },
                new RendaPorPessoaDto
                {
                    PessoaId = _faker.Random.Guid(),
                    NomePessoa = $"Getúlio de Lima{_sufixoDeTeste}",
                    Valor = _faker.Random.Decimal(1)
                },
                new RendaPorPessoaDto
                {
                    PessoaId = _faker.Random.Guid(),
                    NomePessoa = $"Neto Antunes{_sufixoDeTeste}",
                    Valor = _faker.Random.Decimal(70)
                }
            };

            foreach (var rendaPorPessoa in rendaPorPessoas)
            {
                await PostRendaPorPessoa(rendaPorPessoa);
            }
        }

        [Fact]
        public async Task DeveListarRendaPorPessoas()
        {
            //Given
            await SetupListarRendaPorPessoas();

            //When
            var response = await _client.GetAsync("/api/rendaporpessoa");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var retorno = JsonConvert.DeserializeObject<List<RendaPorPessoaDto>>(responseString);

            //Then
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(retorno);
            Assert.NotEmpty(retorno);
        }

        [Fact]
        public async Task DeveInserirPessoa()
        {
            //Given
            var dto = new RendaPorPessoaDto
            {
                PessoaId = _faker.Random.Guid(),
                NomePessoa = _faker.Person.FullName,
                Valor = _faker.Random.Decimal(30, 567)
            };

            var (response, retorno) = await PostRendaPorPessoa(dto);

            //Then
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(retorno);            
            Assert.Equal(dto.PessoaId, retorno.PessoaId);
            Assert.Equal(dto.NomePessoa, retorno.NomePessoa);
            Assert.Equal(dto.Valor, retorno.Valor);
        }
    }
}