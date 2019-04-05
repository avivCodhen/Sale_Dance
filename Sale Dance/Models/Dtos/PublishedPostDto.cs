using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Models.Dtos
{
    public class PublishedPostDto
    {
        public  BusinessDto Business { get; set; }
        public  PostDto Post { get; set; }
        public DateTime PublishTime { get; set; }
        public List<SaleDto> Sales { get; set; }

    }
}
