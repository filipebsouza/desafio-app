using System;
using System.Collections.Generic;

namespace Pontos.Dominio.Dtos
{
    public class FamiliaDto
    {
        public Guid Id { get; set; }
        public List<PessoaDto> Pessoas { get; set; }
        public List<RendaPorPessoaDto> RendaPorPessoas { get; set; }
        public int Status { get; set; }        
    }
}