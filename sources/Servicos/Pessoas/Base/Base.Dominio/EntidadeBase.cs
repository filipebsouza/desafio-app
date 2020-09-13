using System;

namespace Base.Dominio
{
    public class EntidadeBase : NotificadorBase
    {
        protected EntidadeBase()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}
