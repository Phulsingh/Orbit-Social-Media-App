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
                command.UserId = GetUserId();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/family/{id}/approve/{memberId}
        [HttpPut("{id}/approve/{memberId}")]
        public async Task<IActionResult> ApproveMember(int id, int memberId)
        {
            try
            {
                var command = new ApproveMemberCommand
                {
                    FamilyId = id,
                    MemberId = memberId,
                    AdminUserId = GetUserId()
                };
                var result = await _mediator.Send(command);
                return Ok(new { message = "Member approved successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
