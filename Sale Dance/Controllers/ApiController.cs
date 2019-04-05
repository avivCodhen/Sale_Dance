using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sale_Dance.Data;
using Sale_Dance.Services.Interfaces;

namespace Sale_Dance.Controllers
{
    public class ApiController : Controller
    {
        IPublishedPostsService publishedPostsService;
        private readonly ApplicationDbContext _db;
        public ApiController(IPublishedPostsService publishedPostsService, 
            ApplicationDbContext db)
        {
            this.publishedPostsService = publishedPostsService;
            _db = db;
        }
        public IActionResult Index()
        {
            var results = publishedPostsService.GetPublishedPosts();

            return new JsonResult(results);
        }

        public IActionResult Image(int id)
        {
            var bytes = _db.Images.SingleOrDefault(x => x.Id == id)?.Bytes;
            return new JsonResult(bytes);
        }
    }
}