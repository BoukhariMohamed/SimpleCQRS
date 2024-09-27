using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleCQRS.Application.Commands;
using SimpleCQRS.Application.DTOs;
using SimpleCQRS.Application.Queries;

namespace SimpleCQRS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
             _mediator = mediator;
        }

        #region Queries

              [HttpGet]
              public async Task<IActionResult> GetPosts()
              {
                  var posts = await _mediator.Send(new GetPostsQuerie());
                  return Ok(posts);
              }


              [HttpGet("{id}")]
              public async Task<IActionResult> GetPostById(Guid id)
              {
                  var post = await _mediator.Send(new GetPostByIdQuerie{ postId = id});
                  return Ok(post);
              }

        #endregion

       
        #region Commands

             [HttpDelete("{id}")]
             public async Task<IActionResult> DelletePostById(Guid id)
             {
                 var post = await _mediator.Send(new DelletePostCommand { postId = id });
                 return Ok(post);
             }

             [HttpPost]
             public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
             {
                 var postId = await _mediator.Send(command);

                 return CreatedAtAction(nameof(GetPostById), new { id = postId }, null);
             }


             [HttpPut]
             public async Task<IActionResult> CreatePost([FromBody] UpdatePostCommand command)
             {
                 var postId = await _mediator.Send(command);

                 return CreatedAtAction(nameof(GetPostById), new { id = postId }, null);
             }

        #endregion


    }
}
