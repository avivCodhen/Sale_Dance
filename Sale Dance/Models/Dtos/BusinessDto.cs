using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Models.Dtos
{
    public class BusinessDto
    {
        public string Name { get; set; }
        public string BusinessEmailContact { get; set; }
        public string Site { get; set; }
        public string BusinessPhoneContact { get; set; }
        public string About { get; set; }
        public string Address { get; set; }
        public int ImageId { get; set; }
        public string WeekDays { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
    }
}
