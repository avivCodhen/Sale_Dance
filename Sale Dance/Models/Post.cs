using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Models
{
    public class Post
    {

        public int id { get; set; }

        public string OwnerId { get; set; }

        [Required(ErrorMessage ="יש להזין כותרת")]
        public string Name { get; set; }

        public string CreatedOn = DateTime.Now.ToString(CultureInfo.CreateSpecificCulture("en-GB"));

        public DateTime LastPublished{ get; set; }

        public virtual List<SalePost> Sales { get; set; }

        public bool IsPublished { get; set; }

        [Required(ErrorMessage = "יש להזין תוכן הודעה")]
        [StringLength(500, ErrorMessage = "יש להזין תוכן בין 4 עד 500 תווים", MinimumLength = 4)]
        public string Body { get; set; }


        public virtual Business Business { get; set; }

    }
}
