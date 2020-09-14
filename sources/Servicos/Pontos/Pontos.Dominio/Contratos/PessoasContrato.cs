using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using Pontos.Dominio.Dtos;

namespace Pontos.Dominio.Contratos
{
    public class PessoasContrato : Contract
    {
        public bool ValidarContrato(List<PessoaDto> pessoas)
        {
            if (pessoas == null) return false;

            return pessoas.Any(pessoa =>
            {
                var (nome, dataDeNascimento) = pessoa;

                this
                    .IsNotNullOrEmpty(
                        nome,
                        "Nome",
                        PessoaDicionarioDeMensagensDeValidacao.MensagemDominioPessoaNomeInvalido
                    )
                    .HasMaxLen(
                        nome,
                        100,
                        "Nome", PessoaDicionarioDeMensagensDeValidacao.MensagemDominioPessoaNomeQuantidadeMaximaDeCaracteresEh100
                    )
                    .IsNotNull(
                        dataDeNascimento,
                        "Data de Nascimento",
                        PessoaDicionarioDeMensagensDeValidacao.MensagemDominioPessoaDataDeNascimentoInvalida
                    )
                    .IsLowerOrEqualsThan(
                        dataDeNascimento,
                        DateTime.Now.Date,
                        "Data de Nascimento",
                        PessoaDicionarioDeMensagensDeValidacao.MensagemDominioPessoaDataDeNascimentoNaoPodeSerMaiorIgualAhHoje
                    );

                return this.Valid;
            });
        }
    }
}