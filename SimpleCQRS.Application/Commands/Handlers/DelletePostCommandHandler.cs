using MediatR;
using Microsoft.AspNetCore.SignalR;
using SimpleCQRS.Application.Exceptions;
using SimpleCQRS.Application.Hubs;
using SimpleCQRS.Domain.Interfaces;
using SimpleCQRS.Domain.Interfaces.Repositories;
using SimpleCQRS.Infrastructure;

namespace SimpleCQRS.Application.Commands.Handlers
{
    public class DelletePostCommandHandler : IRequestHandler<DelletePostCommand, bool>
    {
        /// <summary>
        /// Post Repository
        /// </summary>
        private readonly IGenericRepository<Post> _postRepository;
        /// <summary>
        /// IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<NotificationHub> _hubContext;

        public DelletePostCommandHandler(
            IGenericRepository<Post> postRepository, 
            IUnitOfWork unitOfWork, 
            IHubContext<NotificationHub> hubContext)
        {
            _postRepository = postRepository;
            _hubContext = hubContext;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Dellete Post Handle
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Handle(DelletePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Exception($"Post Not Found {request.postId}");
                var post = await _postRepository.GetAsync(predicate: x => x.PostId == request.postId, disableTracking: true)
               ?? throw new NotFoundModelException(nameof(Post), request.postId);

                _postRepository.Delete(post);

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();

                // Notify clients via SignalR about the post deletion
                await _hubContext.Clients.All.SendAsync("PostDeleted", request.postId);


                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
           
        }
    }
}
