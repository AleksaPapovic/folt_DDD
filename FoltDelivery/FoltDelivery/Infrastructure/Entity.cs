using System;

namespace FoltDelivery.Infrastructure
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public Entity(Guid id)
        {
            Id = id;
        }
    }
}
