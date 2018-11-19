using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Models
{
    public class SalePosts
    {
        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }

    }
}
