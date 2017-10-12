using MedicalMarket.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMarket.ViewComponents
{
    [ViewComponentAttribute]
    public class NavViewComponent : ViewComponent
    {
        private ApplicationDbContext _context;

        public NavViewComponent(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var navItems = _context.Categoreis.Take(4).ToList();
            return View("NavViewComponent", navItems);
        }
    }
}
