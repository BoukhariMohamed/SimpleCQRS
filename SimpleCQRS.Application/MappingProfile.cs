using AutoMapper;
using SimpleCQRS.Application.DTOs;
using SimpleCQRS.Infrastructure;

namespace SimpleCQRS.Application
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //CreateMap<Post, GetPostDto>().ReverseMap();

            // Map Post entity to GetPostDto
            CreateMap<Post, GetPostDto>()
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));

            // Map Comment entity to GetCommentDto
            CreateMap<Comment, GetCommentDto>();
        }
    }
}
