using FoltDelivery.API.DTO;
using FoltDelivery.Infrastructure.Commands;

namespace FoltDelivery.API.Commands
{
    public record AddOrderItemCommand(OrderUpdateDTO OrderUpdated) : ICommand
    {
        public static AddOrderItemCommand Create(OrderUpdateDTO newOrderUpdated)
        {
            //if (orderId == null)
            //    throw new ArgumentOutOfRangeException(nameof(orderId));

            return new AddOrderItemCommand(newOrderUpdated);
        }
    }
}
