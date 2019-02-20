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
            foreach (var item in postViewModel.SelectedListItem)
            {
                var postId = postViewModel.Post.id;
                var saleId = Int32.Parse(item.Value);
                var salePostExist = db.SalePosts.
                    Any(sp => (sp.PostId == postViewModel.Post.id)
                    && (sp.SaleId == Int32.Parse(item.Value)));

                if (item.Selected == true)
                {

                    
                    if (!salePostExist)
                    {
                        db.SalePosts.Add(new SalePost
                        {
                            PostId = postId,
                            SaleId = saleId
                        });
                    }

                }
                else
                {
                    if (salePostExist)
                    {
                       var salePost =  db.SalePosts.SingleOrDefault(sp => (sp.SaleId == saleId)&&(sp.PostId == postId));

                        db.SalePosts.Remove(salePost);
                    }
                }
            }

            db.SaveChanges();
        }
    }
}
