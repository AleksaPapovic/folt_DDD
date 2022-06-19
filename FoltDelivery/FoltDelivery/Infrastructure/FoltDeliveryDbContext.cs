using Microsoft.EntityFrameworkCore;

namespace FoltDelivery.Model
{
    public class FoltDeliveryDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public FoltDeliveryDbContext(DbContextOptions<FoltDeliveryDbContext> options) : base(options)
        {
        }

        protected FoltDeliveryDbContext()
        {
        }
    }
}
