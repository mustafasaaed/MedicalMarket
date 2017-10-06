using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMarket.Models.App
{
    public class Cart
    {
        [Key]
        public string RecordId { get; set; }
        public string CartId { get; set; }
        public string ItemId { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
        public Item Item { get; set; }
    }
}
