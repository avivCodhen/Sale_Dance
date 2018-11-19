using Sale_Dance.Data;
using Sale_Dance.Models;
using Sale_Dance.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sale_Dance.Services
{
    public class PublishedPostsService : IPublishedPostsService
    {
        private readonly ApplicationDbContext db;
        public PublishedPostsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<PublishedPost> GetPublishedPosts()
        {
            return db.PublishedPosts.Include(pp => pp.Business).
                Include(pp => pp.Post)
                .ThenInclude(s => s.Sales)
                .ThenInclude(sales => sales.Sale).ToList();
        }
    }
}
