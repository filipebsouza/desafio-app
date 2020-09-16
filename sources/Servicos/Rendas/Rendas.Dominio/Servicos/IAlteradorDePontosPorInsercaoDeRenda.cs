using Rendas.Dominio.Dtos;

namespace Rendas.Dominio.Servicos
{
    public interface IAlteradorDePontosPorInsercaoDeRenda
    {
        void Alterar(RendaPorPessoaDto dto);
    }
}