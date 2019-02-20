using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sale_Dance.Data;
using Sale_Dance.Models;
using Sale_Dance.Models.ViewModel;
using Sale_Dance.Services;
using Sale_Dance.Soldiers;
using Sale_Dance.Utility;

namespace Sale_Dance.Controllers
{
    [Authorize()]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext db;
        private readonly IHttpContextAccessor httpContext;
        private string userId;
        private PostViewModel postViewModel;
        private readonly ISalePostService salePostService;

        public HomeController(ApplicationDbContext db,
            IHttpContextAccessor httpContext,
            ISalePostService salePostService)
        {

            this.db = db;
            this.httpContext = httpContext;
            userId = httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            postViewModel = new PostViewModel();
            this.salePostService = salePostService;
        }
        public IActionResult Index()
        {
            var list = db.Posts.Where(p => p.OwnerId == userId).ToList();
            list.Reverse();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            List<Sale> saleList = db.Sales.Where(s => s.OwnderId == userId).ToList();
            var salePosts = db.SalePosts.ToList();
            postViewModel.Post = new Post();
            postViewModel.SelectedListItem = saleList.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.id.ToString(),
                Selected = false
            }).ToList();
            return View(postViewModel);
        }

        //POST Create Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.OwnerId = userId;
                
                db.Add(post);
                await db.SaveChangesAsync();
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

            var post = await db.Posts.FindAsync(id);
            List<Sale> saleList = db.Sales.Where(s=> s.OwnderId == userId).ToList();
            var salePosts = db.SalePosts.ToList();
            postViewModel.Post = post;
            postViewModel.SelectedListItem = saleList.Select(s => new SelectListItem {
                Text = s.Name,
                Value = s.id.ToString(),
                Selected = salePosts.Any(salePost=> salePost.SaleId == s.id)
            }).ToList();

            if (post == null)
            {
                return NotFound();
            }
            return View(postViewModel);
        }


        //POST Edit Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostViewModel pvm)
        {
            if(id != pvm.Post.id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            { 

                salePostService.UpdateSalePost(pvm);
                pvm.Post.OwnerId = userId;
                db.Update(pvm.Post);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pvm.Post);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = await db.Posts.FindAsync(id);
            if (posts == null)
            {
                return NotFound();
            }
            return View(posts);
        }


        //POST Details Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, Post post)
        {
            if (id != post.id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                db.Update(post);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await db.Posts.FindAsync(id);


            
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

            var posts = await db.Posts.FindAsync(id);

            db.Posts.Remove(posts);
            await db.SaveChangesAsync();

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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Publish(int? id)
        {

            if(id == null)
            {
                return NotFound();
            }

            var postFromDb = db.Posts.Find(id);
            var postList = db.Posts.Where(p => p.OwnerId == userId).ToList();
            if(postFromDb == null)
            {
                return NotFound();
            }
            var results = Publisher.ValidToPost(postList, postFromDb);
            if (results.Item1 == true)
            {
                var publishedPost = new PublishedPost
                {
                    PostId = postFromDb.id
                };
                postFromDb.IsPublished = true;
                var business = db.Businesses.Where(b => b.BusinessOwnerId == userId).SingleOrDefault();
                publishedPost.BusinessId = business.id;
                db.PublishedPosts.Add(publishedPost);
                db.SaveChanges();
                TempData["PublishSuccess"] = results.Item2;
            }
            else
            {
                TempData["PublishAlert"] = results.Item2;

            }

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Unpublish(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var postFromDb = db.Posts.Find(id);
            postFromDb.IsPublished = false;
            var publishedPostFromDb = db.PublishedPosts.Where(p=>p.PostId == id).SingleOrDefault();
            if(publishedPostFromDb != null)
            {
                db.Remove(publishedPostFromDb);
                db.SaveChanges();
                TempData["UnpublishAlert"] = "הפוסט הוסר מהפירסום בהצלחה!";
            }
            else
            {
                TempData["PublishError"] = "התרחשה שגיאת בעת הסרת הפוסט.";
            }

            return RedirectToAction(nameof(Index));
            //var db.PublishedPosts.Where(p => p.PostId == id);
        }

        public IActionResult Bump(int? id)
        {
            if( id == null)
            {
                return NotFound();
            }
            var publishedPostFromDb = db.PublishedPosts.Where(p => p.PostId == id).SingleOrDefault();
            var results = Bumper.ValidToBump(publishedPostFromDb);

            if (results.Item1 == true)
            {
                publishedPostFromDb.PublishTime = DateTime.Now;
                db.SaveChanges();
                TempData["PublishSuccess"] = results.Item2;
            }
            else
            {
                TempData["PublishAlert"] = results.Item2;

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
