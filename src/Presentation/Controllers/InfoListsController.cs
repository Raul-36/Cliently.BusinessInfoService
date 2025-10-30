using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.InfoLists.Commands;
using Application.InfoLists.DTOs.Requests;
using Application.InfoLists.DTOs.Responses;
using Application.InfoLists.Queries;

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
            return CreatedAtAction(nameof(GetInfoListById), new { id = response.Id }, response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InfoListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfoListById(Guid id)
        {
            var query = new GetInfoListByIdQuery { Id = id };
            var response = await this.mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("ByBusinessId/{businessId}")]
        [ProducesResponseType(typeof(IEnumerable<InfoListResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllInfoListsByBusinessId(Guid businessId)
        {
            var query = new GetAllInfoListsByBusinessIdQuery { BusinessId = businessId };
            var response = await this.mediator.Send(query);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateInfoListName(Guid id, [FromBody] UpdateInfoListRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest("ID mismatch");
            }
            var command = new UpdateInfoListNameCommand { InfoList = request };
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
        public async Task<IActionResult> DeleteInfoList(Guid id)
        {
            var command = new DeleteInfoListCommand { Id = id };
            var response = await this.mediator.Send(command);
            if (!response)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
