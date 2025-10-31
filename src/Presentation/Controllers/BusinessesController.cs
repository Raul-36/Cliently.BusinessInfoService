using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Businesses.Commands;
using Application.Businesses.DTOs.Requests;
using Application.Businesses.DTOs.Responses;
using Application.Businesses.Queries;
using Application.Common;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessesController : ControllerBase
    {
        private IMediator mediator;

        public BusinessesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BusinessResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBusiness([FromBody] CreateBusinessRequest request)
        {
            var command = new CreateBusinessCommand(request);
            var response = await this.mediator.Send(command);
            if (response.IsSuccess)
            {
                return CreatedAtAction(nameof(GetBusinessById), new { id = response.Value!.Id }, response.Value);
            }
            return BadRequest(response.Errors);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BusinessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBusinessById(Guid id)
        {
            var query = new GetBusinessByIdQuery(id);
            var response = await this.mediator.Send(query);
            if (response.IsSuccess)
            {
                return Ok(response.Value);
            }
            return NotFound(response.Errors);
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ShortBusinessResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBusinesses()
        {
            var query = new GetAllBusinessesQuery();
            var response = await this.mediator.Send(query);
            return Ok(response);
        }

        [HttpPatch("name")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBusinessName([FromBody] UpdateBusinessRequest request)
        {
            var command = new UpdateBusinessNameCommand { Business = request };
            var response = await this.mediator.Send(command);
            if (response.IsSuccess)
            {
                return NoContent();
            }
            return NotFound(response.Errors);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBusiness(Guid id)
        {
            var command = new DeleteBusinessCommand(){ Id = id };
            var response = await this.mediator.Send(command);
            if (response.IsSuccess)
            {
                return NoContent();
            }
            return NotFound(response.Errors);
        }
    }
}
