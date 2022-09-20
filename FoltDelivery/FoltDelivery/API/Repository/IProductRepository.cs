using System;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Infrastructure.Persistance;

namespace FoltDelivery.API.Repository
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        public Money GetPrice(Guid productId);
    }
}
