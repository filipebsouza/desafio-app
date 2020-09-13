using System;
using Base.Dominio;
using Pessoas.Dominio.Contratos;

namespace Pessoas.Dominio.Entidades
{
    public class Pessoa : EntidadeBase
    {
        private readonly PessoaContrato _contrato;
        public Pessoa(string nome, DateTime dataDeNascimento) : base()
        {
            _contrato = new PessoaContrato(nome, dataDeNascimento);

            if (_contrato.Valid)
            {
                Nome = nome;
                DataDeNascimento = dataDeNascimento.Date;
            }
            else
            {
                Notificar(_contrato.Notifications);
            }
        }

        public string Nome { get; private set; }
        public DateTime DataDeNascimento { get; private set; }
    }
}
