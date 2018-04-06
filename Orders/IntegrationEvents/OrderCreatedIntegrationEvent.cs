using EnterprisePlanner.Messaging.Models;
using Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.IntegrationEvents
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public Order Order { get; set; }

        public OrderCreatedIntegrationEvent(Order order) => Order = order;
    }
}
