using FoltDelivery.API.DTO;
using FoltDelivery.Core.Commands;

namespace FoltDelivery.API.Commands
{
    public record RemoveOrderItemCommand(OrderUpdateDTO OrderUpdated) : ICommand
    {
        public static RemoveOrderItemCommand Create(OrderUpdateDTO OrderUpdated)
        {
            return new RemoveOrderItemCommand(OrderUpdated);
        }
    }
}
