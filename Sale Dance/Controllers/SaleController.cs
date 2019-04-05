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
            
            var sale = new Sale()
            {
                SalePrice = vm.SalePrice,
                BeforePrice = vm.BeforePrice,
               
                Name = vm.Name,
                User = user,
                Description = vm.Description

        };
            if (formImage != null)
            {
                image = formImage.FormImageToResizedPng(400, 400);
                sale.Image = new Image()
                {
                    Bytes = image,
                };
            }
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
            var vm = new SaleViewModel()
            {
                Name = sale.Name,
                BeforePrice = sale.BeforePrice,
                Image = sale.Image.Bytes,
                SalePrice = sale.SalePrice,
                Description = sale.Description,
                SaleId = sale.Id
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SaleViewModel vm)
        {
          
            if (ModelState.IsValid)
            {
                var saleFromDb = db.Sales.Single(x=>x.Id == id);

                IFormFileCollection files = HttpContext.Request.Form.Files;

                IFormFile formImage = files.FirstOrDefault();
                if (formImage != null)
                {
                    var image = formImage.FormImageToResizedPng(400, 400);
                    if (saleFromDb.Image != null)
                    {
                        saleFromDb.Image.Bytes = image;

                    }
                    else
                    {
                        saleFromDb.Image = new Image()
                        {
                            Bytes = image,
                        };
                    }

                }
                saleFromDb.Name = vm.Name;
                saleFromDb.BeforePrice = vm.BeforePrice;
                saleFromDb.SalePrice = vm.SalePrice;
                saleFromDb.LastEdited = DateTime.Now;
                saleFromDb.Description = vm.Description;
               await db.SaveChangesAsync();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveImage(int id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var sale = db.Sales.Single(x => x.UserId == user.Id && x.Id == id);
            db.Images.Remove(sale.Image);
            sale.Image = null;
            db.SaveChanges();
            return RedirectToAction("Edit", new{id});
        }

    }
}   