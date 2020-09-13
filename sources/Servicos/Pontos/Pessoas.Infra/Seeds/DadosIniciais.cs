using System;
using System.Collections.Generic;
using Pessoas.Dominio.Entidades;
using Pessoas.Infra.Contextos;

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
                new Pessoa("João da Silva Junior", new DateTime(1990, 08, 03), TipoDaPessoaEnum.Conjuge),
                new Pessoa("Milena dos Santos Villamayor", new DateTime(1990, 08, 12), TipoDaPessoaEnum.Conjuge),
                new Pessoa("É um teste Junior", new DateTime(1956, 09, 04), TipoDaPessoaEnum.Dependente),
                new Pessoa("Mario Bowser Antonio", new DateTime(1966, 08, 23), TipoDaPessoaEnum.Pretendente),
                new Pessoa("Marilda Souza", new DateTime(1998, 07, 30), TipoDaPessoaEnum.Conjuge),
                new Pessoa("Alguém comum", new DateTime(1999, 08, 04), TipoDaPessoaEnum.Dependente),
                new Pessoa("Fulano de Tals", new DateTime(1993, 03, 03), TipoDaPessoaEnum.Conjuge),
                new Pessoa("Getúlio de Lima", new DateTime(1978, 08, 01), TipoDaPessoaEnum.Pretendente),
                new Pessoa("Neto Antunes", new DateTime(1989, 07, 17), TipoDaPessoaEnum.Conjuge),
            };

            _contexto.Pessoas.AddRange(pessoas);
            _contexto.SaveChanges();
        }
    }
}