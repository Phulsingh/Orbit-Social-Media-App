using Orbit.Application.Features.Auth.Command;
using Orbit.Application.Features.Auth.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Orbit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender _mediator;

        public AuthController(ISender mediator) 
        {
            _mediator = mediator;
        }

        //Post api/Auth/Register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
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

        //Post api/Auth/Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
