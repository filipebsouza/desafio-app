using System;
using System.Collections.Generic;
using System.Linq;
using Familias.Dominio.Dtos;
using Flunt.Validations;

namespace Familias.Dominio.Contratos
{
    public class PessoasContrato : Contract
    {
        public bool ValidarContrato(List<PessoaDto> pessoas)
        {
            if (pessoas == null) return false;

            return pessoas.Any(pessoa =>
            {
                var (nome, dataDeNascimento, descricaoTipoDaPessoa) = pessoa;

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
                    )
                    .IsNotNullOrWhiteSpace(
                        descricaoTipoDaPessoa,
                        "Descrição Tipo da Pessoa",
                        PessoaDicionarioDeMensagensDeValidacao.MensagemDominioPessoaTipoDaPessoaInvalida
                    );

                return this.Valid;
            });
        }
    }
}