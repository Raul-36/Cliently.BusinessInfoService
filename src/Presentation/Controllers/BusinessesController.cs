using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Businesses.Commands;
using Application.Businesses.DTOs.Requests;
using Application.Businesses.DTOs.Responses;
using Application.Businesses.Queries;
using Application.Businesses.Exceptions;



namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessesController : ControllerBase
    {
        private IMediator mediator;

        public BusinessesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBusiness([FromBody] CreateBusinessRequest request)
        {
            var command = new CreateBusinessCommand(request);
            var response = await this.mediator.Send(command);
            return Ok(response);
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusinessById(Guid id)
        {
            try
            {
                var query = new GetBusinessByIdQuery(id);
                var response = await this.mediator.Send(query);
                return Ok(response);
            }
            catch (BusinessNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBusinesses()
        {
            
            var query = new GetAllBusinessesQuery();
            var response = await this.mediator.Send(query);
            return Ok(response);
            
        }

        [HttpPatch("name")]
        public async Task<IActionResult> UpdateBusinessName([FromBody] UpdateBusinessRequest request)
        {
            try
            {
                var command = new UpdateBusinessNameCommand { Business = request };
                await this.mediator.Send(command);
                return NoContent();
            }
            catch (BusinessNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusiness(Guid id)
        {
            try
            {
                var command = new DeleteBusinessCommand(){ Id = id };
                await this.mediator.Send(command);
                return NoContent();
            }
            catch (BusinessNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
