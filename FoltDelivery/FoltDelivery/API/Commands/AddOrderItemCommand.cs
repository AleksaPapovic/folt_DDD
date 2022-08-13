using FoltDelivery.API.DTO;
using FoltDelivery.Infrastructure.Commands;

namespace FoltDelivery.API.Commands
{
    public record AddOrderItemCommand(OrderItemsUpdateDTO NewOrderItem) : ICommand
    {
        public static AddOrderItemCommand Create(OrderItemsUpdateDTO newOrderItem)
        {
            //if (orderId == null)
            //    throw new ArgumentOutOfRangeException(nameof(orderId));

            return new AddOrderItemCommand(newOrderItem);
        }
    }
}
