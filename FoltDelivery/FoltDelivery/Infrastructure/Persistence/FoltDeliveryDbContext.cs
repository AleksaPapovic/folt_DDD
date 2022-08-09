using System;
using FoltDelivery.Model.Enums;
using Microsoft.EntityFrameworkCore;
using FoltDelivery.Domain.Aggregates.RestaurantAggregate;
using FoltDelivery.Domain.Aggregates.CustomerAggregate;
using FoltDelivery.Domain.Aggregates.ProductAggregate;

namespace FoltDelivery.Infrastructure.Persistance
{
    public class FoltDeliveryDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }
        public FoltDeliveryDbContext(DbContextOptions<FoltDeliveryDbContext> options) : base(options)
        {
        }

        protected FoltDeliveryDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Address

            modelBuilder.Entity<Address>(addresses =>
            {
                addresses.HasData(
           new Address(new Guid("11223344-5566-7788-99AA-BBCCDDEEFF06"))
           {
               StreetName = "",
               HouseNumber = 1,
               City = "Novi Sad",
               PostalCode = 21000,
               State = "Srbija"
           },
           new Address(new Guid("11223344-5566-7788-99AA-BBCCDDEEFF08"))
           {
               StreetName = "",
               HouseNumber = 1,
               City = "Novi Sad",
               PostalCode = 21000,
               State = "Srbija"
           }
                );
            });
            #endregion

            #region Locations

            modelBuilder.Entity<Location>(locations =>
            {
                locations.HasData(
                    new
                    {
                        Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF05"),
                        AddressId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF06"),
                        Longitude = 19.8425,
                        Latitude = 45.2480,
                    }
                );

                locations.HasData(
                    new
                    {
                        Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF09"),
                        AddressId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF08"),
                        Longitude = 19.8357,
                        Latitude = 45.2589,
                    }
                );
            });

            #endregion

            #region Products

            modelBuilder.Entity<Product>(products =>
            {
                products.HasData(
                    new
                    {
                        Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF15"),
                        Name = "Vojvodjanska",
                        RestaurantMenuId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF15"),
                        Price = 1300.0,
                        Type = ProductType.DRINK,
                        RestaurantId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                        Quantity = 1,
                        Description = "Pica sa tradicijom",
                        Image = "",
                        LogicalDeleted = false
                    }
                );

                products.HasData(
                    new
                    {
                        Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF16"),
                        Name = "Vojvodjanska",
                        Price = 1300.0,
                        Type = ProductType.DRINK,
                        RestaurantMenuId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF15"),
                        RestaurantId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                        Quantity = 1,
                        Description = "Pica sa tradicijom",
                        Image = "",
                        LogicalDeleted = false
                    }
                );
            });

            #endregion

            #region RestaurantMenus

            modelBuilder.Entity<RestaurantMenu>(restaurantMenus =>
            {
                restaurantMenus.HasData(
                    new
                    {
                        Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF15"),
                    }
                );

                restaurantMenus.HasData(
                    new
                    {
                        Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF16"),
                    }
                );
            });

            #endregion


            #region Restaurants

            modelBuilder.Entity<Restaurant>(restaurants =>
            {
                restaurants.HasData(
                    new
                    {
                        Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                        LocationId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF05"),
                        Name = "Pizzeria Ciao",
                        Type = "Restaurant",
                        MenuId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF15"),
                        Status = RestaurantStatus.OPEN,
                        LogoId = 1,
                        Deleted = false,
                    },
                    new
                    {
                        Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF01"),
                        LocationId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF09"),
                        Name = "Boom boom pancakes",
                        Type = "FastFood",
                        MenuId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF16"),
                        Status = RestaurantStatus.OPEN,
                        LogoId = 2,
                        Deleted = false,

                    });



            });
            modelBuilder.Entity<Restaurant>().Property(e => e.Id).HasIdentityOptions(startValue: 3);

            #endregion


        }
    }
}
