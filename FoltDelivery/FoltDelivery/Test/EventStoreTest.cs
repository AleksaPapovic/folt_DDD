using FoltDelivery.API.Service;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace FoltDelivery.Test
{


    public class EventStoreTest
    {

        private OrderService _unitTesting = null;

        public EventStoreTest()
        {
            if (_unitTesting == null)
            {
                _unitTesting = new OrderService();
            }
        }

        [Fact]
    public void Add()
    {
        //arrange
     
        double expected = 8;

            //act
            var actual = 3;

        //Assert
        Assert.Equal(expected, actual, 0);
    }

        [Fact]
        public void GettingState_ForSequenceOfEvents_ShouldSucceed()
        {

        //    var client = new HttpClient(); // no HttpServer

        //    var request = new HttpRequestMessage
        //    {
        //        RequestUri = new Uri("http://localhost:50892/api/product/hello"),
        //        Method = HttpMethod.Get
        //    };

        //    request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        //    using (var response = client.SendAsync(request).Result)
        //    {
        //        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        //    }
        }
    }
}
