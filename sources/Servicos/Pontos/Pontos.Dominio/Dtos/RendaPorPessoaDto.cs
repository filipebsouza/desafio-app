using System;

namespace Pontos.Dominio.Dtos
{
    public class RendaPorPessoaDto
    {
        public Guid Id { get; set; }
        public Guid PessoaId { get; set; }
        public decimal Valor { get; set; }

        public void Deconstruct(out Guid pessoaId, out decimal valor)
        {
            pessoaId = PessoaId;
            valor = Valor;
        }
    }
}