using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Sale_Dance.Models
{
    public class Business 
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public string Name { get; set; } = "";
        public string BusinessEmailContact { get; set; } = "";
        public string Site { get; set; } = "";
        public string BusinessPhoneContact { get; set; } = "";
        public string About { get; set; } = "";
        public string Address { get; set; } = "";
        public int? ImageId { get; set; }
        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }
        public string WeekDays { get; set; } = "";
        public string Friday { get; set; } = "";
        public string Saturday { get; set; } = "";

    }
}
