using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicalMarket.Models;
using MedicalMarket.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicalMarket.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            var model = _context.Items.Include(i => i.Images).ToList();
            return View(model);
        }

        public IActionResult Category(string id)
        {
            var model = _context.Items.Where(i => i.Category.Id == id)
                                      .Include(i => i.Images)
                                      .ToList();
            return View("Index", model);
        }

        public IActionResult Preview()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
