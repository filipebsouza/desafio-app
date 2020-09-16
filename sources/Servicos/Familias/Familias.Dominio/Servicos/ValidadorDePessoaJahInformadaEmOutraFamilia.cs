using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.Dominio.Notificacoes;
using Familias.Dominio.Repositorios;

namespace Familias.Dominio.Servicos
{
    public class ValidadorDePessoaJahInformadaEmOutraFamilia : ServicoDeDominioBase, IValidadorDePessoaJahInformadaEmOutraFamilia
    {
        private readonly IFamiliaRepositorio _familiaRepositorio;

        public ValidadorDePessoaJahInformadaEmOutraFamilia(
            INotificadorBase notificador,
            IFamiliaRepositorio familiaRepositorio
        ) : base(notificador)
        {
            _familiaRepositorio = familiaRepositorio;
        }

        public async Task<bool> PessoaJahFoiInformada(List<Guid> pessoaIds)
        {
            var familias = await _familiaRepositorio.ObterFamiliasPorPessoaIds(pessoaIds);

            return familias != null && familias.Count > 0;
        }
    }
}