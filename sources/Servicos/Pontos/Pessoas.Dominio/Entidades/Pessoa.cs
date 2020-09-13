using System;
using Base.Dominio;
using Pessoas.Dominio.Contratos;

namespace Pessoas.Dominio.Entidades
{
    public class Pessoa : EntidadeBase
    {
        private readonly PessoaContrato _contrato;
        public Pessoa(string nome, DateTime dataDeNascimento, TipoDaPessoaEnum tipoDaPessoa) : base()
        {
            _contrato = new PessoaContrato(nome, dataDeNascimento, tipoDaPessoa);

            if (_contrato.Valid)
            {
                Nome = nome;
                DataDeNascimento = dataDeNascimento.Date;
                TipoDaPessoa = tipoDaPessoa;
            }
            else
            {
                Notificar(_contrato.Notifications);
            }
        }

        public string Nome { get; private set; }
        public DateTime DataDeNascimento { get; private set; }
        public TipoDaPessoaEnum TipoDaPessoa { get; private set; }
    }
}
