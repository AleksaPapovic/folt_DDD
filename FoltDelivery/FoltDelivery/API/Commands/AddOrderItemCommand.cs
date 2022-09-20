using FoltDelivery.API.DTO;
using FoltDelivery.Core.Commands;
using System;

namespace FoltDelivery.API.Commands
{
    public record AddOrderItemCommand(OrderUpdateDTO OrderUpdated) : ICommand
    {
        public static AddOrderItemCommand Create(OrderUpdateDTO newOrderUpdated)
        {
            return new AddOrderItemCommand(newOrderUpdated);
        }
    }
}
