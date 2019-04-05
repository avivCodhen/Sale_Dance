using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sale_Dance.Data;
using Sale_Dance.Models;
using Sale_Dance.Models.ViewModel;
using Sale_Dance.Services;
using Sale_Dance.Soldiers;
using Sale_Dance.Utility;
using Sale_Dance.Utility.Attributes;

namespace Sale_Dance.Controllers
{
    [Authorize()]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this._db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var list = _db.Posts.Where(p => p.UserId == user.Id).ToList();
            list.Reverse();
            return View(new HomeIndexViewModel()
            {
                Posts = list,
                ShowHelpfulSaleCard = !user.DontShowHelpfulSaleAlert

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id, PostAction postAction)
        {
            if (postAction == PostAction.Publish)
            {
                await PublishPost(id);
            }
            else if (postAction == PostAction.Unpublish)
            {
                await UnPublishPost(id);
            }
            else if (postAction == PostAction.Bump)
            {
                BumpPost(id);
            }
            var user = await _userManager.GetUserAsync(User);
            var list = _db.Posts.Where(p => p.UserId == user.Id).ToList();
            list.Reverse();
            return View(new HomeIndexViewModel()
            {
                Posts = list,
                ShowHelpfulSaleCard = !user.DontShowHelpfulSaleAlert

            });
        }


        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);

            List<Sale> saleList = _db.Sales.Where(s => s.UserId == user.Id).ToList();
            var vm = new PostViewModel()
            {
                SelectedListItem = saleList.Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList(),
            };
            return View(vm);
        }

        //POST Create Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel vm)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var post = new Post()
                {
                    User = user,
                    Name = vm.Name,
                    Body = vm.Body,
                };

                _db.Add(post);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            var post = _db.Posts.SingleOrDefault(x => x.Id == id);
            List<Sale> saleList = _db.Sales.Where(s => s.UserId == user.Id).ToList();
            var salePosts = _db.SalePosts.ToList();
            var vm = new PostViewModel()
            {
                SelectedListItem = saleList.Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList(),
                ListOfSales = post.SalePosts.Select(x => x.Sale).ToList(),
                Body = post.Body,
                Name = post.Name,
                Id = post.Id
            };
            return View(vm);
        }

        //POST Edit Action
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PostViewModel pvm)
        {
            if (id != pvm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var postFromDb = _db.Posts.SingleOrDefault(x => x.Id == id);
                postFromDb.Body = pvm.Body;
                postFromDb.Name = pvm.Name;
                postFromDb.LastEdited = DateTime.Now;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(pvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSaleToPost([FromBody] SalePostForm s)
        {
            var user = await _userManager.GetUserAsync(User);
            var post = _db.Posts.Single(x => x.Id == s.PostId);
            if (post.SalePosts.Any(x => x.SaleId == s.SaleId)) return BadRequest();
            {
                var sale = _db.Sales.Single(x => x.Id == s.SaleId);
                post.SalePosts.Add(new SalePost() {Post = post, Sale = sale});
                await _db.SaveChangesAsync();
                return PartialView("Sale/_SaleItemPartial",
                    new SaleItemPartialViewModel() {Name = sale.Name, Id = sale.Id});
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveSaleFromPost([FromBody] SalePostForm s)
        {
            var user = await _userManager.GetUserAsync(User);
            var post = _db.Posts.Single(x => x.Id == s.PostId);
            if (post.SalePosts.All(x => x.SaleId != s.SaleId)) return BadRequest();
            {
                var salePost = _db.SalePosts.Single(x => x.SaleId == s.SaleId && x.PostId == s.PostId);
                post.SalePosts.Remove(salePost);
                await _db.SaveChangesAsync();
                return Ok();
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = await _db.Posts.FindAsync(id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _db.Posts.FindAsync(id);


            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        //POST Delete Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var posts = await _db.Posts.FindAsync(id);

            _db.Posts.Remove(posts);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult Publish(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

      
        private async Task PublishPost(int id)
        {

            var user = await _userManager.GetUserAsync(User);

            var postFromDb = _db.Posts.Single(x=>x.Id == id);
            


            var postList = _db.Posts.Where(p => p.UserId == user.Id).ToList();
            var b = _db.Businesses.Single(x => x.UserId == user.Id);

            var results = Publisher.ValidToPost(postList, postFromDb, b);
            if (results.Item1 == true)
            {
                var publishedPost = new PublishedPost
                {
                    Post = postFromDb,
                    Business = b
                };
                postFromDb.IsPublished = true;
                _db.PublishedPosts.Add(publishedPost);
                await _db.SaveChangesAsync();
                ViewData["PublishSuccess"] = results.Item2;
            }
            else
            {
                ViewData["PublishAlert"] = results.Item2;
            }
        }

        private async Task UnPublishPost(int id)
        {
            var postFromDb = _db.Posts.Single(x => x.Id == id);
            postFromDb.IsPublished = false;
            var publishedPostFromDb = _db.PublishedPosts.SingleOrDefault(p => p.PostId == id);
            if (publishedPostFromDb != null)
            {
                _db.Remove(publishedPostFromDb);
                await _db.SaveChangesAsync();
                ViewData["UnpublishAlert"] = "הפוסט הוסר מהפירסום בהצלחה!";
            }
            else
            {
                ViewData["PublishError"] = "התרחשה שגיאת בעת הסרת הפוסט.";
            }
        }

        private void BumpPost(int id)
        {
            var publishedPostFromDb = _db.PublishedPosts.Single(p => p.PostId == id);
            var results = Bumper.ValidToBump(publishedPostFromDb);

            if (results.Item1 == true)
            {
                publishedPostFromDb.PublishTime = DateTime.Now;
                _db.SaveChanges();
                ViewData["PublishSuccess"] = results.Item2;
            }
            else
            {
                ViewData["BumpAlert"] = results.Item2;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DontShowSaleHelpfulAlert()
        {
            var user = await _userManager.GetUserAsync(User);
            var userFromDb = _db.Users.Single(x => x.Id == user.Id);
            userFromDb.DontShowHelpfulSaleAlert = true;
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}