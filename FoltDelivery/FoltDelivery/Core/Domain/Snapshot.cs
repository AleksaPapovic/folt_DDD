using System;

namespace FoltDelivery.Core.Domain
{
    public abstract class Snapshot : Entity
    {
        public int Version { get; set; }
        public Snapshot(Guid id) : base(id) { }
    }
}
