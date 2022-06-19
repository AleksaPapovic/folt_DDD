using EventStore.ClientAPI;
using FoltDelivery.Domain.Events;
using FoltDelivery.Infrastructure;
using FoltDelivery.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoltDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IUserService _userService;

        private IEventStore _eventStore;

        public OrderController(IUserService userService, IEventStore eventStore)
        {
            _userService = userService;
            _eventStore = eventStore;
        }

        public static int i = 0;

        [HttpGet]
        public async Task<IEnumerable<DomainEvent>> Get()
        {
          
            //return new string[] { "value1", "value2" };
            List<OrderCreated> events = new List<OrderCreated>();
            i = i + 1;
            OrderCreated evt = new OrderCreated(new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"), new Guid("11223344-5566-7788-99AA-BBCCDDEEFF01"));
            events.Add(evt);
            //var data = Enumerable.Range(0, 100).Select(_ => CreateEvent());
            _eventStore.AppendEventsToStream("test-0" + i,events, ExpectedVersion.Any);
            
            return _eventStore.GetStream("test-0" + i,0,0);
        }
    }
}