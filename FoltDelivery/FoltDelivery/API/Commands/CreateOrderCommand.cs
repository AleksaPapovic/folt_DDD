using FoltDelivery.API.DTO;
using FoltDelivery.Core.Commands;
using System;

namespace FoltDelivery.API.Commands
{
    public record CreateOrderCommand(OrderDTO order) : ICommand
    {
        public static CreateOrderCommand Create(OrderDTO newOrder)
        {
            return new CreateOrderCommand(newOrder);
        }
    }
}
