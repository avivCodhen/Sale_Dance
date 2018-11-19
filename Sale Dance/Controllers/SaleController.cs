using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private string userId;

        [BindProperty]
        public SalesViewModel saleViewModel { get; set; }

        public SaleController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)

        {
            this.hostingEnvironment = hostingEnvironment;
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
            userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            saleViewModel = new SalesViewModel
            {
                Sale = new Sale()
            };

        }
        public IActionResult Index()
        {
            var list = db.Sales.Where(s=>s.OwnderId == userId).ToList();
            list.Reverse();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        //POST Create Action
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (!ModelState.IsValid)
            {
                return View(saleViewModel);
            }
            saleViewModel.Sale.OwnderId = userId;
            db.Sales.Add(saleViewModel.Sale);
            await db.SaveChangesAsync();

            var salesFromDb = db.Sales.Find(saleViewModel.Sale.id);
            var files = HttpContext.Request.Form.Files;


            //string webRootPath = hostingEnvironment.WebRootPath;


            //if (files.Count != 0)
            //{
            //    var upload = Path.Combine(webRootPath, SD.ImageFolder);
            //    var extension = Path.GetExtension(files[0].FileName);

            //    using (var fileStream = new FileStream(Path.Combine(upload, saleViewModel.Sale.id + extension), FileMode.Create))
            //    {
            //        files[0].CopyTo(fileStream);
            //    }

            //    salesFromDb.Image = @"\" + SD.ImageFolder + @"\" + saleViewModel.Sale.id + extension;

            //}
            //else
            //{
            //    var upload = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultProductImage);
            //    System.IO.File.Copy(upload, webRootPath + @"\" + SD.ImageFolder + @"\" + saleViewModel.Sale.id + "jpg");
            //    salesFromDb.Image = @"\" + SD.ImageFolder + @"\" + saleViewModel.Sale.id + "jpg";

            //}

            salesFromDb.Image = SaveImageHelper.CopyFilesToFolder(hostingEnvironment,files, salesFromDb.id);

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
        public IActionResult Edit(int id, Sale sale)
        {
          
            if (ModelState.IsValid)
            {
                var saleFromDb = db.Sales.Find(id);
                var files = HttpContext.Request.Form.Files;
                if(files.Count != 0)
                {

                    sale.Image = SaveImageHelper.ReplaceFiles(hostingEnvironment, files, saleFromDb.Image, id);
                    
                }
                saleFromDb.Name = sale.Name;
                saleFromDb.BeforePrice = sale.BeforePrice;
                saleFromDb.AfterPrice = sale.AfterPrice;
                db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }

            return View(sale);

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