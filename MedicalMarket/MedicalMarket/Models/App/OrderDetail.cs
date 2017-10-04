using System.ComponentModel.DataAnnotations;

namespace MedicalMarket.Models.App
{
    public class OrderDetail
    {
        [Key]
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string ItemId { get; set; }
        public Order Order { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}