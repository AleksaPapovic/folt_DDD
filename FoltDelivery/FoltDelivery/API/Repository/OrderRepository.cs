using FoltDelivery.API.DTO;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FoltDelivery.API.Repository
{
    public class OrderRepository : GenericEventRepository<OrderAggregate, OrderSnapshot>, IOrderRepository
    {
        IEventStore _eventStore;
        public OrderRepository(IEventStore eventStore) : base(eventStore) {
        _eventStore = eventStore;
        }

        public List<OrderAggregate> GetOrdersInCart(List<Guid> orderIds)
        {
            List<OrderAggregate> orderAggregates = new List<OrderAggregate>();
            //foreach kroz snapshot-e
            return null;

        }

        public async void GetSuggestedFromAll(string productId)
        {
           string query = @"fromAll()
                    .when({
                        $init: function() {
                            return {
                                suggested: new Map(),
                            };
                        },
                        $any: function(s, e) {
                            if(e.data.OrderItems['11223344-5566-7788-99aa-bbccddeeff15'] && e.data.EventType ==='OrderCreated'){
                                for (const [key, value] of  Object.entries(e.data.OrderItems)) {
                                    if (s.suggested[key] === undefined) { s.suggested[key] = 1; }
                                    else { s.suggested[key] = s.suggested[key] + 1; }
                                }
                             }
                         }
                       }).outputState();";


            string streamName = "$streams_all_suggestion_by_product_id-" + Guid.NewGuid();

            _eventStore.CreateProjectionStream(streamName,query);
            string result = await _eventStore.RunProjection(streamName);

            SuggestionAllDTO result2 = JsonConvert.DeserializeObject<SuggestionAllDTO>(result);
            Debug.WriteLine(result2.Suggested[new Guid("11223344-5566-7788-99aa-bbccddeeff15")]);
        }
    }
}
