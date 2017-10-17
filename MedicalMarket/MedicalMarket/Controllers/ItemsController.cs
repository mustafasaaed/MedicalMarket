using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalMarket.Data;
using MedicalMarket.Models.App;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace MedicalMarket.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.Include(i => i.Images)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [Authorize(Roles = "Admin")]
        // GET: Items/Create
        public IActionResult Create()
        {
            var categoreis = _context.Categoreis.ToList();
            ViewBag.Categoreis = categoreis;
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsOutOfStock,Price,Count,CategoryId,Description,CreateAt,DeletedAt,IsDeleted")] Item item, IEnumerable<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);

                if (images.Count() != 3)
                {
                    var categoreis = _context.Categoreis.ToList();
                    ViewBag.Categoreis = categoreis;
                    ModelState.TryAddModelError("Images", "من فضلك اختار 3 صور");
                    return View(item);
                }

                if (images != null)
                {
                    var imageList = new List<Image>();
                    foreach (var image in images)
                    {
                        var data = ConvertToBytes(image);
                        var img = new Image { ItemId = item.Id };
                        img.ImageData = data;
                        imageList.Add(img);
                    }
                    item.Images = imageList;
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.SingleOrDefaultAsync(m => m.Id == id);
            var categoreis = _context.Categoreis.ToList();
            ViewBag.Categoreis = categoreis;
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,CategoryId,IsOutOfStock,Price,Count,CreateAt,DeletedAt,IsDeleted,Description")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }


        // POST: Items/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var item = await _context.Items
                    .Include(o => o.OrderDetail)
                    .Include(i => i.Images)
                    .Include(c => c.Cart)
                    .SingleOrDefaultAsync(m => m.Id == id);

                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        private bool ItemExists(string id)
        {
            return _context.Items.Any(e => e.Id == id);
        }

        private byte[] ConvertToBytes(IFormFile image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.OpenReadStream().CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
