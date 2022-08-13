using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Infrastructure;
using System;
using System.Collections.Generic;

namespace FoltDelivery.API.DTO
{
    public class MoneyDTO
    {
        public Double Amount { get;set; }

        public MoneyDTO(): this(0){}

        public MoneyDTO(Double amount)
        {
            ThrowExceptionIfNotValid(amount);

            Amount = amount;
        }

        private void ThrowExceptionIfNotValid(Double amount)
        {
            if (amount < 0)
                throw new ArgumentException("Money cannot be a negative amount");
        }
    }
}
