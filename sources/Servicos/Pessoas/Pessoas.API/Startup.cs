using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Base.Dominio;
using Base.Dominio.Notificacoes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pessoas.API.Filters;
using Pessoas.Dominio.Repositorios;
using Pessoas.Dominio.Servicos;
using Pessoas.Infra.Consultas;
using Pessoas.Infra.Contextos;
using Pessoas.Infra.Repositorios;

namespace Pessoas.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Base
            services.AddScoped(typeof(INotificadorBase), typeof(NotificadorBase));
            // Serviços
            services.AddScoped(typeof(IArmazenadorDePessoa), typeof(ArmazenadorDePessoa));
            // Consultas
            services.AddScoped(typeof(IListagemDePessoas), typeof(ListagemDePessoas));
            // Repositórios
            services.AddScoped(typeof(IPessoaRepositorio), typeof(PessoaRepositorio));

            services.AddDbContext<PessoaContexto>(opt => opt.UseInMemoryDatabase("PessoaDB"));

            services.AddControllers();
            services.AddMvcCore(options => options.Filters.Add<FiltrarNotificacoesFilter>());

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Pessoa API",
                    Description = "API para incluir e listar pessoas",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Filipe Bezerra de Souza",
                        Url = new Uri("https://www.filipe.dev")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.RoutePrefix = "swagger";
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Example");
            });
        }
    }
}
