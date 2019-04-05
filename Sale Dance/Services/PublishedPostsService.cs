using Sale_Dance.Data;
using Sale_Dance.Models;
using Sale_Dance.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sale_Dance.Models.Dtos;

namespace Sale_Dance.Services
{
    public class PublishedPostsService : IPublishedPostsService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper _mapper;
        public PublishedPostsService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            _mapper = mapper;
        }

        public List<PublishedPostDto> GetPublishedPosts()
        {
            return db.PublishedPosts
                .Include(x=>x.Business).ThenInclude(x=>x.User).Include(x=>x.Business)
                .Include(x=>x.Post).ThenInclude(x=>x.SalePosts).ThenInclude(x=>x.Sale)
                .Select(x=> new PublishedPostDto()
            {
                Sales = x.Post.SalePosts.Select(s=> _mapper.Map<SaleDto>(s.Sale)).ToList(),
                Post = _mapper.Map<PostDto>(x.Post),
                Business = _mapper.Map<BusinessDto>(x.Business),
                PublishTime = x.PublishTime

            }).ToList();
        }
    }
}
