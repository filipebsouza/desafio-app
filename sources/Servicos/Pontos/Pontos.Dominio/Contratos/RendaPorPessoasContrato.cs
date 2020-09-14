using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using Pontos.Dominio.Dtos;

namespace Pontos.Dominio.Contratos
{
    public class RendaPorPessoasContrato : Contract
    {
        public bool ValidarContrato(List<RendaPorPessoaDto> rendaPorPessoas)
        {
            if (rendaPorPessoas == null) return false;

            return rendaPorPessoas.Any(rendaPorPessoa =>
            {
                var (pessoaId, valor) = rendaPorPessoa;

                this
                    .IsNotNull(
                        pessoaId,
                        "Pessoa",
                        RendaPorPessoaDicionarioDeMensagensDeValidacao.MensagemDominioRendaPorPessoaIdInvalida
                    )
                    .IsGreaterThan(
                        valor,
                        0,
                        "Valor",
                        RendaPorPessoaDicionarioDeMensagensDeValidacao.MensagemDominioRendaPorPessoaValorNaoPorSerMenorOuIgualAhZero
                    );

                return this.Valid;
            });
        }
    }
}