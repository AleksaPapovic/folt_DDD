using System;

namespace FoltDelivery.Core.Domain
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
