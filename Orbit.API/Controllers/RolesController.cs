using Orbit.Application.Features.Roles.Commands;
using Orbit.Application.Features.Roles.Commands.UpdateRole;
using Orbit.Application.Features.Roles.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Orbit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var roles = await _mediator.Send(new GetRolesQuery());
            return Ok(roles);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _mediator.Send(new GetRoleByIdQuery(id));
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddRole(CreateRoleCommand command)
        {
            var role = await _mediator.Send(command);
            return Ok(role);
            
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, UpdateRoleCommand command)
        {
            command = command with { Id = id };

            var role = await _mediator.Send(command);
            if(role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role  = await _mediator.Send(new DeleteRoleCommand(id));
            if (role == null)
            {
                return NotFound();
            }

            return Ok("Role deleted successfully");
        }

    }
}
