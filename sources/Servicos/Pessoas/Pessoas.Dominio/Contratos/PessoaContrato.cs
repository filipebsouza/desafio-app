using System;
using Flunt.Validations;

namespace Pessoas.Dominio.Contratos
{
    public class PessoaContrato : Contract
    {
        public PessoaContrato(string nome, DateTime dataDeNascimento)
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
                    PessoaDicionarioDeMensagensDeValidacao.MensagemDominioPessoaDataDeNascimentoNaoPodeSerMaioIgualAhHoje
                );
        }
    }
}
