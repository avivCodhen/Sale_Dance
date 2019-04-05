using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sale_Dance.Data;
using Sale_Dance.Models.ViewModel.UserViewModel;

namespace Sale_Dance.Areas.Identity.Pages.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {

        private UserViewModel userViewModel;
        private readonly ApplicationDbContext db;

        public AdminController(ApplicationDbContext db)
        {
            this.db = db;
            userViewModel = new UserViewModel {
                Users = db.Users.ToList()
            };

        }

        public IActionResult Index()
        {
            return View(userViewModel.Users);
        }

        public IActionResult Details(int id)
        {
            userViewModel.UserBusiness = db.Businesses.Where(b => b.Id == id).ToList();
            return View(userViewModel);

        }

        
    }
}