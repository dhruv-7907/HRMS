using Application.Features.Login.Commands;
using Application.ModelDto.Request;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Middleware;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly GenerateJwtToken _jwt;

        public LoginController(IMediator mediator, GenerateJwtToken jwt)
        {
            _mediator = mediator;
            _jwt = jwt;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            try
            {
                var command = new LoginCommand { LoginDto = loginDto };
                var user = await _mediator.Send(command); // ✅ now returns user

                var token = _jwt.GenerateToken(user);     // ✅ token created in WebApi layer

                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
    }
}
