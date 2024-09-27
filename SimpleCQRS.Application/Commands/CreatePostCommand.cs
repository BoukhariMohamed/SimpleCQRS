using MediatR;

namespace SimpleCQRS.Application.Commands
{
    public class CreatePostCommand : IRequest<Guid> //Guid is Out value
    {
        public string title { get; set; }
        public string content { get; set; }
    }
}
