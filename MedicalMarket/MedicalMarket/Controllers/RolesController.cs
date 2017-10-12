using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MedicalMarket.Data;
using MedicalMarket.Models;
using MedicalMarket.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace MedicalMarket.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
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
                    return RedirectToAction("Index", "Home");
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
    }
}