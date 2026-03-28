using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orbit.Application.DTOs.Users;
using Orbit.Application.Features.Users.Commands.UploadProfileImage;
using System.Security.Claims;


namespace Orbit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload-profile-image")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadProfileImage(
        [FromForm] UploadProfileImageRequest request)
        {
            // ✅ Use request.File (NOT separate file param)
            if (request.File == null || request.File.Length == 0)
                return BadRequest(new { message = "No file uploaded." });

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized(new { message = "User ID not found in token." });

            if (!int.TryParse(userIdClaim, out var userId))
                return BadRequest(new { message = "Invalid user ID in token." });

            try
            {
                using var stream = request.File.OpenReadStream();

                var command = new UploadProfileImageCommand
                {
                    UserId = userId,
                    FileStream = stream,
                    FileName = request.File.FileName,
                    ContentType = request.File.ContentType
                };

                var url = await _mediator.Send(command);

                return Ok(new
                {
                    message = "Profile image uploaded successfully",
                    profileUrl = url
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "User ID not found in token." });
            }

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return BadRequest(new { message = "Invalid user ID in token." });
            }

            try
            {
                var result = await _mediator.Send(new GetUserProfileQuery
                {
                    UserId = userId
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
