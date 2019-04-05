using Sale_Dance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sale_Dance.Models.Dtos;

namespace Sale_Dance.Services.Interfaces
{
    public interface IPublishedPostsService
    {
       
        List<PublishedPostDto> GetPublishedPosts();
    }
}
