using MedicalMarket.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMarket.ViewComponents
{

    [ViewComponent]
    public class AdminDashboardViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public AdminDashboardViewComponent(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isDone)
        {
            if (isDone)
            {
                var done = _context.Orders.Where(o => o.IsFinished == true).ToList();
                return View("AdminDashboardDoneViewComponent", done);

            }
            var processing = _context.Orders.Where(o => o.IsFinished == false).ToList();
            return View("AdminDashboardViewComponent", processing);
        }
    }
}
