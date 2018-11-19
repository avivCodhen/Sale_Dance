using Sale_Dance.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Services
{
    public interface ISalePostService
    {

        void CreateSalePost(int saleId, int postId);
        void UpdateSalePost(PostViewModel postViewModel);
    }
}
