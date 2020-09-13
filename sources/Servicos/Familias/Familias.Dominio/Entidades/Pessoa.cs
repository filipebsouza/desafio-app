using System;

namespace Familias.Dominio.Entidades
{
    public class Pessoa
    {
        public Pessoa(Guid id, string nome, DateTime dataDeNascimento, string descricaoTipoDaPessoa)
        {
            Id = id;
            Nome = nome;
            DataDeNascimento = dataDeNascimento;
            DescricaoTipoDaPessoa = descricaoTipoDaPessoa;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataDeNascimento { get; private set; }
        public string DescricaoTipoDaPessoa { get; private set; }
    }
}