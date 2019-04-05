using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Sale_Dance.Models.Dtos;
using Sale_Dance.Utility;

namespace Sale_Dance.Models.Profiles
{
    public class PublishedPostProfile : Profile
    {
        public PublishedPostProfile()
        {
            CreateMap<PublishedPost, PublishedPostDto>().AfterMap((src, dest) =>
                dest.PublishTime = DateTime.Parse(dest.PublishTime.YearMonthDay()));
        }
    }
}
