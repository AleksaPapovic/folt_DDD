using FoltDelivery.API.DTO;
using FoltDelivery.Infrastructure.Commands;

namespace FoltDelivery.API.Commands
{
    public record RemoveOrderItemCommand(OrderItemsUpdateDTO RemovedOrderItem) : ICommand
    {
        public static RemoveOrderItemCommand Create(OrderItemsUpdateDTO removedOrderItem)
        {
            //if (orderId == null)
            //    throw new ArgumentOutOfRangeException(nameof(orderId));

            return new RemoveOrderItemCommand(removedOrderItem);
        }
    }
}
