using AutoMapper;
using SimpleCQRS.Application.DTOs;
using SimpleCQRS.Infrastructure;

namespace SimpleCQRS.Application
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, GetPostDto>().ReverseMap();
        }
    }
}
