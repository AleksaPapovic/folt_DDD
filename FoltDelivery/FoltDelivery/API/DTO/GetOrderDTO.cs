using FoltDelivery.Core.Domain;
using System;

namespace FoltDelivery.API.DTO
{
    public class GetOrderDTO : Entity
    {
        public GetOrderDTO(Guid id) : base(id)
        {
        }
    }
}
