namespace Shipments.Entities
{
    public class Shipment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public string Status { get; set; }
    }
}