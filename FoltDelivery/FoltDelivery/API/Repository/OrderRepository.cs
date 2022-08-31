using FoltDelivery.API.DTO;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FoltDelivery.API.Repository
{
    public class OrderRepository : GenericEventRepository<OrderAggregate, OrderSnapshot>, IOrderRepository
    {
        IEventStore _eventStore;
        public OrderRepository(IEventStore eventStore) : base(eventStore) {
            _eventStore = eventStore;
        }

        public async Task<List<Guid>> GetSuggestedFromAllOrders(OrderInCartDTO order)
        {
            Dictionary<Guid, int> allSuggestedProductsIds = new Dictionary<Guid, int>();
            foreach (Guid productId in order.OrderItemsIds)
            {
                string query = @"fromAll()
                    .when({
                        $init: function() {
                            return {
                                suggested: new Map(),
                            };
                        },
                        $any: function(s, e) {
                            if(e.data.OrderItems['" + productId + @"'] && e.data.CustomerId != '" + order.OwnerId + @"'  && e.data.EventType ==='OrderCreated'){
                                for (const [key, value] of  Object.entries(e.data.OrderItems)) {
                                    if (s.suggested[key] === undefined) { s.suggested[key] = 1; }
                                    else { s.suggested[key] = s.suggested[key] + 1; }
                                }
                             }
                         }
                       }).outputState();";

                string result = await RunProjection(query);

                allSuggestedProductsIds = UpdateSuggestedIds(allSuggestedProductsIds, result);

            }
            return GetTop2SuggestedIds(order.OrderItemsIds, allSuggestedProductsIds);
        }

        public async Task<List<Guid>> GetSuggestedFromPersonalOrders(OrderInCartDTO order)
        {

            Dictionary<Guid, int> allSuggestedProductsIds = new Dictionary<Guid, int>();
            foreach (Guid productId in order.OrderItemsIds)
            {
                string query = @"fromAll()
                    .when({
                        $init: function() {
                            return {
                                suggested: new Map(),
                            };
                        },
                        $any: function(s, e) {
                            if(e.data.OrderItems['" + productId + @"'] && e.data.CustomerId != '" + order.OwnerId + @"'  && e.data.EventType ==='OrderCreated'){
                                for (const [key, value] of  Object.entries(e.data.OrderItems)) {
                                    if (s.suggested[key] === undefined) { s.suggested[key] = 1; }
                                    else { s.suggested[key] = s.suggested[key] + 1; }
                                }
                             }
                         }
                       }).outputState();";

                string result = await RunProjection(query);

                allSuggestedProductsIds = UpdateSuggestedIds(allSuggestedProductsIds, result);

            }
            return GetTop2SuggestedIds(order.OrderItemsIds, allSuggestedProductsIds);
        }
    
        private async Task<string> RunProjection(string query)
        {
            string streamName = "$streams_all_suggestion_by_product_id-" + Guid.NewGuid();

            _eventStore.CreateProjectionStream(streamName, query); await Task.Delay(200);
            string result = await _eventStore.RunProjection(streamName);
            return result;
        }

        private static Dictionary<Guid, int> UpdateSuggestedIds(Dictionary<Guid, int> allSuggestedProductsIds, string result)
        {
            SuggestionAllDTO result2 = JsonConvert.DeserializeObject<SuggestionAllDTO>(result);

            allSuggestedProductsIds = allSuggestedProductsIds.Concat(result2.Suggested)
               .GroupBy(x => x.Key)
               .ToDictionary(x => x.Key, x => x.Sum(y => y.Value));
            return allSuggestedProductsIds;
        }

        private static List<Guid> GetTop2SuggestedIds(List<Guid> productIds, Dictionary<Guid, int> allSuggestedProductsIds)
        {
            List<KeyValuePair<Guid, int>> myList = allSuggestedProductsIds.ToList();
            Debug.WriteLine(myList[0].ToString());
            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            List<Guid> allKeys = (from kvp in myList select kvp.Key).Distinct().ToList();
            Debug.WriteLine(myList[0].ToString());
            return allKeys.Where(p => productIds.All(p2 => p2 != p)).Take(4).ToList();
        }


    }
}
