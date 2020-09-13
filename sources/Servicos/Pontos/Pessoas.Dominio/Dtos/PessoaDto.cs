using System;

namespace Pessoas.Dominio.Dtos
{
    public class PessoaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public int TipoDaPessoa { get; set; }
    }
}