using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.InfoLists.Commands;
using Application.InfoLists.DTOs.Requests;
using Application.InfoLists.DTOs.Responses;
using Application.InfoLists.Queries;
using Application.InfoLists.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Presentation.Consts;
using System.Security.Claims;
using System;
using Presentation.Extensions;
using Core.Businesses.Entities;
using Application.Businesses.Exceptions;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InfoListsController : ControllerBase
    {
        private readonly IMediator mediator;

        public InfoListsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = DefaultRoles.User)]
        public async Task<IActionResult> CreateInfoList([FromBody] CreateInfoListRequest request)
        {
            try
            {
                request.UserId = this.GetUserId();
                var command = new CreateInfoListCommand { InfoList = request };
                var response = await mediator.Send(command);
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
        public async Task<IActionResult> GetInfoListById(Guid id)
        {
            try
            {
                var query = new GetInfoListByIdQuery { Id = id, UserId = this.GetUserId() }; 
                var response = await mediator.Send(query);
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

        [HttpGet("ByBusinessId/{businessId}")]
        public async Task<IActionResult> GetAllInfoListsByBusinessId(Guid businessId)
        {
            try
            {
                var query = new GetAllInfoListsByBusinessIdQuery { BusinessId = businessId, UserId = this.GetUserId() }; 
                var response = await mediator.Send(query);
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

        [HttpPatch("name")]
        public async Task<IActionResult> UpdateInfoListName([FromBody] UpdateInfoListRequest request)
        {
            try
            {
                request.UserId = this.GetUserId(); 
                var command = new UpdateInfoListNameCommand { InfoList = request };
                await mediator.Send(command);
                return NoContent();
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfoList(Guid id)
        {
            try
            {
                var command = new DeleteInfoListCommand { Id = id, UserId = this.GetUserId() }; 
                await mediator.Send(command);
                return NoContent();
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
    }
}
