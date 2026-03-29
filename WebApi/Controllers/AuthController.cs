using Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //
        [HttpPost("Register")]
        public async Task<IActionResult> Register(
            RegisterUserCommand command)
        {
            var userId = await _mediator.Send(command);

            return Ok(new
            {
                Message = "User registered successfully",
                UserId = userId
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
