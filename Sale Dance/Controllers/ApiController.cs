using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sale_Dance.Services.Interfaces;

namespace Sale_Dance.Controllers
{
    public class ApiController : Controller
    {
        IPublishedPostsService publishedPostsService;
        public ApiController(IPublishedPostsService publishedPostsService)
        {
            this.publishedPostsService = publishedPostsService;
        }
        public IActionResult Index()
        {
            var results = publishedPostsService.GetPublishedPosts();
            return new JsonResult(results);
        }
    }
}