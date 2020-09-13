using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Rendas.Infra.Contextos.MetodosDeExtensao
{
    public class AutoIncrementoParaBancoEmMemoria<TEntity> : InMemoryIntegerValueGenerator<Guid> where TEntity : class
    {
        public AutoIncrementoParaBancoEmMemoria(int propertyIndex) : base(propertyIndex)
        { }

        public override bool GeneratesTemporaryValues => false;
        public override Guid Next(EntityEntry entry)
        {
            Guid chave;

            if (entry.Properties.Any(x => x.Metadata.ValueGenerated == ValueGenerated.OnAdd))
                chave = Guid.NewGuid();
            else
                chave = base.Next(entry);

            return chave;
        }
    }
}
