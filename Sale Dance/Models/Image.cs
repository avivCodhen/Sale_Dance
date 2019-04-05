using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Models
{
    public class Image
    {
        [Key]public int Id { get; set; }
        public byte[] Bytes { get; set; }

        public int SaleId { get; set; }

    }
}
