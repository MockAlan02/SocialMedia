using AutoMapper;
using SocialMedia.Core.Dtos;
using SocialMedia.Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace SocialMedia.Infrastructure.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostDto,Post>();
        }
    }
}
