using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sale_Dance.Models;

namespace Sale_Dance.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SalePosts>()
                .HasKey(t => new { t.SaleId, t.PostId });

            modelBuilder.Entity<SalePosts>()
                .HasOne(p => p.Post)
                .WithMany(x => x.Sales)
                .HasForeignKey(y => y.PostId);

            modelBuilder.Entity<SalePosts>()
               .HasOne(p => p.Sale)
               .WithMany(x => x.Posts)
               .HasForeignKey(y => y.SaleId);


        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Sale> Sales{ get; set; }
        public DbSet<ApplicationUser> Users{ get; set; }
        public DbSet<PublishedPost> PublishedPosts{ get; set; }
        public DbSet<Business> Businesses{ get; set; }
        public DbSet<SalePosts> SalePosts{ get; set; }
    }

    
}
