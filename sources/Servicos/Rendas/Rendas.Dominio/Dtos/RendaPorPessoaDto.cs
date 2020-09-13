using System;

namespace Rendas.Dominio.Dtos
{
    public class RendaPorPessoaDto
    {
        public Guid Id { get; set; }
        public Guid PessoaId { get; set; }
        public string NomePessoa { get; set; }
        public decimal Valor { get; set; }
    }
}