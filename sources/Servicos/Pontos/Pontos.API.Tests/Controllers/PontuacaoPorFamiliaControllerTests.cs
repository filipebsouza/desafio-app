using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Pontos.Dominio.Dtos;
using Pontos.Infra.Contextos;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Pontos.Dominio.Entidades;

namespace Pontos.API.Tests.Controllers
{
    public class PontuacaoPorFamiliaControllerTests
    {
        private readonly string _sufixoDeTeste;
        private readonly Faker _faker;
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public PontuacaoPorFamiliaControllerTests()
        {
            _sufixoDeTeste = "_TEST";
            _faker = new Faker();
            _server = new TestServer(
                new WebHostBuilder()
                    .UseStartup<Startup>()
                    .ConfigureServices(services =>
                    {
                        services.AddDbContext<PontoContexto>(opt => opt.UseInMemoryDatabase($"PontoDB{_sufixoDeTeste}"));
                    })
                );
            _client = _server.CreateClient();
            _client.BaseAddress = new Uri("http://localhost:5004");
        }

        private async Task<(HttpResponseMessage, FamiliaDto)> PostFamilia(FamiliaDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/pontuacaoporfamilia", data);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            return (
                response,
                JsonConvert.DeserializeObject<FamiliaDto>(responseString)
            );
        }

        private async Task SetupListarFamilias()
        {
            var dto = new List<FamiliaDto>();
            var pessoaDto = new List<PessoaDto>
            {
                new PessoaDto
                {
                    Id = Guid.NewGuid(),
                    Nome = "João da Silva Junior",
                    DataDeNascimento = new DateTime(1990, 08, 03),
                    DescricaoTipoDaPessoa = "Cônjuge",
                    TipoDaPessoa = (int)TipoDaPessoaEnum.Conjuge
                },
                new PessoaDto
                {
                    Id = Guid.NewGuid(),
                    Nome = "Milena dos Santos Villamayor",
                    DataDeNascimento = new DateTime(1990, 08, 12),
                    DescricaoTipoDaPessoa = "Dependente",
                    TipoDaPessoa = (int)TipoDaPessoaEnum.Dependente
                },
                new PessoaDto
                {
                    Id = Guid.NewGuid(),
                    Nome = "É um teste Junior",
                    DataDeNascimento = new DateTime(1956, 09, 04),
                    DescricaoTipoDaPessoa = "Cônjuge",
                    TipoDaPessoa = (int)TipoDaPessoaEnum.Conjuge
                }
            };

            var rendaPorPessoasDto = new List<RendaPorPessoaDto>
            {
                new RendaPorPessoaDto
                {
                    Id = Guid.NewGuid(),
                    PessoaId = pessoaDto[0].Id,
                    Valor = 50
                },
                new RendaPorPessoaDto
                {
                    Id = Guid.NewGuid(),
                    PessoaId = pessoaDto[1].Id,
                    Valor = 500
                },
                new RendaPorPessoaDto
                {
                    Id = Guid.NewGuid(),
                    PessoaId = pessoaDto[2].Id,
                    Valor = 150
                },
            };

            dto.Add(new FamiliaDto
            {
                Pessoas = pessoaDto,
                RendaPorPessoas = rendaPorPessoasDto
            });

            pessoaDto = new List<PessoaDto>
            {
                new PessoaDto
                {
                    Id = Guid.NewGuid(),
                    Nome = "Getúlio de Lima",
                    DataDeNascimento = new DateTime(1990, 08, 03),
                    DescricaoTipoDaPessoa = "Cônjuge",
                    TipoDaPessoa = (int)TipoDaPessoaEnum.Conjuge
                },
                new PessoaDto
                {
                    Id = Guid.NewGuid(),
                    Nome =  "Alguém comum",
                    DataDeNascimento = new DateTime(1990, 08, 12),
                    DescricaoTipoDaPessoa = "Dependente",
                    TipoDaPessoa = (int)TipoDaPessoaEnum.Dependente
                },
            };

            rendaPorPessoasDto = new List<RendaPorPessoaDto>
            {
                new RendaPorPessoaDto
                {
                    Id = Guid.NewGuid(),
                    PessoaId = pessoaDto[0].Id,
                    Valor = 50
                },
            };

            dto.Add(new FamiliaDto
            {
                Pessoas = pessoaDto,
                RendaPorPessoas = rendaPorPessoasDto
            });

            foreach (var item in dto)
            {
                await PostFamilia(item);
            }
        }

        [Fact]
        public async Task DeveListarFamiliasComPontuacao()
        {
            //Given
            await SetupListarFamilias();

            //When
            var response = await _client.GetAsync("/api/pontuacaoporfamilia");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var retorno = JsonConvert.DeserializeObject<List<FamiliaDto>>(responseString);

            //Then
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(retorno);
            Assert.NotEmpty(retorno);
        }

        [Fact]
        public async Task DeveInserirPonto()
        {
            //Given
            var pessoaId = Guid.NewGuid();
            var dto = new FamiliaDto
            {
                Pessoas = new List<PessoaDto>
                {
                    new PessoaDto
                    {
                        Id = pessoaId,
                        Nome = _faker.Person.FullName,
                        DataDeNascimento = _faker.Date.Past().Date,
                        DescricaoTipoDaPessoa = "Cônjuge",
                        TipoDaPessoa = (int)TipoDaPessoaEnum.Conjuge
                    },
                },
                RendaPorPessoas = new List<RendaPorPessoaDto>
                {
                    new RendaPorPessoaDto
                    {
                        Id = Guid.NewGuid(),
                        PessoaId = pessoaId,
                        Valor = _faker.Random.Decimal(35)
                    },
                }
            };

            var (response, retorno) = await PostFamilia(dto);

            //Then
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(retorno);
        }
    }
}