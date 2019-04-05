using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Models.ViewModel
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string LastEdit { get; set; }

        [Required(ErrorMessage = "יש להזין כותרת")]
        [StringLength(50, ErrorMessage = "יש להזין תוכן בין 4 עד 50 תווים", MinimumLength = 4)]
        public string Name { get; set; }

        public List<SalePost> Sales { get; set; }
        public List<Sale> ListOfSales { get; set; }

        [Required(ErrorMessage = "יש להזין תוכן הודעה")]
        [StringLength(500, ErrorMessage = "יש להזין תוכן בין 4 עד 500 תווים", MinimumLength = 4)]
        public string Body { get; set; }
        public List<SelectListItem> SelectedListItem { get; set; } 


    }
}
