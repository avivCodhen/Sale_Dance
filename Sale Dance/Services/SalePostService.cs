using Sale_Dance.Data;
using Sale_Dance.Models;
using Sale_Dance.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Services
{
    public class SalePostService : ISalePostService
    {
        private readonly ApplicationDbContext db;
        public SalePostService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateSalePost(int saleId, int postId)
        {
            throw new NotImplementedException();
        }

        public void UpdateSalePost(PostViewModel postViewModel)
        {
            
            db.SaveChanges();
        }
    }
}
