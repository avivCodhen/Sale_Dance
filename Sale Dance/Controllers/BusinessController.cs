using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sale_Dance.Data;
using Sale_Dance.Models;
using Sale_Dance.Models.ViewModel;
using Sale_Dance.Utility;

namespace Sale_Dance.Controllers
{
    public class BusinessController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public BusinessController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var b = _db.Businesses.Single(x=>x.UserId == user.Id);
            var vm = new BusinessViewModel()
            {
                About = b.About,
                Address = b.Address,
                BusinessEmailContact = b.BusinessEmailContact,
                BusinessPhoneContact = b.BusinessPhoneContact,
                Friday = b.Friday,
                Image = b.Image?.Bytes,
                Name = b.Name,
                Saturday = b.Saturday,
                Site = b.Site,
                WeekDays = b.WeekDays,
                Id = b.Id
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BusinessViewModel vm)
        {

            var user =await _userManager.GetUserAsync(User);
            var bFromDb = _db.Businesses.Single(x => x.UserId == user.Id);

            if (ModelState.IsValid)
            {
                bFromDb.Name = vm.Name;
                bFromDb.Address = vm.Address;
                bFromDb.About = vm.About;
                bFromDb.BusinessPhoneContact = vm.BusinessPhoneContact;
                bFromDb.BusinessEmailContact = vm.BusinessEmailContact;
                IFormFileCollection files = HttpContext.Request.Form.Files;

                IFormFile formImage = files.FirstOrDefault();
                if (formImage != null)
                {
                    var image = formImage.FormImageToResizedPng(400, 250);
                    if (bFromDb.Image != null)
                    {
                        bFromDb.Image.Bytes = image;
                    }
                    else
                    {
                        bFromDb.Image = new Image()
                        {
                            Bytes = image,
                        };
                    }
                    
                }
                bFromDb.Saturday = vm.Saturday;
                bFromDb.Friday = vm.Friday;
                bFromDb.WeekDays = vm.WeekDays;
                if (vm.Site != null && !vm.Site.ToLower().Contains("http://"))
                {
                    vm.Site = $"http://{vm.Site}";
                }
                bFromDb.Site = vm.Site;
                await _db.SaveChangesAsync();
                TempData["Success"] = "פרטי העסק נשמרו בהצלחה!";
            }
            else
            {
                TempData["Error"] = "התרחשה שגיאת, הפרטים לא נשמרו.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveImage()
        {
            var user = await _userManager.GetUserAsync(User);
            var business = _db.Businesses.Single(x => x.UserId == user.Id);
            _db.Images.Remove(business.Image);
            business.Image = null;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}