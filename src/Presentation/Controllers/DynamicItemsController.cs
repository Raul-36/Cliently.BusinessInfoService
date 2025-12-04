using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DynamicItems.Commands;
using Application.DynamicItems.DTOs.Requests;
using Application.DynamicItems.DTOs.Responses;
using Application.DynamicItems.Queries;
using Application.DynamicItems.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Presentation.Consts;
using Presentation.Extensions;
using Application.InfoLists.Exceptions;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = DefaultRoles.User)]
    public class DynamicItemsController : ControllerBase
    {
        private readonly IMediator mediator;

        public DynamicItemsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDynamicItem([FromBody] CreateDynamicItemRequest request)
        {
            try
            {
                request.UserId = this.GetUserId();
                var command = new CreateDynamicItemCommand { DynamicItem = request };
                var response = await this.mediator.Send(command);
                return Ok(response);
            }
            catch (InfoListNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDynamicItemById(Guid id)
        {
            try
            {
                var query = new GetDynamicItemByIdQuery { Id = id, UserId = this.GetUserId() };
                var response = await this.mediator.Send(query);
                return Ok(response);
            }
            catch (DynamicItemNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("ByList/{listId}")]
        public async Task<IActionResult> GetAllDynamicItemsByListId(Guid listId)
        {
            try
            {
                var query = new GetAllDynamicItemsByListIdQuery { ListId = listId, UserId = this.GetUserId() };
                var response = await this.mediator.Send(query);
                return Ok(response);
            }
            catch (InfoListNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDynamicItem([FromBody] UpdateDynamicItemRequest request)
        {
            try
            {
                request.UserId = this.GetUserId();
                var command = new UpdateDynamicItemCommand { DynamicItem = request };
                await this.mediator.Send(command);
                return NoContent();
            }
            catch (DynamicItemNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDynamicItem(Guid id)
        {
            try
            {
                var command = new DeleteDynamicItemCommand { Id = id, UserId = this.GetUserId() };
                await this.mediator.Send(command);
                return NoContent();
            }
            catch (DynamicItemNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
