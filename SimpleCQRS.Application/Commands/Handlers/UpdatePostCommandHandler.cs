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
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, bool>
    {
        /// <summary>
        /// Post Repository
        /// </summary>
        private readonly IGenericRepository<Post> _postRepository;

        /// <summary>
        /// IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Validator Update Post
        /// </summary>
        private readonly IValidator<UpdatePostCommand> _validator;

        private readonly IHubContext<NotificationHub> _hubContext;

        public UpdatePostCommandHandler(IGenericRepository<Post> postRepository, IUnitOfWork unitOfWork, IValidator<UpdatePostCommand> validator, IHubContext<NotificationHub> hubContext)
        {
            _postRepository = postRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }


        public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var validationResult = await _validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                var post = await _postRepository.GetAsync(predicate: x => x.PostId == request.PostId) ??
                     throw new NotFoundModelException(nameof(Post), request.PostId);

                post.UpdatePost(request.Title, request.Content);

                _postRepository.Update(post);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                await _hubContext.Clients.All.SendAsync("PostUpdated",new GetPostDto
                {
                    Content = post.Content,
                    Title = post.Title,
                    PostId = post.PostId,
                    DateCreated = post.DateCreated,
                    LastModified = post.LastModified
                });

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
