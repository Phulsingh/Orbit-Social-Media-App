using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orbit.Application.Features.Family.Commands;
using Orbit.Application.Features.Family.Queries;
using System.Security.Claims;

namespace Orbit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FamilyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FamilyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private int GetUserId() =>
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        // POST api/family/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateFamily([FromBody] CreateFamilyCommand command)
        {
            try
            {
                command.UserId = GetUserId();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/family/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFamily(int id)
        {
            try
            {
                var query = new GetFamilyQuery { FamilyId = id, UserId = GetUserId() };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }

        // GET api/family/{id}/members
        [HttpGet("{id}/members")]
        public async Task<IActionResult> GetFamilyMembers(int id)
        {
            try
            {
                var query = new GetMembersQuery { FamilyId = id, UserId = GetUserId() };
                var result = await _mediator.Send(query);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        // POST api/family/{id}/invite
        [HttpPost("{id}/invite")]
        public async Task<IActionResult> GenerateInviteCode(int id)
        {
            try
            {
                var command = new GenerateInviteCodeCommand { FamilyId = id, AdminUserId = GetUserId() };
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST api/family/join
        [HttpPost("join")]
        public async Task<IActionResult> JoinFamily([FromBody] JoinFamilyCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST api/family/approve-member
        [HttpPost("approve-member")]
        public async Task<IActionResult> ApproveMember(
            [FromBody] ApproveMemberCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { message = "Member approved successfully", success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // POST api/family/remove-member
        [HttpPost("remove-member")]
        public async Task<IActionResult> RemoveMember(
            [FromBody] RemoveMemberCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { message = "Member removed successfully", success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/family/all
        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllFamily()
        {
            try
            {
                var result = await _mediator.Send(new GetAllFamilyQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
    }
}
