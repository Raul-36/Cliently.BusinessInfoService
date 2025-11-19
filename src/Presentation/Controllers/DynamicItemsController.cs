using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DynamicItems.Commands;
using Application.DynamicItems.DTOs.Requests;
using Application.DynamicItems.DTOs.Responses;
using Application.DynamicItems.Queries;
using Application.DynamicItems.Exceptions;


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
        public async Task<IActionResult> CreateDynamicItem([FromBody] CreateDynamicItemRequest request)
        {
            var command = new CreateDynamicItemCommand { DynamicItem = request };
            var response = await this.mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDynamicItemById(Guid id)
        {
            try
            {
                var query = new GetDynamicItemByIdQuery { Id = id };
                var response = await this.mediator.Send(query);
                return Ok(response);
            }
            catch (DynamicItemNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ByList/{listId}")]
        public async Task<IActionResult> GetAllDynamicItemsByListId(Guid listId)
        {
            var query = new GetAllDynamicItemsByListIdQuery { ListId = listId };
            var response = await this.mediator.Send(query);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDynamicItem([FromBody] UpdateDynamicItemRequest request)
        {
            try
            {
                var command = new UpdateDynamicItemCommand { DynamicItem = request };
                await this.mediator.Send(command);
                return NoContent();
            }
            catch (DynamicItemNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDynamicItem(Guid id)
        {
            var command = new DeleteDynamicItemCommand { Id = id };
            await this.mediator.Send(command);
            return NoContent();
            
        }
    }
}
