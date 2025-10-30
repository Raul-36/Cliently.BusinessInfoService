using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DynamicItems.Commands;
using Application.DynamicItems.DTOs.Requests;
using Application.DynamicItems.DTOs.Responses;
using Application.DynamicItems.Queries;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DynamicItemsController : ControllerBase
    {
        private IMediator mediator;

        public DynamicItemsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DynamicItemResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDynamicItem([FromBody] CreateDynamicItemRequest request)
        {
            var command = new CreateDynamicItemCommand { DynamicItem = request };
            var response = await this.mediator.Send(command);
            return CreatedAtAction(nameof(GetDynamicItemById), new { id = response.Id }, response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DynamicItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDynamicItemById(Guid id)
        {
            var query = new GetDynamicItemByIdQuery { Id = id };
            var response = await this.mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("ByList/{listId}")]
        [ProducesResponseType(typeof(IEnumerable<DynamicItemResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDynamicItemsByListId(Guid listId)
        {
            var query = new GetAllDynamicItemsByListIdQuery { ListId = listId };
            var response = await this.mediator.Send(query);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDynamicItem(Guid id, [FromBody] UpdateDynamicItemRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest("ID mismatch");
            }
            var command = new UpdateDynamicItemCommand { DynamicItem = request };
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
        public async Task<IActionResult> DeleteDynamicItem(Guid id)
        {
            var command = new DeleteDynamicItemCommand { Id = id };
            var response = await this.mediator.Send(command);
            if (!response)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
