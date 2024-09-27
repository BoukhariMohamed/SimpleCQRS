using MediatR;
using SimpleCQRS.Application.DTOs;

namespace SimpleCQRS.Application.Queries
{
    public class GetPostByIdQuerie : IRequest<GetPostDto>
    {
        public Guid postId { get; set; }
    }
}
