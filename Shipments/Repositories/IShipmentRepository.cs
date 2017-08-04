using System.Collections.Generic;
using System.Linq;
using Shipments.Entities;
using ShipmentsDtoTypes.Helpers;

namespace Shipments.Repositories
{
    public interface IShipmentRepository
    {
        IQueryable<Shipment> GetShipments(ShipmentsPagingParameters pagingParameters);
        Shipment GetShipment(int shipmentId);
        IQueryable<Shipment> GetShipments(IEnumerable<int> shipmentIds);
        int GetShipmentsCount();
        void AddShipment(Shipment shipment);
        void DeleteShipment(Shipment shipment);
        void UpdateShipment(Shipment shipment);
        bool ShipmentExists(int shipmentId);
        bool Save();
    }
}