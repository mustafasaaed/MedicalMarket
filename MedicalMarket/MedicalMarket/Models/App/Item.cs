using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMarket.Models.App
{
    public class Item
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="من فضلك ادخل اسم المنتج")]
        [Display(Name="اسم المنتج")]
        public string Name { get; set; }

        [Display(Name = "هل المنتج غير موجود ؟")]
        public bool IsOutOfStock { get; set; }
        [Display(Name = "السعر")]
        [Required(ErrorMessage =  "من فضلك ادخل السعر")]
        public decimal Price { get; set; }
        [Display(Name = "الكميه")]
        [Required(ErrorMessage = "من فضلك ادخل الكميه المتاحه")]
        public int Count { get; set; }
        public string CategoryId { get; set; }
        [Required]
        [Display(Name = "التوقيت")]
        public DateTime CreateAt { get; set; }
        [Required]
        public DateTime DeletedAt { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل وصف للمنتج")]
        [MaxLength(1000, ErrorMessage = "من فضلك ادخل وصف اقل من 1000 حرف.")]
        [MinLength(60, ErrorMessage = "من فضلك ادخل وصف اكبر من 60 حرف.")]
        public string Description { get; set; }
        public ICollection<Image> Images { get; set; }
        public Category Category { get; set; }
    }
}
