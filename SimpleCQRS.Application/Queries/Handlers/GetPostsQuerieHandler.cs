using AutoMapper;
using MediatR;
using SimpleCQRS.Application.DTOs;
using SimpleCQRS.Domain.Interfaces.Repositories;
using SimpleCQRS.Infrastructure;

namespace SimpleCQRS.Application.Queries.Handlers
{
    public class GetPostsQuerieHandler : IRequestHandler<GetPostsQuerie, IEnumerable<GetPostDto>>
    {

        private readonly IGenericRepository<Post> _postRepository;
        private readonly IMapper _mapper;

        public GetPostsQuerieHandler(IGenericRepository<Post> postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<GetPostDto>> Handle(GetPostsQuerie request, CancellationToken cancellationToken)
        {
            try
            {
                var postes = await _postRepository.GetListAsync(disableTracking: true);
                
                var mapPostes = _mapper.Map<IEnumerable<GetPostDto>>(postes);

                return mapPostes;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}
