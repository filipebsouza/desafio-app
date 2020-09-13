namespace Familias.Dominio.Contratos
{
    public static class RendaPorPessoaDicionarioDeMensagensDeValidacao
    {
        public static string MensagemDominioRendaPorPessoaIdInvalida = "Vínculo com pessoa é inválido.";
        public static string MensagemDominioRendaPorPessoaNomeInvalido = "Nome é inválido.";
        public static string MensagemDominioRendaPorPessoaNomeQuantidadeMaximaDeCaracteresEh100 = "Nome não deve ter mais de 100 caracteres.";
        public static string MensagemDominioRendaPorPessoaValorNaoPorSerMenorOuIgualAhZero = "Valor não pode ser menor ou igual a Zero.";
        public static string MensagemDtoInvalido = "Dados de envio inválidos.";
    }
}