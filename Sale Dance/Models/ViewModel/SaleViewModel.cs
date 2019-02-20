using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Models.ViewModel
{
    public class SaleViewModel
    {
        [Required(ErrorMessage = "יש להזין שם")]
        public string Name { get; set; }
        public byte[] Image { get; set; }

        [Required(ErrorMessage = "יש להזין מחיר לפני")]
        public double BeforePrice { get; set; }

        [Required(ErrorMessage = "יש להזין מחיר אחרי ההנחה")]

        public double AfterPrice { get; set; }
    }
}
