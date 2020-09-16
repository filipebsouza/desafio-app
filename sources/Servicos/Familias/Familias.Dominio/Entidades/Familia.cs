using System.Collections.Generic;
using Base.Dominio.Entidades;
using Familias.Dominio.Contratos;

namespace Familias.Dominio.Entidades
{
    public class Familia : EntidadeBase
    {
        private readonly FamiliaContrato _contrato;
        protected Familia() { }
        public Familia(List<Pessoa> pessoas, List<RendaPorPessoa> rendaPorPessoas) : base()
        {
            _contrato = new FamiliaContrato(pessoas, rendaPorPessoas);

            if (_contrato.Valid)
            {
                Pessoas = pessoas;
                Rendas = rendaPorPessoas;
                Status = StatusDaFamiliaEnum.CadastroCompleto;
            }
            else
            {
                Notificar(_contrato.Notifications);
            }
        }

        public virtual List<Pessoa> Pessoas { get; private set; }
        public virtual List<RendaPorPessoa> Rendas { get; private set; }
        public StatusDaFamiliaEnum Status { get; private set; }
    }
}