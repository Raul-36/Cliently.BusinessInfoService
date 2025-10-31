using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DynamicItems.Commands;
using Application.DynamicItems.DTOs.Requests;
using Application.DynamicItems.DTOs.Responses;
using Application.DynamicItems.Queries;
using Application.Common;

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
            if (response.IsSuccess)
            {
                return CreatedAtAction(nameof(GetDynamicItemById), new { id = response.Value!.Id }, response.Value);
            }
            return BadRequest(response.Errors);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DynamicItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDynamicItemById(Guid id)
        {
            var query = new GetDynamicItemByIdQuery { Id = id };
            var response = await this.mediator.Send(query);
            if (response.IsSuccess)
            {
                return Ok(response.Value);
            }
            return NotFound(response.Errors);
        }

        [HttpGet("ByList/{listId}")]
        [ProducesResponseType(typeof(IEnumerable<DynamicItemResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllDynamicItemsByListId(Guid listId)
        {
            var query = new GetAllDynamicItemsByListIdQuery { ListId = listId };
            var response = await this.mediator.Send(query);
            if (response.IsSuccess)
            {
                return Ok(response.Value);
            }
            return BadRequest(response.Errors);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDynamicItem([FromBody] UpdateDynamicItemRequest request)
        {
            var command = new UpdateDynamicItemCommand { DynamicItem = request };
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
        public async Task<IActionResult> DeleteDynamicItem(Guid id)
        {
            var command = new DeleteDynamicItemCommand { Id = id };
            var response = await this.mediator.Send(command);
            if (response.IsSuccess)
            {
                return NoContent();
            }
            return NotFound(response.Errors);
        }
    }
}
