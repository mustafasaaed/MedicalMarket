using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMarket.ViewModels
{
    public class AddAdminViewModel
    {
        [Required(ErrorMessage = "من فضلك ادخل ايميل صحيح.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
