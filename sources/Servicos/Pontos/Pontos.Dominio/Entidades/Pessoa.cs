using System;

namespace Pontos.Dominio.Entidades
{
    public class Pessoa
    {
        protected Pessoa() { }
        public Pessoa(Guid id, string nome, DateTime dataDeNascimento, TipoDaPessoaEnum tipoDaPessoa)
        {
            Id = id;
            Nome = nome;
            DataDeNascimento = dataDeNascimento;
            TipoDaPessoa = tipoDaPessoa;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataDeNascimento { get; private set; }
        public TipoDaPessoaEnum TipoDaPessoa { get; private set; }
        public int Idade
        {
            get
            {
                var hoje = DateTime.Now;
                var idade = hoje.Year - DataDeNascimento.Year;

                if (DataDeNascimento.Date > hoje.AddYears(-idade)) idade--;

                return idade;
            }
        }
        public bool EhMaiorDeIdade
        {
            get
            {
                return DateTime.Now.Date >= DataDeNascimento.AddYears(18).Date;
            }
        }
    }
}