﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Models
{
    public class Sale
    {
        public int id { get; set; }

        public string OwnderId { get; set; }
        [Required(ErrorMessage = "יש להזין שם")]
        public string Name { get; set; }
        public string Image { get; set; }

        [Required(ErrorMessage = "יש להזין מחיר לפני")]
        public double BeforePrice { get; set; }

        [Required(ErrorMessage = "יש להזין מחיר אחרי ההנחה")]

        public double AfterPrice { get; set; }

        public List<SalePosts> Posts { get; set; }
    }
}
