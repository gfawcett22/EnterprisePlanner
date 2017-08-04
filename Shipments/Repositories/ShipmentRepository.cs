using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shipments.Contexts;
using Shipments.Entities;
using ShipmentsDtoTypes.Helpers;

namespace Shipments.Repositories
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly ShipmentsDbContext _context;

        public ShipmentRepository(ShipmentsDbContext shipmentsContext)
        {
            _context = shipmentsContext;
        }

        public void AddShipment(Shipment shipment)
        {
            _context.Shipments.Add(shipment);
        }

        public void DeleteShipment(Shipment shipment)
        {
            _context.Remove(shipment);
        }

        public Shipment GetShipment(int shipmentId)
        {
            return _context.Shipments.FirstOrDefault(o => o.Id == shipmentId);
        }

        public IQueryable<Shipment> GetShipments(ShipmentsPagingParameters pagingParameters)
        {
            return _context.Shipments
                 .Where(o => o.Id.ToString().Contains(pagingParameters.Id ?? "") 
                         && o.CustomerId.ToString().Contains(pagingParameters.CustomerId ?? "")
                         && o.OrderId.ToString().Contains(pagingParameters.OrderId ?? "")
                         && o.Status.Contains(pagingParameters.Status ?? ""))
                .Skip(pagingParameters.PageSize * (pagingParameters.PageNumber - 1))
                .Take(pagingParameters.PageSize);
        }

        public IQueryable<Shipment> GetShipments(IEnumerable<int> shipmentIds)
        {
            return _context.Shipments.Where(o => shipmentIds.Contains(o.Id));
        }

        public int GetShipmentsCount() 
        {
            return _context.Shipments.Count();
        }

        public bool ShipmentExists(int shipmentId)
        {
            return _context.Shipments.Any(o => o.Id == shipmentId);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateShipment(Shipment shipment)
        {
            _context.Update(shipment);
            _context.Entry(shipment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
