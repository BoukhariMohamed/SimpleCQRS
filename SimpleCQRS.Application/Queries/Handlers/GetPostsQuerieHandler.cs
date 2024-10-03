using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SimpleCQRS.Application.DTOs;
using SimpleCQRS.Application.Exceptions;
using SimpleCQRS.Application.Hubs;
using SimpleCQRS.Domain.Interfaces.Repositories;
using SimpleCQRS.Infrastructure;

namespace SimpleCQRS.Application.Queries.Handlers
{
    public class GetPostsQuerieHandler : IRequestHandler<GetPostsQuerie, IEnumerable<GetPostDto>>
    {

        private readonly IGenericRepository<Post> _postRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _hubContext;


        public GetPostsQuerieHandler(IGenericRepository<Post> postRepository, IMapper mapper, IHubContext<NotificationHub> hubContext)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _hubContext = hubContext;
        }


        public async Task<IEnumerable<GetPostDto>> Handle(GetPostsQuerie request, CancellationToken cancellationToken)
        {
            try
            {
                var postes = await _postRepository.GetListAsync(disableTracking: true);
                
                var mapPostes = _mapper.Map<IEnumerable<GetPostDto>>(postes);

                await _hubContext.Clients.All.SendAsync("PostsRetrieved",postes.Select(x=>new GetPostDto
                {
                    Content = x.Content,
                    Title = x.Title,
                    PostId = x.PostId,
                    DateCreated = x.DateCreated,
                    LastModified = x.LastModified
                }));

                return mapPostes;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}
