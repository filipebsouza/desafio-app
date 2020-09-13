using System;
using Flunt.Validations;
using Pessoas.Dominio.Entidades;

namespace Pessoas.Dominio.Contratos
{
    public class PessoaContrato : Contract
    {
        public PessoaContrato(string nome, DateTime dataDeNascimento, TipoDaPessoaEnum tipoDaPessoa)
        {
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
                .IsNotNull(
                    tipoDaPessoa,
                    "Tipo da Pessoa",
                    PessoaDicionarioDeMensagensDeValidacao.MensagemDominioPessoaTipoDaPessoaInvalida
                );
        }
    }
}
