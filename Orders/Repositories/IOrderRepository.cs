using System;
using System.Collections.Generic;
using System.Linq;
using Orders.Entities;
using OrdersDtoTypes.Helpers;

namespace Orders.Repositories
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetOrders(OrdersPagingParameters pagingParameters);
		Order GetOrder(int orderId);
        IQueryable<Order> GetOrders(IEnumerable<int> orderIds);
		void AddOrder(Order order);
		void DeleteOrder(Order order);
		void UpdateOrder(Order order);
		bool OrderExists(int orderId);
		bool Save();
    }
}
