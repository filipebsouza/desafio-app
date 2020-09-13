using System;
using Flunt.Validations;

namespace Rendas.Dominio.Contratos
{
    public class RendaPorPessoaContrato : Contract
    {
        public RendaPorPessoaContrato(Guid pessoaId, string nomePessoa, decimal valor)
        {
            this
                .IsNotNull(
                    pessoaId,
                    "Pessoa",
                    RendaPorPessoaDicionarioDeMensagensDeValidacao.MensagemDominioRendaPorPessoaIdInvalida
                )
                .IsNotNullOrEmpty(
                    nomePessoa,
                    "Nome",
                    RendaPorPessoaDicionarioDeMensagensDeValidacao.MensagemDominioRendaPorPessoaNomeInvalido
                )
                .HasMaxLen(
                    nomePessoa,
                    100,
                    "Nome", RendaPorPessoaDicionarioDeMensagensDeValidacao.MensagemDominioRendaPorPessoaNomeQuantidadeMaximaDeCaracteresEh100
                )
                .IsGreaterThan(
                    valor,
                    0,
                    "Valor",
                    RendaPorPessoaDicionarioDeMensagensDeValidacao.MensagemDominioRendaPorPessoaValorNaoPorSerMenorOuIgualAhZero
                );
        }
    }
}
