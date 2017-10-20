using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicalMarket.Data;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MedicalMarket.Models;
using MedicalMarket.ViewModels;

namespace MedicalMarket.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public AdminController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        public IActionResult Index(int page = 1)
        {
            var adminRoleId = _context.Roles
                .Where(r => r.Name == "Admin")
                .Select(r => r.Id)
                .FirstOrDefault();

            var userIds = _context.UserRoles
                .Where(ur => ur.RoleId == adminRoleId)
                .Select(u => u.UserId)
                .Distinct()
                .ToList();

            var users = _context.Users.Where(a => userIds.Any(c => c == a.Id))
                .ToPagedList(page, 10);

            return View(users);
        }

        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(AddAdminViewModel vm)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!IsAdmin())
                {
                    return RedirectToAction("Index", "Home");
                }

                if (vm.Email == null)
                {
                    ModelState.TryAddModelError("Email", "من فضلك ادخل الايميل");
                    return View(vm);
                }

                var user = await _userManager.FindByEmailAsync(vm.Email);
                if (user != null)
                {
                    var isExist = await _roleManager.RoleExistsAsync("Admin");
                    if (!isExist)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    }

                    await _userManager.AddToRoleAsync(user, "Admin");
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.TryAddModelError("Email", "هذا الايميل غير موجود.");
                    return View(vm);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        private bool IsAdmin()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                    return true;
                else return false;
            }
            return false;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAdmin(string email)
        {
            if (email != null && !string.IsNullOrWhiteSpace(email))
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    return StatusCode(404, new { msg = "user is not found !" });
                }

                await _userManager.RemoveFromRoleAsync(user, "Admin");
                return StatusCode(200);
            }
            return StatusCode(404);
        }
    }
}