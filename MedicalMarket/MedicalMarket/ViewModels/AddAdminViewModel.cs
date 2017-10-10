using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMarket.ViewModels
{
    public class AddAdminViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
