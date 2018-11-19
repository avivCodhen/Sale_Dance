using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Models.ViewModel
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public List<Sale> Sales { get; set; }
        public List<SelectListItem> SelectedListItem { get; set; } 
    }
}
