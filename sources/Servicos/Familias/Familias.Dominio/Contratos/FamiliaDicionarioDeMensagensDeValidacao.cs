namespace Familias.Dominio.Contratos
{
    public static class FamiliaDicionarioDeMensagensDeValidacao
    {
        public static string MensagemDominioFamiliaPessoasInvalido = "Pessoas atribuídas à família são inválidas.";
        public static string MensagemDominioFamiliaPessoasPeloMenosUmaPessoaDeveSerInserida = "Pelo menos uma pessoa deve compor a família.";
        public static string MensagemDominioFamiliaRendaPorPessoasInvalido = "Renda por pessoas atribuídas à família são inválidas.";
        public static string MensagemDominioFamiliaRendaPorPessoasPeloMenosUmaRendaPorPessoaDeveSerInserida = "Pelo menos uma renda deve compor a família.";
        public static string MensagemDominioFamiliaRendaPorPessoasRendaDeveSerReferenteAhUmaPessoaInformada = "A renda da família deve ser referente as pessoas informadas.";


        public static string MensagemDominioFamiliaIdInvalida = "Vínculo com pessoa é inválido.";
        public static string MensagemDominioFamiliaNomeQuantidadeMaximaDeCaracteresEh100 = "Nome não deve ter mais de 100 caracteres.";
        public static string MensagemDominioFamiliaValorNaoPorSerMenorOuIgualAhZero = "Valor não pode ser menor ou igual a Zero.";
        public static string MensagemDtoInvalido = "Dados de envio inválidos.";
    }
}