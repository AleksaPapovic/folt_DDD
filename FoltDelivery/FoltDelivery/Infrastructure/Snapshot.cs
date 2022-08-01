using System;

namespace FoltDelivery.Infrastructure
{
    public abstract class Snapshot:Entity
    {
        public int Version { get; set; }
        public Snapshot(Guid id) : base(id)
        {
        }
    }
}
