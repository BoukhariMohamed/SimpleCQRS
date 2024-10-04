using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using SimpleCQRS.Application.DTOs;
using SimpleCQRS.Application.Exceptions;
using SimpleCQRS.Application.Hubs;
using SimpleCQRS.Domain.Interfaces;
using SimpleCQRS.Domain.Interfaces.Repositories;
using SimpleCQRS.Infrastructure;

namespace SimpleCQRS.Application.Commands.Handlers;

public class CreateCommentHandler(
    IGenericRepository<Comment> commentRepository,
    IUnitOfWork unitOfWork,
    IGenericRepository<Post> postRepository,
    IValidator<CreateCommantCommand> validator,
    IHubContext<NotificationHub> hubContext) : IRequestHandler<CreateCommantCommand, Guid>
{
    /// <summary>
    /// Comment Repository
    /// </summary>
    private readonly IGenericRepository<Comment> _commentRepository = commentRepository;

    /// <summary>
    /// Post Repository
    /// </summary>
    private readonly IGenericRepository<Post> _postRepository = postRepository;

    /// <summary>
    /// Unit Of Work
    /// </summary>
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    private readonly IValidator<CreateCommantCommand> _validator = validator;

    private readonly IHubContext<NotificationHub> _hubContext = hubContext;

    public async Task<Guid> Handle(CreateCommantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new InvalidModelException("Not Valid Comment!");
            }

            var post = await _postRepository.GetAsync(x => x.PostId == request.PostId) ??
                throw new NotFoundModelException(nameof(Post), "post not exist");


            var comment = Comment.CreateComment(request.PostId, request.Text);

            await _commentRepository.AddedAsync(comment);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            await _hubContext.Clients.All.SendAsync("CommentCreated", new GetCommentDto
            {
                CommentId = comment.CommentId,
                PostId = request.PostId,
                Text = request.Text
            });

            return comment.CommentId;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
        
    }
}
