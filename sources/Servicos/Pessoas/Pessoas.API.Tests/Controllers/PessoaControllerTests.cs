using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Pessoas.Dominio.Dtos;
using Pessoas.Infra.Contextos;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Pessoas.Dominio.Entidades;

namespace Pessoas.API.Tests.Controllers
{
    public class PessoaControllerTests
    {
        private readonly string _sufixoDeTeste;
        private readonly Faker _faker;
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public PessoaControllerTests()
        {
            _sufixoDeTeste = "_TEST";
            _faker = new Faker();
            _server = new TestServer(
                new WebHostBuilder()
                    .UseStartup<Startup>()
                    .ConfigureServices(services =>
                    {
                        services.AddDbContext<PessoaContexto>(opt => opt.UseInMemoryDatabase($"PessoaDB{_sufixoDeTeste}"));
                    })
                );
            _client = _server.CreateClient();
            _client.BaseAddress = new Uri("http://localhost:5001");
        }

        private async Task<(HttpResponseMessage, PessoaDto)> PostPessoa(PessoaDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/pessoa", data);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            return (
                response,
                JsonConvert.DeserializeObject<PessoaDto>(responseString)
            );
        }

        private async Task SetupListarPessoas()
        {
            var pessoas = new List<PessoaDto>
            {
                new PessoaDto
                {
                    Nome = $"João da Silva Junior{_sufixoDeTeste}",
                    DataDeNascimento = new DateTime(1990, 08, 03),
                    TipoDaPessoa = (int)_faker.PickRandom<TipoDaPessoaEnum>()
                },
                new PessoaDto
                {
                    Nome = $"Milena dos Santos Villamayor{_sufixoDeTeste}",
                    DataDeNascimento = new DateTime(1990, 08, 12),
                    TipoDaPessoa = (int)_faker.PickRandom<TipoDaPessoaEnum>()
                },
                new PessoaDto
                {
                    Nome = $"É um teste Junior{_sufixoDeTeste}",
                    DataDeNascimento = new DateTime(1956, 09, 04),
                    TipoDaPessoa = (int)_faker.PickRandom<TipoDaPessoaEnum>()
                },
                new PessoaDto
                {
                    Nome = $"Mario Bowser Antonio{_sufixoDeTeste}",
                    DataDeNascimento = new DateTime(1966, 08, 23),
                    TipoDaPessoa = (int)_faker.PickRandom<TipoDaPessoaEnum>()
                },
                new PessoaDto
                {
                    Nome = $"Marilda Souza{_sufixoDeTeste}",
                    DataDeNascimento = new DateTime(1998, 07, 30),
                    TipoDaPessoa = (int)_faker.PickRandom<TipoDaPessoaEnum>()
                },
                new PessoaDto
                {
                    Nome = $"Alguém comum{_sufixoDeTeste}",
                    DataDeNascimento = new DateTime(1999, 08, 04),
                    TipoDaPessoa = (int)_faker.PickRandom<TipoDaPessoaEnum>()
                },
                new PessoaDto
                {
                    Nome = $"Fulano de Tals{_sufixoDeTeste}",
                    DataDeNascimento = new DateTime(1993, 03, 03),
                    TipoDaPessoa = (int)_faker.PickRandom<TipoDaPessoaEnum>()
                },
                new PessoaDto
                {
                    Nome = $"Getúlio de Lima{_sufixoDeTeste}",
                    DataDeNascimento = new DateTime(1978, 08, 01),
                    TipoDaPessoa = (int)_faker.PickRandom<TipoDaPessoaEnum>()
                },
                new PessoaDto
                {
                    Nome = $"Neto Antunes{_sufixoDeTeste}",
                    DataDeNascimento = new DateTime(1989, 07, 17),
                    TipoDaPessoa = (int)_faker.PickRandom<TipoDaPessoaEnum>()
                }
            };

            foreach (var pessoa in pessoas)
            {
                await PostPessoa(pessoa);
            }
        }

        [Fact]
        public async Task DeveListarPessoas()
        {
            //Given
            await SetupListarPessoas();

            //When
            var response = await _client.GetAsync("/api/pessoa");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var retorno = JsonConvert.DeserializeObject<List<PessoaDto>>(responseString);

            //Then
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(retorno);
            Assert.NotEmpty(retorno);            
        }

        [Fact]
        public async Task DeveInserirPessoa()
        {
            //Given
            var dto = new PessoaDto
            {
                Nome = _faker.Person.FullName,
                DataDeNascimento = _faker.Date.Past().Date,
                TipoDaPessoa = (int)_faker.PickRandom<TipoDaPessoaEnum>()
            };

            var (response, retorno) = await PostPessoa(dto);

            //Then
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(retorno);            
            Assert.Equal(dto.Nome, retorno.Nome);
            Assert.Equal(dto.DataDeNascimento, retorno.DataDeNascimento);
        }
    }
}