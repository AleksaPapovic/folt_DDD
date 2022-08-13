using System;

namespace FoltDelivery.Infrastructure
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public Entity(Guid id)
        {
            Id = id;
        }
    }
}
