using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Models.Dtos
{
    public class PostDto
    {
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public string Body { get; set; }
    }
}
