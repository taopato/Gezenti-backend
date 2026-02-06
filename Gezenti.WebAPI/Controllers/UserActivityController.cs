using Gezenti.Application.Features.UserActivity.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gezenti.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserActivityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserActivityCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserActivityCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteUserActivityCommand { Id = id };
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}

