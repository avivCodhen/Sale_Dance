using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sale_Dance.Data;
using Sale_Dance.Models;
using Sale_Dance.Utility;

namespace Sale_Dance.Areas.Identity.Pages.Account
{
    public class AddNewAdminModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _db;


        public AddNewAdminModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            this.roleManager = roleManager;
            _db = db;
        }

        public async Task<IActionResult> OnGet()
        {

            if (!await roleManager.RoleExistsAsync(Constants.SuperAdmin))
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.SuperAdmin));
            }

            var userAdmin = new ApplicationUser
            {
                UserName = "kaazz931@gmail.com",
                Email = "kaazz931@gmail.com",
                PhoneNumber = "124243424",
                Name = "Aviv Cohen",
            };

            var resultUser = await _userManager.CreateAsync(userAdmin, "Aviv123*");
            await _userManager.AddToRoleAsync(userAdmin, Constants.SuperAdmin);
            _db.Businesses.Add(new Business() {User = userAdmin});
            await _db.SaveChangesAsync();
            return Page();
        }
    }
}