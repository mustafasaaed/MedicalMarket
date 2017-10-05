using MedicalMarket.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMarket.ViewComponents
{
    [ViewComponent]
    public class AllCategoriesViewComponent : ViewComponent
    {
        private ApplicationDbContext _context;

        public AllCategoriesViewComponent(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var navItems = _context.Categoreis.ToList();
            return View("AllCategoriesViewComponent", navItems);
        }
    }
}
