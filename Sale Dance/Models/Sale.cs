using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Sale_Dance.Models
{
    public class Sale 
    {
        [Key] public int Id { get; set; }
        public DateTime Created = DateTime.Now;
        public DateTime? LastEdited { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")] public virtual ApplicationUser User { get; set; }
        public string Name { get; set; }

        public int? ImageId { get; set; }
        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }

        public double BeforePrice { get; set; }
        public double SalePrice { get; set; }
        public virtual ICollection<SalePost> SalePosts { get; set; }
        public string Description { get; set; }
    }
}