using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Sale_Dance.Models
{
    public class Post
    {
        [Key] public int Id { get; set; }

        public DateTime? LastEdited { get; set; }
        public string UserId { get; set; }


        [ForeignKey("UserId")] public virtual ApplicationUser User { get; set; }

        public string Name { get; set; }

        public DateTime Created = DateTime.Now;

        public DateTime LastPublished { get; set; }

        public virtual ICollection<SalePost> SalePosts { get; set; }

        public bool IsPublished { get; set; }

        public string Body { get; set; }
    }
}