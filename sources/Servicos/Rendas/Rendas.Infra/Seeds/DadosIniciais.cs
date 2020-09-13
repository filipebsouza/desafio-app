using System;
using System.Collections.Generic;
using Rendas.Dominio.Entidades;
using Rendas.Infra.Contextos;

namespace Rendas.Infra.Seeds
{
    public class DadosIniciais
    {
        private readonly RendaPorPessoaContexto _contexto;

        public DadosIniciais(RendaPorPessoaContexto contexto)
        {
            _contexto = contexto;
        }

        public void CriarDados()
        {
            var rendaPorPessoas = new List<RendaPorPessoa>
            {
                new RendaPorPessoa(Guid.NewGuid(), "João da Silva Junior", 50),
                new RendaPorPessoa(Guid.NewGuid(), "Milena dos Santos Villamayor", 500),
                new RendaPorPessoa(Guid.NewGuid(), "É um teste Junior", 150),
                new RendaPorPessoa(Guid.NewGuid(), "Mario Bowser Antonio", 50.9m),
                new RendaPorPessoa(Guid.NewGuid(), "Marilda Souza", 67.99m),
                new RendaPorPessoa(Guid.NewGuid(), "Alguém comum", 4239.89m),
                new RendaPorPessoa(Guid.NewGuid(), "Fulano de Tals", 10),
                new RendaPorPessoa(Guid.NewGuid(), "Getúlio de Lima", 34),
                new RendaPorPessoa(Guid.NewGuid(), "Neto Antunes", 46),
            };

            _contexto.RendaPorPessoas.AddRange(rendaPorPessoas);
            _contexto.SaveChanges();
        }
    }
}