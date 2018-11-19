using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sale_Dance.Data;
using Sale_Dance.Models;
using Sale_Dance.Models.ViewModel;

namespace Sale_Dance.Controllers
{
    public class BusinessController : Controller
    {
        private readonly ApplicationDbContext db;
        private BusinessViewModel businessViewModel;
        private IHttpContextAccessor httpContextAccessor;
        public BusinessController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.db = db;
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var business = db.Businesses.Where(b => b.BusinessOwnerId == userId).SingleOrDefault();
            if(business == null)
            {
                business = new Business {
                    BusinessOwnerId = userId
                };
                db.Add(business);
                db.SaveChanges();
            }
            businessViewModel = new BusinessViewModel {
                Business = business 
            };
        }
        public IActionResult Index()
        {
            

            return View(businessViewModel.Business);
        }

        [HttpPost]
        public IActionResult Update(Business business)
        {
            
           
            
            if (ModelState.IsValid)
            {
                businessViewModel.Business.Name = business.Name;
                businessViewModel.Business.Address = business.Address;
                businessViewModel.Business.About = business.Address;
                businessViewModel.Business.BusinessPhoneContact = business.BusinessPhoneContact;
                businessViewModel.Business.BusinessEmailContact = business.BusinessEmailContact;
                businessViewModel.Business.Image = business.Image;
                businessViewModel.Business.Saturday = business.Saturday;
                businessViewModel.Business.Friday = business.Friday;
                businessViewModel.Business.WeekDays = business.WeekDays;
                businessViewModel.Business.Site = business.Site;






                db.SaveChanges();
                TempData["Success"] = "פרטי העסק נשמרו בהצלחה!";
            }
            else
            {
                TempData["Error"] = "התרחשה שגיאת, הפרטים לא נשמרו.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}