using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMarket.Models.App
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsOutOfStock { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }

        public ICollection<Image> Images { get; set; }
        public Category Category { get; set; }
    }
}
