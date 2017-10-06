using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMarket.Models.App
{
    public class Category
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "اسم الصنف مطلوب")]
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Item> Items { get; set; }

    }
}
