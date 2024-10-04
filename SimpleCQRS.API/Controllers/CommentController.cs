using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCQRS.Application.Commands;
using SimpleCQRS.Application.Commands.Handlers;
using SimpleCQRS.Application.Commands.Validators;

namespace SimpleCQRS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommantCommand command)
        {
            var commandId = await _mediator.Send(command);

            return Ok(commandId);
        }


    }
}
