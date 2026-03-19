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
        [HttpPost("dh")]
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
    }
}
