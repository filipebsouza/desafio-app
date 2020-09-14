using System;
using System.Collections.Generic;

namespace Pontos.Dominio.Entidades
{
    public class Familia
    {
        protected Familia() { }

        public Familia(Guid id, List<Pessoa> pessoas, List<RendaPorPessoa> rendaPorPessoas, int statusDaFamilia)
        {
            Id = id;
            Pessoas = pessoas;
            Rendas = rendaPorPessoas;
            Status = statusDaFamilia;
        }

        public Guid Id { get; private set; }
        public virtual List<Pessoa> Pessoas { get; private set; }
        public virtual List<RendaPorPessoa> Rendas { get; private set; }
        public int Status { get; private set; }
    }
}