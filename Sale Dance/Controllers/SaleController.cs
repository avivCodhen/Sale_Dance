using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sale_Dance.Data;
using Sale_Dance.Models;
using Sale_Dance.Models.ViewModel;
using Sale_Dance.Utility;

namespace Sale_Dance.Controllers
{
    [Authorize]
    public class SaleController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SaleController(ApplicationDbContext db, SignInManager<ApplicationUser> signInManager)

        {
            this.db = db;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var list = db.Sales.Where(s=>s.UserId == user.Id).ToList();
            list.Reverse();
            
            return View(list);
        }

        public IActionResult Create()
        {
            return View(new SaleViewModel());
        }

        //POST Create Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleViewModel vm)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
                return View();
            }
            IFormFileCollection files = HttpContext.Request.Form.Files;

            IFormFile formImage = files.FirstOrDefault();
            byte[] image = new byte[0];
            if (formImage != null)
            {
                image = formImage.FormImageToResizedPng(250, 250);
            }
            var sale = new Sale()
            {
                AfterPrice = vm.AfterPrice,
                BeforePrice = vm.BeforePrice,
                Image = image,
                Name = vm.Name,
                User = user

        };
            db.Sales.Add(sale);
            await db.SaveChangesAsync();

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)

        {

            if (id == null)
            {
                return NotFound();
            }

            var sale = db.Sales.Find(id);
            return View(sale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SaleViewModel vm)
        {
          
            if (ModelState.IsValid)
            {
                var saleFromDb = db.Sales.Find(id);

                IFormFileCollection files = HttpContext.Request.Form.Files;

                IFormFile formImage = files.FirstOrDefault();
                if (formImage != null)
                {

                    vm.Image = formImage.FormImageToResizedPng(250, 250);

                }
                saleFromDb.Name = vm.Name;
                saleFromDb.BeforePrice = vm.BeforePrice;
                saleFromDb.AfterPrice = vm.AfterPrice;
                db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }

            return View(vm);

        }

        public IActionResult Details(int? id)

        {

            if(id == null)
            {
                return NotFound();
            
            }
            var sale = db.Sales.Find(id);
            return View(sale);
        }

        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var sale = db.Sales.Find(id);
            if(sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        { 
                var sale = db.Sales.Find(id);
            if (ModelState.IsValid)
            {
                db.Sales.Remove(sale);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(sale);
        }
    }
}