using System.Collections.Generic;
using System.Linq;
using Familias.Dominio.Entidades;
using Flunt.Validations;

namespace Familias.Dominio.Contratos
{
    public class FamiliaContrato : Contract
    {
        public FamiliaContrato(List<Pessoa> pessoas, List<RendaPorPessoa> rendaPorPessoas)
        {
            this
                .IsNotNull(
                    pessoas,
                    "Pessoas",
                    FamiliaDicionarioDeMensagensDeValidacao.MensagemDominioFamiliaPessoasInvalido
                )
                .IsGreaterOrEqualsThan(
                    pessoas?.Count ?? 0,
                    1,
                    "Pessoas",
                    FamiliaDicionarioDeMensagensDeValidacao.MensagemDominioFamiliaPessoasPeloMenosUmaPessoaDeveSerInserida
                )
                .IsNotNull(
                    rendaPorPessoas,
                    "Renda por Pessoas",
                    FamiliaDicionarioDeMensagensDeValidacao.MensagemDominioFamiliaPessoasInvalido
                )
                .IsGreaterOrEqualsThan(
                    rendaPorPessoas?.Count ?? 0,
                    1,
                    "Renda por Pessoas",
                    FamiliaDicionarioDeMensagensDeValidacao.MensagemDominioFamiliaPessoasPeloMenosUmaPessoaDeveSerInserida
                )
                .IsTrue(
                    RendaInformadaDeveFazerParteDeUmaDasPessoasDaFamilia(pessoas, rendaPorPessoas),
                    "Renda por Pessoas",
                    FamiliaDicionarioDeMensagensDeValidacao.MensagemDominioFamiliaRendaPorPessoasRendaDeveSerReferenteAhUmaPessoaInformada
                );
        }

        private bool RendaInformadaDeveFazerParteDeUmaDasPessoasDaFamilia(List<Pessoa> pessoas, List<RendaPorPessoa> rendaPorPessoas)
        {
            if (pessoas == null || pessoas.Count <= 0) return false;

            if (rendaPorPessoas == null || rendaPorPessoas.Count <= 0) return false;

            return rendaPorPessoas.All(renda =>
                pessoas.Select(p => p.Id).Contains(renda.PessoaId)
            );
        }
    }
}
