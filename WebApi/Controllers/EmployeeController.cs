using Application.Features.Department.Commands;
using Application.Features.Department.Queries;
using Application.Features.Employee.Commands;
using Application.Features.Employee.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var command = new DeleteEmployeeCommand(Id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("/employees")]
        public async Task<IActionResult> GetAll([FromBody] GetAllEmployeeQueries command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet("/employee")]
        public async Task<IActionResult> GetById(int Id, CancellationToken cancellationToken)
        {
            var command = new GetByIdEmployeeQueries(Id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}
