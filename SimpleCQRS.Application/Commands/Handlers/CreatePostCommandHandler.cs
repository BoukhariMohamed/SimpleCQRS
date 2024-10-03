using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SimpleCQRS.Application.DTOs;
using SimpleCQRS.Application.Exceptions;
using SimpleCQRS.Application.Hubs;
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
        /// <summary>
        /// Unit Of Work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Notification Hub
        /// </summary>
        private readonly IHubContext<NotificationHub> _hubContext;

        private readonly IValidator<CreatePostCommand> _validator;


        public CreatePostCommandHandler(
            IGenericRepository<Post> postRepository, 
            IUnitOfWork unitOfWork, 
            IHubContext<NotificationHub> hubContext , IValidator<CreatePostCommand> validator)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
            _validator = validator;
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
                await _unitOfWork.BeginTransactionAsync();

                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    throw new InvalidModelException("Not Valid Contect or Titre");
                }

                var post = Post.CreatePost(request.title, request.content);

                await _postRepository.AddedAsync(post);

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();

                await _hubContext.Clients.All.SendAsync("PostCreated",new GetPostDto
                {
                    Content = post.Content,
                    Title = post.Title,
                    PostId = post.PostId,
                    DateCreated = post.DateCreated,
                    LastModified = post.LastModified    
                });

                return post.PostId;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
          
        }
    }
}
