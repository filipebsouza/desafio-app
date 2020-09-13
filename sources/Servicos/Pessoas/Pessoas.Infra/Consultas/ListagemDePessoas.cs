using System.Collections.Generic;
using System.Linq;
using Pessoas.Dominio.Dtos;
using Pessoas.Dominio.Entidades;
using Pessoas.Infra.Contestos;

namespace Pessoas.Infra.Consultas
{
    public class ListagemDePessoas : IListagemDePessoas
    {
        private readonly PessoaContexto _pessoaContexto;

        public ListagemDePessoas(PessoaContexto pessoaContexto)
        {
            _pessoaContexto = pessoaContexto;
        }

        public List<PessoaDto> Listar()
        {
            return _pessoaContexto.Pessoas            
                .Select(p => new PessoaDto
                {
                    Id = p.Id,
                    DataDeNascimento = p.DataDeNascimento,
                    Nome = p.Nome
                })
                .ToList();
        }
    }
}