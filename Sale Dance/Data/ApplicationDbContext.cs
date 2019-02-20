using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sale_Dance.Models;

namespace Sale_Dance.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SalePost>()
                .HasKey(t => new { t.SaleId, t.PostId });

            modelBuilder.Entity<SalePost>()
                .HasOne(p => p.Post)
                .WithMany(x => x.Sales)
                .HasForeignKey(y => y.PostId);

            modelBuilder.Entity<SalePost>()
               .HasOne(p => p.Sale)
               .WithMany(x => x.SalePosts)
               .HasForeignKey(y => y.SaleId);

            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Sale> Sales{ get; set; }
        public DbSet<ApplicationUser> Users{ get; set; }
        public DbSet<PublishedPost> PublishedPosts{ get; set; }
        public DbSet<Business> Businesses{ get; set; }
        public DbSet<SalePost> SalePosts{ get; set; }
    }

    
}
