using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Sale_Dance.Models
{
    public class SalePostForm
    {
        [JsonProperty("saleId")]
        public int SaleId { get; set; }

        [JsonProperty("postId")]
        public int PostId { get; set; }
    }
}
