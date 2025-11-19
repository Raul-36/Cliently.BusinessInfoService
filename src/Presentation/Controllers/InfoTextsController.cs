using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.InfoTexts.Commands;
using Application.InfoTexts.DTOs.Requests;
using Application.InfoTexts.DTOs.Responses;
using Application.InfoTexts.Queries;
using Application.InfoTexts.Exceptions;


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
        public async Task<IActionResult> CreateInfoText([FromBody] CreateInfoTextRequest request)
        {
            var command = new CreateInfoTextCommand { InfoText = request };
            var response = await this.mediator.Send(command);
            return Ok(response);
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfoTextById(Guid id)
        {
            try
            {
                var query = new GetInfoTextByIdQuery { Id = id };
                var response = await this.mediator.Send(query);
                return Ok(response);
            }
            catch (InfoTextNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ByBusinessId/{businessId}")]
        public async Task<IActionResult> GetAllInfoTextsByBusinessId(Guid businessId)
        {
            var query = new GetAllInfoTextsByBusinessIdQuery { BusinessId = businessId };
            var response = await this.mediator.Send(query);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInfoText([FromBody] UpdateInfoTextRequest request)
        {
            try
            {
                var command = new UpdateInfoTextCommand { InfoText = request };
                await this.mediator.Send(command);
                return NoContent();
            }
            catch (InfoTextNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfoText(Guid id)
        {
            var command = new DeleteInfoTextCommand { Id = id };
            await this.mediator.Send(command);
            return NoContent();
        }
    }
}
