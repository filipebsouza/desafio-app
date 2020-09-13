using System;
using System.Collections.Generic;
using Familias.Dominio.Entidades;
using Familias.Infra.Contextos;

namespace Familias.Infra.Seeds
{
    public class DadosIniciais
    {
        private readonly FamiliaContexto _contexto;

        public DadosIniciais(FamiliaContexto contexto)
        {
            _contexto = contexto;
        }

        public void CriarDados()
        {
            var familias = new List<Familia>();
            var pessoas = new List<Pessoa>
            {
                new Pessoa(Guid.NewGuid(), "João da Silva Junior", new DateTime(1990, 08, 03), "Cônjuge"),
                new Pessoa(Guid.NewGuid(), "Milena dos Santos Villamayor", new DateTime(1990, 08, 12), "Dependente"),
                new Pessoa(Guid.NewGuid(), "É um teste Junior", new DateTime(1956, 09, 04), "Cônjuge")
            };

            var rendaPorPessoas = new List<RendaPorPessoa>
            {
                new RendaPorPessoa(Guid.NewGuid(), pessoas[0].Id, 50),
                new RendaPorPessoa(Guid.NewGuid(), pessoas[1].Id, 500),
                new RendaPorPessoa(Guid.NewGuid(), pessoas[2].Id, 150),
            };

            familias.Add(new Familia(
                pessoas,
                rendaPorPessoas
             ));

            pessoas = new List<Pessoa>
            {
                new Pessoa(Guid.NewGuid(), "Getúlio de Lima", new DateTime(1990, 08, 03), "Cônjuge"),
                new Pessoa(Guid.NewGuid(),  "Alguém comum", new DateTime(1990, 08, 12), "Dependente"),                
            };

            rendaPorPessoas = new List<RendaPorPessoa>
            {
                new RendaPorPessoa(Guid.NewGuid(), pessoas[0].Id, 50),                
            };

            familias.Add(new Familia(
                pessoas,
                rendaPorPessoas
             ));

            _contexto.Familias.AddRange(familias);
            _contexto.SaveChanges();
        }
    }
}