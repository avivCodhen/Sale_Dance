using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Sale_Dance.Models
{
    public class Sale
    {
       [Key] public int id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public double BeforePrice { get; set; }
        public double AfterPrice { get; set; }
        public virtual List<SalePost> SalePosts { get; set; }

    }
}
