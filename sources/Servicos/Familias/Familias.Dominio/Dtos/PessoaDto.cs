using System;

namespace Familias.Dominio.Dtos
{
    public class PessoaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string DescricaoTipoDaPessoa { get; set; }

        public void Deconstruct(out string nome, out DateTime dataDeNascimento, out string descricaoTipoDaPessoa)
        {
            nome = Nome;
            dataDeNascimento = DataDeNascimento;
            descricaoTipoDaPessoa = DescricaoTipoDaPessoa;
        }
    }
}