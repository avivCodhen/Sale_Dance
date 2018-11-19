using Sale_Dance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Services.Interfaces
{
    public interface IPublishedPostsService
    {
       
        List<PublishedPost> GetPublishedPosts();
    }
}
