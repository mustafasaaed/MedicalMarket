using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMarket.Models.App
{
    public class Order
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }

    }
}
