using FoltDelivery.API.DTO;
using FoltDelivery.Infrastructure.Commands;
using System;

namespace FoltDelivery.API.Commands
{
    public record CreateOrderCommand(OrderDTO order) : ICommand
    {
        public static CreateOrderCommand Create(OrderDTO newOrder)
        {
            //if (orderId == null)
            //    throw new ArgumentOutOfRangeException(nameof(orderId));

            return new CreateOrderCommand(newOrder);
        }
    }
}
