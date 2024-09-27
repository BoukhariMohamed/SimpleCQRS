using MediatR;
using SimpleCQRS.Domain.Interfaces;
using SimpleCQRS.Domain.Interfaces.Repositories;
using SimpleCQRS.Infrastructure;

namespace SimpleCQRS.Application.Commands.Handlers
{
    internal class CreatePostCommandHandler : IRequestHandler<CreatePostCommand,Guid>
    {
        /// <summary>
        /// Post Repository
        /// </summary>
        private readonly IGenericRepository<Post> _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        //whay you dont use DTO
        public CreatePostCommandHandler(IGenericRepository<Post> postRepository, IUnitOfWork unitOfWork)
        {
             _postRepository = postRepository;
             _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle To Create New Post
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// this handler just do one action
        public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
             
                var post = Post.CreatePost(request.title, request.content);

                await _postRepository.AddedAsync(post);

                await _unitOfWork.SaveChangesAsync();

                return post.PostId;
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }
    }
}
