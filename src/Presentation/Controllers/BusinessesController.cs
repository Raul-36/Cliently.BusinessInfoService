using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Businesses.Commands;
using Application.Businesses.DTOs.Requests;
using Application.Businesses.DTOs.Responses;
using Application.Businesses.Queries;
using Application.Businesses.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Presentation.Consts;
using System.Security.Claims;
using Presentation.Extensions;



namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BusinessesController : ControllerBase
    {
        private readonly IMediator mediator;

        public BusinessesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = DefaultRoles.User)]
        public async Task<IActionResult> CreateBusiness([FromBody] CreateBusinessRequest request)
        {
            try
            {
                request.UserId = this.GetUserId();
                var command = new CreateBusinessCommand(request);
                var response = await this.mediator.Send(command);
                return Ok(response);
            }
            catch (UserAlreadyHasBusinessException ex)
            {
                return Conflict(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusinessById(Guid id)
        {
            try
            {
                var query = new GetBusinessByIdQuery{Id = id, UserId = this.GetUserId()};
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
        [HttpGet]
        [Authorize(Roles = DefaultRoles.Admin)]
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
                request.UserId = this.GetUserId();
                var command = new UpdateBusinessNameCommand { Business = request };
                await this.mediator.Send(command);
                return NoContent();
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusiness(Guid id)
        {
            try
            {
                var command = new DeleteBusinessCommand(){ Id = id, UserId = this.GetUserId() };
                await this.mediator.Send(command);
                return NoContent();
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
    }
}
