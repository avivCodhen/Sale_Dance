using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Sale_Dance.Models
{
    public class SalePost
    {
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

        public int SaleId { get; set; }

        [ForeignKey("SaleId")]
        public virtual Sale Sale { get; set; }

    }
}
