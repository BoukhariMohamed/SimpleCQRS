﻿using FluentValidation;
using MediatR;
using SimpleCQRS.Application.Exceptions;
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


        public UpdatePostCommandHandler(IGenericRepository<Post> postRepository, IUnitOfWork unitOfWork, IValidator<UpdatePostCommand> validator)
        {
            _postRepository = postRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
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
