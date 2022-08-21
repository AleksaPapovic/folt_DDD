using System;
using System.Linq;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Infrastructure.Persistance;

namespace FoltDelivery.API.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly FoltDeliveryDbContext _dbContext;

        public ProductRepository(FoltDeliveryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Money GetPrice(Guid productId)
        {
            return _dbContext.Products.FirstOrDefault(p => p.Id == productId).Price;
        }

    }
}
