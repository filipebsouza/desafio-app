namespace Pessoas.Dominio.Contratos
{
    public static class PessoaDicionarioDeMensagensDeValidacao
    {
        public static string MensagemDominioPessoaNomeInvalido = "Nome é inválido.";
        public static string MensagemDominioPessoaNomeQuantidadeMaximaDeCaracteresEh100 = "Nome não deve ter mais de 100 caracteres.";
        public static string MensagemDominioPessoaDataDeNascimentoInvalida = "Data de Nascimento é inválida.";
        public static string MensagemDominioPessoaDataDeNascimentoNaoPodeSerMaioIgualAhHoje = "Data de Nascimento não pode ser maior ou igual a hoje.";
        public static string MensagemDtoInvalido = "Dados de envio inválidos.";
    }
}