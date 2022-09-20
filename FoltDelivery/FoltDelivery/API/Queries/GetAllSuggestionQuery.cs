using FoltDelivery.API.DTO;
using FoltDelivery.Core.Queries;
using System;

namespace FoltDelivery.API.Queries
{
    public record GetAllSuggestionQuery(OrderInCartDTO Order) : IQuery<SuggestionDTO>
    {
        public static GetAllSuggestionQuery Create(OrderInCartDTO order)
        {
            if (order == null)
                throw new ArgumentOutOfRangeException(nameof(order));

            return new GetAllSuggestionQuery(order);
        }
    }
}
