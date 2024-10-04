using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SimpleCQRS.Application.DTOs;
using SimpleCQRS.Application.Exceptions;
using SimpleCQRS.Application.Hubs;
using SimpleCQRS.Domain.Interfaces.Repositories;
using SimpleCQRS.Infrastructure;

namespace SimpleCQRS.Application.Queries.Handlers
{
    public class GetPostByIdQuerieHandler : IRequestHandler<GetPostByIdQuerie, GetPostDto>
    {
        /// <summary>
        /// Post Repository
        /// </summary>
        private readonly IGenericRepository<Post> _postRepository;
        /// <summary>
        /// Auto Mapper
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// Notification Hub
        /// </summary>
        private readonly IHubContext<NotificationHub> _hubContext;

        public GetPostByIdQuerieHandler(IGenericRepository<Post> postRepository, IMapper mapper, IHubContext<NotificationHub> hubContext)
        {
            _postRepository = postRepository;
            _hubContext = hubContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Handle Get Post
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<GetPostDto> Handle(GetPostByIdQuerie request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetAsync(predicate:x=>x.PostId == request.postId , include:inc=> inc.Include(p=>p.Comments)) ?? 
                         throw new NotFoundModelException(nameof(Post),request.postId);

            var postDto = _mapper.Map<GetPostDto>(post);

            await _hubContext.Clients.All.SendAsync("PostGet", postDto);

            return postDto;
        }
    }
}
