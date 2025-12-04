using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.InfoTexts.Commands;
using Application.InfoTexts.DTOs.Requests;
using Application.InfoTexts.DTOs.Responses;
using Application.InfoTexts.Queries;
using Application.InfoTexts.Exceptions;
using Presentation.Extensions;
using Core.Businesses.Entities;
using Application.Businesses.Exceptions;


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
            try
            {
                request.UserId = this.GetUserId();
                var command = new CreateInfoTextCommand { InfoText = request };
                var response = await this.mediator.Send(command);
                return Ok(response);
            }
            catch (BusinessNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfoTextById(Guid id)
        {
            try
            {
                var query = new GetInfoTextByIdQuery { Id = id, UserId = this.GetUserId() };
                var response = await this.mediator.Send(query);
                return Ok(response);
            }
            catch (InfoTextNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("ByBusinessId/{businessId}")]
        public async Task<IActionResult> GetAllInfoTextsByBusinessId(Guid businessId)
        {
            try
            {
                var query = new GetAllInfoTextsByBusinessIdQuery { BusinessId = businessId, UserId = this.GetUserId() };
                var response = await this.mediator.Send(query);
                return Ok(response);
            }
            catch (BusinessNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInfoText([FromBody] UpdateInfoTextRequest request)
        {
            try
            {
                request.UserId = this.GetUserId();
                var command = new UpdateInfoTextCommand { InfoText = request };
                await this.mediator.Send(command);
                return NoContent();
            }
            catch (InfoTextNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfoText(Guid id)
        {
            try
            {
                var command = new DeleteInfoTextCommand { Id = id, UserId = this.GetUserId() };
                await this.mediator.Send(command);
                return NoContent();
            }
            catch (InfoTextNotFoundException ex)
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
