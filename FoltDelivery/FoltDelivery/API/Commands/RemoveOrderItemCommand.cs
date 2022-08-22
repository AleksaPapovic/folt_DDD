using FoltDelivery.API.DTO;
using FoltDelivery.Infrastructure.Commands;

namespace FoltDelivery.API.Commands
{
    public record RemoveOrderItemCommand(OrderUpdateDTO OrderUpdated) : ICommand
    {
        public static RemoveOrderItemCommand Create(OrderUpdateDTO OrderUpdated)
        {
            //if (orderId == null)
            //    throw new ArgumentOutOfRangeException(nameof(orderId));

            return new RemoveOrderItemCommand(OrderUpdated);
        }
    }
}
