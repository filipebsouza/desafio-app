using System;

namespace Familias.Dominio.Entidades
{
    public class RendaPorPessoa
    {
        public RendaPorPessoa(Guid id, Guid pessoaId, decimal valor)
        {
            Id = id;
            PessoaId = pessoaId;
            Valor = valor;
        }

        public Guid Id { get; private set; }
        public Guid PessoaId { get; private set; }
        public Decimal Valor { get; private set; }
    }
}
