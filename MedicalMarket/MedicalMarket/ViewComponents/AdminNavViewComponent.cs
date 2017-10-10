using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMarket.ViewComponents
{
    [ViewComponentAttribute]
    public class AdminNavViewComponent : ViewComponent
    {
        public AdminNavViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("AdminNavViewComponent");
        }
    }
}
