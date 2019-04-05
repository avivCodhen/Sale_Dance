using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Models
{
    public class PublishedPost
    {
        [Key] public int Id { get; set; }
        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post{ get; set; }

        public int BusinessId { get; set; }
        

        [ForeignKey("BusinessId")]
        public virtual Business Business { get; set; }

        public DateTime PublishTime = DateTime.Now;
    }
}
