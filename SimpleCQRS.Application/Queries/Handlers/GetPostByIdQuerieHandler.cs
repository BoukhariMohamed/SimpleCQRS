using AutoMapper;
using MediatR;
using SimpleCQRS.Application.DTOs;
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

        public GetPostByIdQuerieHandler(IGenericRepository<Post> postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
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
            var result =await _postRepository.GetAsync(predicate:x=>x.PostId == request.postId , disableTracking:true) ?? 
                         throw new Exception($"Post Not Found With Id : {request.postId}");
           
            return _mapper.Map<GetPostDto>(result);
        }
    }
}
