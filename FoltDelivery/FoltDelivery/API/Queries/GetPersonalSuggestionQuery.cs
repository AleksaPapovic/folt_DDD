using FoltDelivery.API.DTO;
using FoltDelivery.Core.Queries;
using System;

namespace FoltDelivery.API.Queries
{
    public record GetPersonalSuggestionQuery(OrderInCartDTO Order) : IQuery<SuggestionDTO>
    {
        public static GetPersonalSuggestionQuery Create(OrderInCartDTO order)
        {
            if (order == null)
                throw new ArgumentOutOfRangeException(nameof(order));

            return new GetPersonalSuggestionQuery(order);
        }
    }
}
