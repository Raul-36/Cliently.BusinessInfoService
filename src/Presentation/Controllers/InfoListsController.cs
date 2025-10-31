using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.InfoLists.Commands;
using Application.InfoLists.DTOs.Requests;
using Application.InfoLists.DTOs.Responses;
using Application.InfoLists.Queries;
using Application.Common;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfoListsController : ControllerBase
    {
        private IMediator mediator;

        public InfoListsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(InfoListResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateInfoList([FromBody] CreateInfoListRequest request)
        {
            var command = new CreateInfoListCommand { InfoList = request };
            var response = await this.mediator.Send(command);
            if (response.IsSuccess)
            {
                return CreatedAtAction(nameof(GetInfoListById), new { id = response.Value!.Id }, response.Value);
            }
            return BadRequest(response.Errors);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InfoListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfoListById(Guid id)
        {
            var query = new GetInfoListByIdQuery { Id = id };
            var response = await this.mediator.Send(query);
            if (response.IsSuccess)
            {
                return Ok(response.Value);
            }
            return NotFound(response.Errors);
        }

        [HttpGet("ByBusinessId/{businessId}")]
        [ProducesResponseType(typeof(IEnumerable<InfoListResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllInfoListsByBusinessId(Guid businessId)
        {
            var query = new GetAllInfoListsByBusinessIdQuery { BusinessId = businessId };
            var response = await this.mediator.Send(query);
            if (response.IsSuccess)
            {
                return Ok(response.Value);
            }
            return BadRequest(response.Errors);
        }

        [HttpPatch("name")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateInfoListName([FromBody] UpdateInfoListNameRequest request)
        {
            
            var command = new UpdateInfoListNameCommand { InfoList = request };
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
        public async Task<IActionResult> DeleteInfoList(Guid id)
        {
            var command = new DeleteInfoListCommand { Id = id };
            var response = await this.mediator.Send(command);
            if (response.IsSuccess)
            {
                return NoContent();
            }
            return NotFound(response.Errors);
        }
    }
}
