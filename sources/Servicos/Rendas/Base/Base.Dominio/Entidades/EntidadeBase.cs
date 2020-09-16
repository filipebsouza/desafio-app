using System;
using Base.Dominio.Notificacoes;

namespace Base.Dominio.Entidades
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
