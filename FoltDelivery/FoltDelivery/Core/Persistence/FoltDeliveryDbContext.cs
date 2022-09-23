using FoltDelivery.Domain.Aggregates.CustomerAggregate;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Domain.Aggregates.RestaurantAggregate;
using FoltDelivery.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace FoltDelivery.Core.Persistance
{
    public class FoltDeliveryDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Product> Products { get; set; }

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
                        Type = ProductType.DRINK,
                        RestaurantId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                        Quantity = 1,
                        Description = "Pica sa tradicijom",
                        Image = "https://stil.kurir.rs/data/images/2017/06/30/20/120607_pica_ls.jpg",
                        LogicalDeleted = false,
                        InitialVersion = 0,
                        Version = 0,
                    }
                );

                products.HasData(
                    new
                    {
                        Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF16"),
                        Name = "TRIKOLORE",
                        Type = ProductType.DRINK,
                        RestaurantMenuId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF15"),
                        RestaurantId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                        Quantity = 1,
                        Description = "Pica sa tradicijom",
                        Image = "https://scontent.fbeg1-1.fna.fbcdn.net/v/t1.6435-9/89189397_10158536361014050_9143639258949484544_n.jpg?stp=dst-jpg_p843x403&_nc_cat=108&ccb=1-7&_nc_sid=8bfeb9&_nc_ohc=i-3299St7_UAX8ljWKb&_nc_ht=scontent.fbeg1-1.fna&oh=00_AT85srxLBdHg0LQN11CsEAx3Y0lXrrozesPb_6jXKG7OLw&oe=634B5E7E",
                        LogicalDeleted = false,
                        InitialVersion = 0,
                        Version = 0,
                    }
                );

                products.HasData(
                    new
                    {
                        Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF17"),
                        Name = "KAPRIĆOZA",
                        Type = ProductType.DRINK,
                        RestaurantMenuId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF15"),
                        RestaurantId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                        Quantity = 1,
                        Description = "Pica sa tradicijom",
                        Image = "https://media-cdn.tripadvisor.com/media/photo-s/0f/59/77/04/ciao-porodicna-kapricoza.jpg",
                        LogicalDeleted = false,
                        InitialVersion = 0,
                        Version = 0,
                    }
                );

                products.HasData(
                    new
                    {
                        Id = new Guid("22223344-5566-7788-99AA-BBCCDDEEFF10"),
                        Name = "Palacinka Rafaelo",
                        Type = ProductType.DRINK,
                        RestaurantMenuId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF16"),
                        RestaurantId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF01"),
                        Quantity = 1,
                        Description = "Pica sa tradicijom",
                        Image = "https://cdn.navidiku.rs/firme/proizvodgalerija3/galerija55141/raffaello-palacinka-novi-sad-97237.jpg",
                        LogicalDeleted = false,
                        InitialVersion = 0,
                        Version = 0,
                    }
                );

                products.HasData(
                    new
                    {
                        Id = new Guid("22223344-5566-7788-99AA-BBCCDDEEFF11"),
                        Name = "Palacinka Oreo",
                        Type = ProductType.DRINK,
                        RestaurantMenuId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF16"),
                        RestaurantId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF01"),
                        Quantity = 1,
                        Description = "Pica sa tradicijom",
                        Image = "data:image/jpeg;base64,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",
                        LogicalDeleted = false,
                        InitialVersion = 0,
                        Version = 0,
                    }
                );

                products.HasData(
                    new
                    {
                        Id = new Guid("22223344-5566-7788-99AA-BBCCDDEEFF12"),
                        Name = "Palacinka Nutela",
                        Type = ProductType.DRINK,
                        RestaurantMenuId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF16"),
                        RestaurantId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF01"),
                        Quantity = 1,
                        Description = "Pica sa tradicijom",
                        Image = "data:image/jpeg;base64,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",
                        LogicalDeleted = false,
                        InitialVersion = 0,
                        Version = 0,
                    }
                );

                products.OwnsOne(pr => pr.Price).HasData(
                    new
                    {
                        ProductId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF15"),
                        Amount = 1300.00
                    },
                    new
                    {
                        ProductId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF16"),
                        Amount = 1650.00
                    },
                    new
                    {
                        ProductId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF17"),
                        Amount = 1350.00
                    },
                    new
                    {
                        ProductId = new Guid("22223344-5566-7788-99AA-BBCCDDEEFF10"),
                        Amount = 280.00
                    },
                    new
                    {
                        ProductId = new Guid("22223344-5566-7788-99AA-BBCCDDEEFF11"),
                        Amount = 360.00
                    },
                    new
                    {
                        ProductId = new Guid("22223344-5566-7788-99AA-BBCCDDEEFF12"),
                        Amount = 240.00
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

                restaurantMenus.HasData(
                 new
                 {
                     Id = new Guid("21223344-5566-7788-99AA-BBCCDDEEFF10"),
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
                        InitialVersion = 0,
                        Version = 0,
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
                        InitialVersion = 0,
                        Version = 0,
                    });
         

             restaurants.OwnsOne(re => re.Logo).HasData(
                    new
                    {
                        RestaurantId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                        Image = "https://10619-2.s.cdn12.com/rests/original/325_507751715.jpg"
                    },
                    new
                    {
                        RestaurantId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF01"),
                        Image = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAoHCBUVFRgWFRYYGBgaGhgYGBgYGhoaGhoYGBwaGhocGhwcIS4lHB4rIRgYJjgmKy8xNTU1HCQ7QDs0Py40NTEBDAwMEA8QHhISHjErISE2MTQ0NDE0MTE0NDQ0MTQ0NDQ0NDQ0NDQ0NDExNDQ0MTg0NDQ6PzQ1NDQ0MTE0NDQ9Mf/AABEIAOAA4QMBIgACEQEDEQH/xAAbAAABBQEBAAAAAAAAAAAAAAAAAQIDBAUGB//EAEQQAAEDAQQFCAcFBwQDAQAAAAEAAhEDBBIhMQVBUWGhBhMiYnGRseEUFTJCUoHRgqLB8PEWU3KSssLSIzNDg1Rjcwf/xAAZAQEBAQEBAQAAAAAAAAAAAAAAAQIDBAX/xAAhEQEBAAMAAgIDAQEAAAAAAAAAAQIREiExA0EEE1GBcf/aAAwDAQACEQMRAD8A8ZQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCAQhCCUsGqU24nIRSXAkup6IVDSxJdCkEpCEDbgRcCcEoUCCijmxrn87VO15/Tu15JwfGpBC2i0xnj2Jxs7RmTHy+ia9wmQhpAGMk9sIGGljAKTmwrDqYwnX+daa9hGzagr3QlDAnga4Rd1IGtp7UFg37lMRtw3b0rWyYJyyQRGjAE69SVlEHWZ2KR5xaNh70PukyMZ1fjuQR1KIAGaY5gH6p7yTEnsQ1u7tQKKAJwJjVvTTTbEyZSiNaR4gwFUIyjPildSbIAJ3pWPOO+OCkpU8Z8EETaMkjHCU1lOdquOcAoqLIcUU30cb+8fRKrKEFBCVCBEShEIHNKJTUqB2CRIlUFiinOAjtULHxkMUZ5oEcNiaApOb4pLpQDyTCY7H85p7sEXcQJzlAjGbcBrU3NjYosSd/iE9j+8IHPE+PzQ8Shqa92aB10a1XG3HtU4qYQmYR4BBG6e3XgnlkDPf2lMa3WkAk5SqFOeCLsJ1zGErqcGJx4IEa2cIyQHFv0KBIIJ3pHvk5IFD1KyoSoIKAVYLEjahQyNgQroRpEFELIEqEAICEIQEAlCRKECsKkkqMBKFBYG0IOSjpqQjBRdIycdqQngnBiCwqqS8kvYpxYm3SiFc8qNSFhQaZVNGJqkLCkLCiGhOovgmUlwpbqB14XpGv6JtR8xuRzZTQE0Co+e8nvSCRqSwnsbMJoJccRkEASnlupIArA26hOhC0IEIQsAQUQi6gEqVCBEsIhLCgQBPAQGqVrQUWGsZsUzWK5o/Rz6jg1jS9xnBuOQJ/Are0Zom4avOU3h7WBwvU+ca0SJc5sgExEXjGOKzco3lhnMesZb/AMm3LNanBk5LtKbXtPOuZafYayk99Nrh0nSSAIa2cAACczim2ypaG1W1DZ3NcGllFzmOY4ucA0l8ESYL4AyLlO3GT57dT464xzI3Jt1dfXr2gFtPmHte5t55IeXuIkSA4mWACIIOtTVbBYix7WUnl7WEkAVudBGt0i4ADnqTst+XHXXx3z/PLiYRCtvsrwA4sddJIBIME7j3KEsW5du2WOWPuIC1EKS6nuouBggg7CCCq5WxXIRCluE5BJdVibRwkexSFqLqqq0Jwzw7VK5kpGHB3YgTNIhqVIEQiEKqsDQlpLQ4WauWkBwcKT7pacQQbsRGtVH0XNMPa5p1hzSD3EKSlbKjcG1HgRADXuGGzA5bktprPc433Oc4YEucXHDVJXLdFdrU4jenMJ1K1Ta8dJt3KcbpG3JwI+SW6WRULQM1NSsjneziugsfLHSAYKbLS8MaCGtu08G5wCWTAxgahACn0XabXSe2u1jT0sHvFO6XiCQC4xOI71Llo05etZXMMOBB1g4FI2nO9dLym0tWtVV1RzWScDcDLsjojpDMYZyVWslorUw0ijQcHAwX0qNQkZH2gS3HbCdLMbWLTp4xh+q1dHaJqVMGMJ7MclsC32vmTTNCmWNiS2z0y5oeS5vSAJaMwMoAA3La5IcsfRjDqVIjGbrGsqd4+m1ZuS3Cxh6Aqts9VxqgAXKjbpvQS5pABjGDMZres9fn6dUtABLabGMZekNYQdc4QMyVS0zygNoe5rLPS6Ti4AU2X4wlrXAXowJOe1bPJjlSyxlzX2drJAEAPDp1TeJw1xvWLOq9nxfk54Yak9KVuYxtFtNtZnuufJdec7UMsGtGUHHpHs0KDG85QDHsfSa9plrpJdIl79k8B85wrfylY973ChQIdBAfTBukT7ERdmROeS2eSXKynZy+/RZLoAuYEk4gESQBB1BOXS/nXn0gdWZSrPc5zQ3mi1obVDveaHNDhk4i9vUNvtVNzq7w5t11Kk0S8FxM0jE5uIDXSd2qVicpNMCs+WUKVMAu9hjml0kGH9LMQMozKxPSRPsMI+E85HB88U4dJ+d93Hy7TTnNGyBjarHGncugEzLg41MYg9IjInABcTSs73nBs+WKe60NL5uNaDmxpfdiZiXOLhMZzIldnyF05ZbO4OqUGjAy+89xGyGuJb4LU8PH+R8v7fOvTmNFWdzKpDui4NfcmABUuuuSTkZy611adS2VnGqXFocyzFvQOIvObIcZm9BJI1Y5GVJyr0vZ6ldz2WZoBdecL7wXDMzBgSNi52nXoBjg6i57pwdzpZdGo3Qwhy1Lv6fN+T4Or1t0+hKFOgG0y/8A1KoebzHBwuik4sBBjC88iJxcwKHRANNgNCmytLqrapLRhkGF0+yy70scDJnLDlhUpknoGMeiKhEfMtM+S0tC16DKgc9j3AHJrwDjkb1w+CeXK/i27tvv2j03ZA2o91NsMkRE3Zui9dn3ZmN0LIN7YvWOWOmLBUoNZTpC+BqN2DAkdH2zh9F54K1mk36dYiMCytTbjGEh1J0iY1hamT0446xk/jIk7PFAB2K5Z30h7barttyoxnbF6m5SVOZL/wDTFQNwwe5rn549JrGjZq2rfUb0gZZS4SAVHVolpgheochrVo9jHms1zjGBeAROd1oGPfwXJ8q32d1ZzqDXNYTIBdOeOUYd+tTpHLXULRmh/wC37n0Sq9KxqHtN7R4p1Yy49p8U2kOkO1DpJPauf2v0VhhWqUtBkgy0uwIPtNmCRkccRmCqwHhxUtHJ38LvBKY+17R5Z0ecJDcZu4HFvmu3sj7BVrMs4r1PQ5D3tqOAl5ZUbMjIyWfJpXB2azl7DDSY2ThhJy3A9ylq2YBpznDXq1eK5ZSW+29bdVyqsNkpPeLHVlgph0l1+Xkkls54tDY3lcsy2Pc7pm9ERu6bFAxkyMhIHipmWWbwGcSRuBBngtTUmr7XGW3cPsWkKjQWte5og4Nc4TAMTBxgnCclfo6crG8wv6JvOyEy1r4xz1rMbQuzliD4hNokEzEEBxwOeBGv84pZL9Om85NW+GrY9IOvNeWjouAlstMwYM9pHcMlVt+kH1HNc97nuIEl5JJiQMTjkApKNMFkyQL2IgkSWkg/cd+kxnvZMXTgNuvNXHX0xcsta34LfxUlnqAHE4KG4QkbMkd6rMWqlVpGeP5hLZ3tY4GQdox3bVBdO5Jc7NSLtsmoxzHGAS6Lro9k324iRIwEKOmQC5zjJlszsBHmoaVkqOY27kROWx5+gUtPR7332wAXAGTMQDB1b1j/AFdqtpqMIgOHtPPykkd6oPxJjctM6Gc5pcHCQSIIgG6S32jlMbFlFp4reLnfIIyOOzJKx5GSaRsHyR2Ba2liWpWJzKWmwh4wDtcHIgZziJULKbnODADecQ1u8uIA4kLsOS+jWc61zg2oTTD2tvMF116MnGHGBl2FVmtXkRoa9anmvZW1WtN1zAyk+mwPbebeaZg+xB1Q6c8W1mWR2kKwFmbTpsFMc06myA7pEkNZLROHBd1yKoupstIggmpIDi0kzSpn3cBiTtXl2kLS4Wy1Pc0h0sDmyCQQzKQYKzjlu1LPTnmVSGTOUd6rmqSdic9/QP8AF+CqziFuemr7a3obPichRc4ULG634/jJZmFcp0mwTIOZGDjjqybH6alTcwjNNDVdMtR9Vke6yASMDidQ6I178PFFiLDJc4NljxMEiTAEwdp+Sp07TUZg172jVDiBwSi2VAS4PfeOZkye0qaTbpeRWn22aqC9t6m8OY9oDcnNLZLnZZnLNMtWl6L6bWQRdbdv4y7YSNWrLYuddaHPMveSSIl0u3bzkmggHNp+Rw7wFi/FLdrMrFypVEkiASZgCAJnIYwMckr6kQQ87DjG3IalTeQBmD/N+IQ7HWO/6rel2tlw2953zAUjYyL9WM71nh07EXzuy2gj6JydVoUrV7ROJcMTMH5a8pHYSmWeqwYux6MDVB271SDiP1nwSymk22X2mkTeDXT0faeLuEZtawbNqgp2wCq991mOQI6IyxA+XFZvYCgHt7E1Flbtr0mx7bvNsZMYtgZdjQo7NamCAWyBM4AnEtOE7A096q6ODCHX2ziI246uC6Ow6IovZeuHHMg6xIO5YusQmg7SebbrwdkBqJ1BQ6StRBY6+WEOkOAJOIdIgZ5BaB0LRE4vb82+Sgr6FoSLz3AbZGH5wWNzaso6TaA4ANJMyXAkS4kk3RA1/JZTWGMjCaaxa51wwMRO0Tr7gofSXge26O0rrIynEGceE9+xM7t+aR9qdqgbwPqoxVxl0Ow1gDjC1oWrPUuuDhm3pDXiCIVjRVsZRqtqOF4tMtwIAcCMZBxwkZa1RfaZwDRv3oo12g9Nl4bGktM6scfBPpHpugf/ANEoMvmreaXvmGNc8BoYxmcDHok5a1wtv0lzlatVENNQtOsjAEEYicjsCo2i0UzFymQNZc8lxPywjLVKrscySTIw6OvpbzIUxxmO7Psp1Q9D7X4KADKFMwAjHbt3KVtEDETlr3rpPRfYvITLyFnTo363JZzz/uAfZP8Akk/Y9+qoz+U/VWWWqh+8qj7dQ+JKsMtVPVaKg+YP9TFz3WeayH8ka+otPcPxUB5MWke5PHwXTMtLdVqf8xT/AMFYZaXarS35tYfAhOqcuPHJm0Zljt2BUL9A1xmw8fou7bXq6q9M9tP6PUwr2j46R+w4f3lTqnLzg6KqjNvFNOjqnw8fNelGtaPhou7S8f2lRvqVj/wUD9s/ixOq1w88OjXx7IP2goTYX53eK9DLHn2rMw/wvb+ICYbO0Z2R32X0yOLwnVOHn3oj/hKT0R/wlehCnT/8R/3D/S8pwp0MzZnj7Dj4FOl/XXnZoP2HuS0qRdhBMHGBMdq9B5uya6VQdtOt9FM2nYoHSc3t5xv9QV7X9dcdYbIwTenUcRBwGK27PXDGhrYjHPHErTFGwT/uhp284AeKedH2V+VoB/7GHjCxfJyy3Wqfdb3JlWsXCCAAtgaDpH2a2J/gKU8ngcn4f/Nv4FTRy4a16OeHOLcQT+HFUallePdPcV6J+zXXae0OHg5QO5NP1PYdxv4LpLWLjHnppuAxB7io3NleiO0BVy6B+3HiEp5PPjFjT2VG/ixXpOY88OBKScu1d9V5NuP/ABn5Ppn+0KmeTJGdN/ya0+D1dpy41sQiF1b+TEmLrx20z4hxVNnJ1we8PvtaCLjrs3hr14as1ZlDTAaYK0RVaRsK2xydBwD35e8E2pyZcAYcSYwEZ7ty3jlo05q6haHqur+7f/KUizto0FOBTAUsqOqQOTg9RBLKmliYPT21N6gBShymmlhtd3xHvKmZanj33/zFUpTg5TSytBukKg993eVM3SdX4zwWWHJwcpy1uNZml6vx8B9FM3TVTaD8liByeHppdxus068fD3H6qdvKJ+treK5wPTg9OTw6P9oCc2N7/JMdpimfaotPd9Fz99I16cp4b5ttmPtWdn8jPohtWx/uQOxsf0lYIel5xXlLMXQB1kPxt7H1B4OUg9H92tWb/wBj/wAZXN84l5xXlm4x0fQ921VR2vYf6mJwv6rWfmKZ8AFzXOI5xXmMcunHP6rSx3bTnweE8PtI9+ifsPH9xXK30oqHae9XmM8ur9JtQ92iftvHi0pPTbR71Jh7Kn1YFy4tLxk93eU9ttqD33d6sxOXQOtdQZUHj+GpTI7iQmnSTx7VF/yuHwcsQaRqfGeCcNJ1Nbh8wFrSctn1kP3VT+Uf5JViesn7R3JE5OXOgpZTEoWWtnygFJCcENnApU0FOBRdnBKCmyiVF2kDkByZKJQ2kBTg5RSgFNLtMHJbyhDkspo2mDkl5R3kgero2laUt5RXkShtLeReUV5LeRNpLyWVFeReVTaS8llRyllU2kDkXlHKW8qm0l5F5MlAKqn3kJl5CDNBSyqPp3V4+SPTurx8lzc+ovSlCoendXj5JfT+rx8kOovpZVD0/q8fJHp/V4+SHUaEoBWf6w6vHyR6w6vHyRe40ZSys31h1ePkl9Y9Xj5IdxpSgFZw0j1ePkj1j1ePkh3GlKAVm+serx8kesurx8kXuNIvhErM9Y9Xj5JfWXV4+SJ3GlKWVmeserx8kvrLq/e8lTuNIFAKzfWfV4+SPWfV4+SHcaaJWZ6z6v3vJL6z6v3vJDuNKUsrM9adX73kj1p1ePkrtOo05RKzPWnV+95JfWvV+95Js6jTvJQVletOr97ySjSvV+95JuHUakoWV62Pw8fJKruHUZiEIWHIIQhAIQhAIQhAIQhAIQhAIQhAIQhAIQhAIQhAIQhAIQhAIQhAIQhAIQhB/9k="
                    });
            });

            modelBuilder.Entity<Restaurant>().Property(e => e.Id).HasIdentityOptions(startValue: 3);

            #endregion


        }
    }
}
