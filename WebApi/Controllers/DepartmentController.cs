using Application.Features.Department.Commands;
using Application.Features.Department.Queries;
using Application.Features.Designations.Commands;
using Application.Features.Designations.Queries;
using Application.ModelDto.Request;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Controllers
{
    [Route("/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentDto department, CancellationToken cancellationToken)
        {
            var command = new CreateDepartmentCommand(department);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(DepartmentDto DepartmentDto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateDepartmentCommand(DepartmentDto), cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var command = new DeleteDepartmentCommand(Id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("/departments")]
        public async Task<IActionResult> GetAll(PaginationParams PaginationParams, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllDepartmentQueries(PaginationParams), cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int Id, CancellationToken cancellationToken)
        {
            var command = new GetByIdDepartmentQueries(Id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet("/departments")]
        public async Task<IActionResult> GetDepartments()
        {
            var command = new GetAllDepartmentForDropdownQueries();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
