using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Models.ViewModel.UserViewModel
{
    public class UserViewModel
    {
        public List<ApplicationUser> Users{ get; set; }
        public List<Business> UserBusiness { get; set; }
    }
}
