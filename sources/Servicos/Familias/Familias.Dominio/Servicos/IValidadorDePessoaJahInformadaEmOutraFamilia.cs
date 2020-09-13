using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Familias.Dominio.Servicos
{
    public interface IValidadorDePessoaJahInformadaEmOutraFamilia
    {
        Task<bool> PessoaJahFoiInformada(List<Guid> pessoaIds);
    }
}