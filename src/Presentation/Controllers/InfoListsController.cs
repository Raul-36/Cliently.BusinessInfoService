using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.InfoLists.Commands;
using Application.InfoLists.DTOs.Requests;
using Application.InfoLists.DTOs.Responses;
using Application.InfoLists.Queries;
using Application.InfoLists.Exceptions;


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
        public async Task<IActionResult> CreateInfoList([FromBody] CreateInfoListRequest request)
        {
            var command = new CreateInfoListCommand { InfoList = request };
            var response = await this.mediator.Send(command);
            return Ok(response);
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfoListById(Guid id)
        {
            try
            {
                var query = new GetInfoListByIdQuery { Id = id };
                var response = await this.mediator.Send(query);
                return Ok(response);
            }
            catch (InfoListNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ByBusinessId/{businessId}")]
        public async Task<IActionResult> GetAllInfoListsByBusinessId(Guid businessId)
        {
            var query = new GetAllInfoListsByBusinessIdQuery { BusinessId = businessId };
            var response = await this.mediator.Send(query);
            return Ok(response);
            
        }

        [HttpPatch("name")]
        public async Task<IActionResult> UpdateInfoListName([FromBody] UpdateInfoListNameRequest request)
        {
            
            try
            {
                var command = new UpdateInfoListNameCommand { InfoList = request };
                await this.mediator.Send(command);
                return NoContent();
            }
            catch (InfoListNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfoList(Guid id)
        {
            var command = new DeleteInfoListCommand { Id = id };
            await this.mediator.Send(command);
            return NoContent();
        }
    }
}
