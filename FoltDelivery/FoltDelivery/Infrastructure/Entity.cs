using System;

namespace FoltDelivery.Model
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
    }
}
