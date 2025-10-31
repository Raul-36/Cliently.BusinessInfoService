using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.InfoTexts.Commands;
using Application.InfoTexts.DTOs.Requests;
using Application.InfoTexts.DTOs.Responses;
using Application.InfoTexts.Queries;
using Application.Common;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfoTextsController : ControllerBase
    {
        private IMediator mediator;

        public InfoTextsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(InfoTextResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateInfoText([FromBody] CreateInfoTextRequest request)
        {
            var command = new CreateInfoTextCommand { InfoText = request };
            var response = await this.mediator.Send(command);
            if (response.IsSuccess)
            {
                return CreatedAtAction(nameof(GetInfoTextById), new { id = response.Value!.Id }, response.Value);
            }
            return BadRequest(response.Errors);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InfoTextResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfoTextById(Guid id)
        {
            var query = new GetInfoTextByIdQuery { Id = id };
            var response = await this.mediator.Send(query);
            if (response.IsSuccess)
            {
                return Ok(response.Value);
            }
            return NotFound(response.Errors);
        }

        [HttpGet("ByBusinessId/{businessId}")]
        [ProducesResponseType(typeof(IEnumerable<InfoTextResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllInfoTextsByBusinessId(Guid businessId)
        {
            var query = new GetAllInfoTextsByBusinessIdQuery { BusinessId = businessId };
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
        public async Task<IActionResult> UpdateInfoText([FromBody] UpdateInfoTextRequest request)
        {
            var command = new UpdateInfoTextCommand { InfoText = request };
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
        public async Task<IActionResult> DeleteInfoText(Guid id)
        {
            var command = new DeleteInfoTextCommand { Id = id };
            var response = await this.mediator.Send(command);
            if (response.IsSuccess)
            {
                return NoContent();
            }
            return NotFound(response.Errors);
        }
    }
}
