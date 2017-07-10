using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orders.Contexts;
using Orders.Entities;
using OrdersDtoTypes.Helpers;

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

        public IQueryable<Order> GetOrders(OrdersPagingParameters pagingParameters)
        {
            return _context.Orders
                // .Where(o => o.Id.ToString().Contains(pagingParameters.Id.ToString()) 
                //         || o.CustomerId.ToString().Contains(pagingParameters.CustomerId.ToString())
                //         || o.DatePlaced == pagingParameters.DatePlaced)
                .Skip(pagingParameters.PageSize * (pagingParameters.PageNumber - 1))
                .Take(pagingParameters.PageSize);
        }

        public IQueryable<Order> GetOrders(IEnumerable<int> orderIds)
        {
            return _context.Orders.Where(o => orderIds.Contains(o.Id));
        }

        public int GetOrdersCount() 
        {
            return _context.Orders.Count();
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
