using Application.Features.Designations.Commands;
using Application.Features.Designations.Queries;
using Application.ModelDto.Responce;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Controllers
{
    [Route("designation")]
    [ApiController]
    public class DesignationsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DesignationsDto designation, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateDesignationsCommand(designation), cancellationToken);
            return Ok(result);
           
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DesignationsDto designation, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateDesignationsCommand(designation), cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var command = new DeleteDesignationsCommand(Id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("/designations")]
        public async Task<IActionResult> GetAll([FromBody] PaginationParams PaginationParams, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllDesignationsQueries(PaginationParams), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById (int id, CancellationToken cancellationToken)
        {
            var command = new GetByIdDesignationsQueries(id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }


    }
}
