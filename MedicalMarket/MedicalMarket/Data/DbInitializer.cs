using MedicalMarket.Interfaces;
using MedicalMarket.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMarket.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext context,
                             UserManager<ApplicationUser> userManager,
                             RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async void Initialize()
        {
            _context.Database.EnsureCreated();

            if (_context.Roles.Any(r => r.Name == "Admin")) return;

            await _roleManager.CreateAsync(new IdentityRole("Admin"));

            var user = "admin@admin.com";
            var password = "As#123456";

            await _userManager.CreateAsync(new ApplicationUser { UserName = user, Email = user, EmailConfirmed = true }, password);
            await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(user), "Admin");
        }
    }
}
