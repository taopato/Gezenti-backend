using Gezenti.Application.Features.Place.Commands;
using Gezenti.Application.Features.Place.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gezenti.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlaceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlaces()
        {
            var query = new GetAllPlacesQuery();
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaceById(int id)
        {
            var query = new GetPlaceByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetPlacesByCategory(int categoryId)
        {
            var query = new GetPlacesByCategoryQuery { CategoryId = categoryId };
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlace(CreatePlaceCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlace(int id, UpdatePlaceCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            var command = new DeletePlaceCommand { Id = id };
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}

