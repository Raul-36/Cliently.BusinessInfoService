using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Businesses.Commands;
using Application.Businesses.DTOs.Requests;
using Application.Businesses.DTOs.Responses;
using Application.Businesses.Queries;

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
            return CreatedAtAction(nameof(GetBusinessById), new { id = response.Id }, response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BusinessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBusinessById(Guid id)
        {
            var query = new GetBusinessByIdQuery(id);
            var response = await this.mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBusinessName(Guid id, [FromBody] UpdateBusinessRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest("ID mismatch");
            }
            var command = new UpdateBusinessNameCommand { Business = request };
            var response = await this.mediator.Send(command);
            if (response == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBusiness(Guid id)
        {
            var command = new DeleteBusinessCommand(){ Id = id };
            var response = await this.mediator.Send(command);
            if (!response)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
