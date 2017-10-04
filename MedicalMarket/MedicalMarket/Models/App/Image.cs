using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMarket.Models.App
{
    public class Image
    {
        public string Id { set; get; }
        public byte[] ImageData { set; get; }
        public string ItemId { get; set; }
        public Item Item { get; set; }
    }
}
