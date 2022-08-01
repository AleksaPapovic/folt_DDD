﻿using System;

namespace FoltDelivery.Infrastructure
{
    public class Entity
    {
        public Guid Id { get; protected set; }

        public Entity(Guid id)
        {
            Id = id;
        }
    }
}
