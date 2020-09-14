using System;
using System.Collections.Generic;
using Flunt.Validations;
using Pontos.Dominio.Entidades;

namespace Pontos.Dominio.Contratos
{
    public class PontuacaoPorFamiliaContrato : Contract
    {
        public PontuacaoPorFamiliaContrato(Familia familia)
        {
            this
                .IsNotNull(
                    familia,
                    "Família",
                    PontuacaoPorFamiliaDicionarioDeMensagensDeValidacao.MensagemDominioPontuacaoPorFamiliaObjetoFamiliaInvalida
                )
                .IsTrue(
                    ValidarPessoasDaFamilia(familia?.Pessoas),
                    "Pessoas",
                    PontuacaoPorFamiliaDicionarioDeMensagensDeValidacao.MensagemDominioPontuacaoPorFamiliaPessoasDaFamiliaInvalidas
                )
                .IsTrue(
                    ValidarRendaPorPessoaDaFamilia(familia?.Rendas),
                    "Rendas",
                    PontuacaoPorFamiliaDicionarioDeMensagensDeValidacao.MensagemDominioPontuacaoPorFamiliaRendasDasPessoasDaFamiliaInvalidas
                );
        }

        private bool ValidarRendaPorPessoaDaFamilia(List<RendaPorPessoa> rendas)
        {
            if (rendas == null || rendas.Count <= 0)
                return false;

            return true;
        }

        private bool ValidarPessoasDaFamilia(List<Pessoa> pessoas)
        {
            if (pessoas == null || pessoas.Count <= 0)
                return false;

            return true;
        }
    }
}
