using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoltDelivery.Migrations
{
    public partial class FirstM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StreetName = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    PostalCode = table.Column<int>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loyalty",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TypeName = table.Column<int>(nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    PointsRequired = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loyalty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantMenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    CustomerOrdersIds = table.Column<List<Guid>>(nullable: true),
                    CartId = table.Column<int>(nullable: false),
                    RestaurantId = table.Column<int>(nullable: false),
                    DeliveryOrdersIds = table.Column<List<Guid>>(nullable: true),
                    Points = table.Column<double>(nullable: false),
                    TypeId = table.Column<Guid>(nullable: true),
                    LogicalDeleted = table.Column<bool>(nullable: false),
                    Blocked = table.Column<bool>(nullable: false),
                    Confirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Loyalty_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Loyalty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    RestaurantId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    LogicalDeleted = table.Column<bool>(nullable: false),
                    RestaurantMenuId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_RestaurantMenu_RestaurantMenuId",
                        column: x => x.RestaurantMenuId,
                        principalTable: "RestaurantMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    MenuId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: true),
                    LogoId = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurants_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Restaurants_RestaurantMenu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "RestaurantMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "HouseNumber", "PostalCode", "State", "StreetName" },
                values: new object[,]
                {
                    { new Guid("11223344-5566-7788-99aa-bbccddeeff06"), "Novi Sad", 1, 21000, "Srbija", "" },
                    { new Guid("11223344-5566-7788-99aa-bbccddeeff08"), "Novi Sad", 1, 21000, "Srbija", "" }
                });

            migrationBuilder.InsertData(
                table: "RestaurantMenu",
                column: "Id",
                values: new object[]
                {
                    new Guid("11223344-5566-7788-99aa-bbccddeeff15"),
                    new Guid("11223344-5566-7788-99aa-bbccddeeff16")
                });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "AddressId", "Latitude", "Longitude" },
                values: new object[,]
                {
                    { new Guid("11223344-5566-7788-99aa-bbccddeeff05"), new Guid("11223344-5566-7788-99aa-bbccddeeff06"), 45.247999999999998, 19.842500000000001 },
                    { new Guid("11223344-5566-7788-99aa-bbccddeeff09"), new Guid("11223344-5566-7788-99aa-bbccddeeff08"), 45.258899999999997, 19.835699999999999 }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Image", "LogicalDeleted", "Name", "Price", "Quantity", "RestaurantId", "RestaurantMenuId", "Type" },
                values: new object[,]
                {
                    { new Guid("11223344-5566-7788-99aa-bbccddeeff15"), "Pica sa tradicijom", "", false, "Vojvodjanska", 1300.0, 1, new Guid("11223344-5566-7788-99aa-bbccddeeff00"), new Guid("11223344-5566-7788-99aa-bbccddeeff15"), 1 },
                    { new Guid("11223344-5566-7788-99aa-bbccddeeff16"), "Pica sa tradicijom", "", false, "Vojvodjanska", 1300.0, 1, new Guid("11223344-5566-7788-99aa-bbccddeeff00"), new Guid("11223344-5566-7788-99aa-bbccddeeff15"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Deleted", "LocationId", "LogoId", "MenuId", "Name", "Status", "Type" },
                values: new object[,]
                {
                    { new Guid("11223344-5566-7788-99aa-bbccddeeff00"), false, new Guid("11223344-5566-7788-99aa-bbccddeeff05"), 1, new Guid("11223344-5566-7788-99aa-bbccddeeff15"), "Pizzeria Ciao", 1, "Restaurant" },
                    { new Guid("11223344-5566-7788-99aa-bbccddeeff01"), false, new Guid("11223344-5566-7788-99aa-bbccddeeff09"), 2, new Guid("11223344-5566-7788-99aa-bbccddeeff16"), "Boom boom pancakes", 1, "FastFood" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_AddressId",
                table: "Location",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_RestaurantMenuId",
                table: "Product",
                column: "RestaurantMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_LocationId",
                table: "Restaurants",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_MenuId",
                table: "Restaurants",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TypeId",
                table: "Users",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "RestaurantMenu");

            migrationBuilder.DropTable(
                name: "Loyalty");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
