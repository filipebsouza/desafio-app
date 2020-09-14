using System;

namespace Pontos.Dominio.Dtos
{
    public class PessoaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public int TipoDaPessoa { get; set; }
        public string DescricaoTipoDaPessoa { get; set; }

        public void Deconstruct(out string nome, out DateTime dataDeNascimento)
        {
            nome = Nome;
            dataDeNascimento = DataDeNascimento;
        }
    }
}