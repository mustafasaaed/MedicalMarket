using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMarket.Models.App
{
    public class Order
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل الاسم")]
        [DisplayName("الاسم")]
        [StringLength(160)]
        public string Name { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل الايميل")]
        [DisplayName("الايميل")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
                                    ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل العنوان")]
        [DisplayName("العنوان")]
        [StringLength(70)]
        public string Address { get; set; }
        [DisplayName("رقم الهاتف")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "من فضلك ادخل الهاتف")]
        [StringLength(24)]
        public string Phone { get; set; }
        [ScaffoldColumn(false)]
        [DisplayName("الإجمالى")]
        public decimal Total { get; set; }
        [ScaffoldColumn(false)]
        public DateTime OrderDate { get; set; }
        [DisplayName("تم")]
        public bool IsFinished { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }

    }
}
