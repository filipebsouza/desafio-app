using System;
using System.Collections.Generic;
using Pessoas.Dominio.Entidades;
using Pessoas.Infra.Contestos;

namespace Pessoas.Infra.Seeds
{
    public class DadosIniciais
    {
        private readonly PessoaContexto _contexto;

        public DadosIniciais(PessoaContexto contexto)
        {
            _contexto = contexto;
        }

        public void CriarDados()
        {
            var pessoas = new List<Pessoa>
            {
                new Pessoa("João da Silva Junior", new DateTime(1990, 08, 03)),
                new Pessoa("Milena dos Santos Villamayor", new DateTime(1990, 08, 12)),
                new Pessoa("É um teste Junior", new DateTime(1956, 09, 04)),
                new Pessoa("Mario Bowser Antonio", new DateTime(1966, 08, 23)),
                new Pessoa("Marilda Souza", new DateTime(1998, 07, 30)),
                new Pessoa("Alguém comum", new DateTime(1999, 08, 04)),
                new Pessoa("Fulano de Tals", new DateTime(1993, 03, 03)),
                new Pessoa("Getúlio de Lima", new DateTime(1978, 08, 01)),
                new Pessoa("Neto Antunes", new DateTime(1989, 07, 17)),
            };

            _contexto.Pessoas.AddRange(pessoas);
            _contexto.SaveChanges();
        }
    }
}