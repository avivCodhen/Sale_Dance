using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Models.Dtos
{
    public class SaleDto
    {
        public string Name { get; set; }
        public int ImageId { get; set; }
        public double BeforePrice { get; set; }
        public double SalePrice { get; set; }

    }
}