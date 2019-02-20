using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Models
{
    public class Business
    {
        public int id { get; set; }
        public string BusinessOwnerId { get; set; }

        [ForeignKey("BusinessOwnerId")]
        public virtual ApplicationUser BusinessOwner { get; set; }
        public string Name { get; set; }
        public int BusinessEmailContact { get; set; }
        public string Site { get; set; }
        public string BusinessPhoneContact { get; set; }
        public string About { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string WeekDays { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }

    }
}
