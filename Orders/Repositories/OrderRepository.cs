using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orders.Contexts;
using Orders.Entities;

namespace Orders.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersDbContext _context;

        public OrderRepository(OrdersDbContext ordersContext)
        {
            _context = ordersContext;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public void DeleteOrder(Order order)
        {
            _context.Remove(order);
        }

        public Order GetOrder(int orderId)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == orderId);
        }

        public IQueryable<Order> GetOrders()
        {
            return _context.Orders;
        }

        public IQueryable<Order> GetOrders(IEnumerable<int> orderIds)
        {
            return _context.Orders.Where(o => orderIds.Contains(o.Id));
        }

        public bool OrderExists(int orderId)
        {
            return _context.Orders.Any(o => o.Id == orderId);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateOrder(Order order)
        {
			_context.Update(order);
			_context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
