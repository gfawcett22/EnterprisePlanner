using System;
using System.Collections.Generic;
using Orders.Entities;

namespace Orders.Repositories
{
    public interface IOrderRepository
    {
		IEnumerable<Order> GetOrders();
		Order GetOrder(int orderId);
		IEnumerable<Order> GetOrders(IEnumerable<int> orderIds);
		void AddOrder(Order order);
		void DeleteOrder(Order order);
		void UpdateOrder(Order order);
		bool OrderExists(int orderId);
		bool Save();
    }
}
