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
        [Display(Name = "الاسم")]
        [Required(ErrorMessage = "من فضلك ادخل اسم القسم")]
        public string Name { get; set; }

        [Display(Name = "التوقيت")]
        [Required]
        public DateTime CreateAt { get; set; }
        [Required]
        public DateTime DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Item> Items { get; set; }

    }
}
