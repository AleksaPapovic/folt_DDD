using EventStore.ClientAPI;
using FluentAssertions;
using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EventData = EventStore.ClientAPI.EventData;


namespace FoltDelivery.Test
{
    public class EventStoreTest
    {
        // EVENTS


        public record ShoppingCartOpened(
            Guid ShoppingCartId,
            Guid ClientId
        )
        {
            public Guid ShoppingCartId { get; } = ShoppingCartId;
            public Guid ClientId { get; } = ClientId;
        }

        public record ProductItemAddedToShoppingCart(
            Guid ShoppingCartId,
            PricedProductItem ProductItem
        )
        {
            public Guid ShoppingCartId { get; } = ShoppingCartId;
            public PricedProductItem ProductItem { get; } = ProductItem;
        }

        public record ProductItemRemovedFromShoppingCart(
            Guid ShoppingCartId,
            PricedProductItem ProductItem
        )
        {
            public Guid ShoppingCartId { get; } = ShoppingCartId;
            public PricedProductItem ProductItem { get; } = ProductItem;
        }

        public record ShoppingCartConfirmed(
            Guid ShoppingCartId,
            DateTime ConfirmedAt
        )
        {
            public Guid ShoppingCartId { get; } = ShoppingCartId;
            public DateTime ConfirmedAt { get; } = ConfirmedAt;
        }

        public record ShoppingCartCanceled(
            Guid ShoppingCartId,
            DateTime CanceledAt
        )
        {
            public Guid ShoppingCartId { get; } = ShoppingCartId;
            public DateTime CanceledAt { get; } = CanceledAt;
        }

        // VALUE OBJECTS
        public record PricedProductItem(
            ProductItem ProductItem,
            decimal UnitPrice
        )
        {
            public Guid ProductId => ProductItem.ProductId;
            public int Quantity => ProductItem.Quantity;

            public decimal TotalPrice => Quantity * UnitPrice;
            public ProductItem ProductItem { get; } = ProductItem;
            public decimal UnitPrice { get; } = UnitPrice;
        }

        public record ProductItem(
            Guid ProductId,
            int Quantity
        )
        {
            public Guid ProductId { get; } = ProductId;
            public int Quantity { get; } = Quantity;
        }


        [Fact]
        public async Task GettingState_ForSequenceOfEvents_ShouldSucceed()
        {
            //var shoppingCartId = Guid.NewGuid();
            //var clientId = Guid.NewGuid();
            //var shoesId = Guid.NewGuid();
            //var tShirtId = Guid.NewGuid();
            //var twoPairsOfShoes = new PricedProductItem(new ProductItem(shoesId, 2), 100);
            //var pairOfShoes = new PricedProductItem(new ProductItem(shoesId, 1), 100);
            //var tShirt = new PricedProductItem(new ProductItem(tShirtId, 1), 50);

            //var events = new object[]
            //{
            //    new ShoppingCartOpened(shoppingCartId, clientId),
            //    new ProductItemAddedToShoppingCart(shoppingCartId, twoPairsOfShoes),
            //    new ProductItemAddedToShoppingCart(shoppingCartId, tShirt),
            //    new ProductItemRemovedFromShoppingCart(shoppingCartId, pairOfShoes),
            //    new ShoppingCartConfirmed(shoppingCartId, DateTime.UtcNow),
            //    new ShoppingCartCanceled(shoppingCartId, DateTime.UtcNow)
            //};
            var events = new object[] { };

            var connection = EventStoreConnection.Create(
                    new Uri("tcp://admin:changeit@localhost:1113")
                );
            await connection.ConnectAsync();

            var streamName = "shopping_cart-1";

            var appendedEvents = 0l;

            //const string streamName = "newstream";
            const string eventType = "event-type";
            const string data = "{ \"a\":\"2\"}";
            const string metadata = "{}";

            var eventPayload = new EventData(
                eventId: Guid.NewGuid(),
                type: eventType,
                isJson: true,
                data: Encoding.UTF8.GetBytes(data),
                metadata: Encoding.UTF8.GetBytes(metadata)
            );
            var result = await connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventPayload);

            var exception = await Record.ExceptionAsync(async () =>
            {
                var result = await connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventPayload);
                appendedEvents = result.NextExpectedVersion;
            });

            exception.Should().BeNull();
            appendedEvents.Should().Be((long)events.Length - 1);

        }
    }
}
