using Application.Features.Department.Commands;
using Application.Features.Department.Queries;
using Application.Features.Employee.Commands;
using Application.Features.Employee.Queries;
using Application.ModelDto.Request;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateEmployeeCommand(employeeDto), cancellationToken);
            return Ok(result);
            
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateEmployeeCommand(employeeDto) , cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var command = new DeleteEmployeeCommand(Id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("/employees")]
        public async Task<IActionResult> GetAll([FromBody] PaginationParams paginationParams, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllEmployeeQueries(paginationParams), cancellationToken);
            return Ok(result);
            
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int Id, CancellationToken cancellationToken)
        {
            var command = new GetByIdEmployeeQueries(Id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}
