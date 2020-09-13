using System;
using Base.Dominio;
using Rendas.Dominio.Contratos;

namespace Rendas.Dominio.Entidades
{
    public class RendaPorPessoa : EntidadeBase
    {
        private readonly RendaPorPessoaContrato _contrato;
        public RendaPorPessoa(Guid pessoaId, string nomePessoa, decimal valor) : base()
        {
            _contrato = new RendaPorPessoaContrato(pessoaId, nomePessoa, valor);

            if (_contrato.Valid)
            {
                PessoaId = pessoaId;
                NomePessoa = nomePessoa;
                Valor = valor;
            }
            else
            {
                Notificar(_contrato.Notifications);
            }
        }

        public Guid PessoaId { get; private set; }
        public string NomePessoa { get; private set; }
        public Decimal Valor { get; private set; }
    }
}