using Application.Features.Designations.Queries;
using Application.Features.Employee.Queries;
using Application.Interfaces;
using Application.ModelDto.Request;
using Application.ModelDto.Responce;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employee.Handler
{

    public class GetByIdEmployeeQueriesHandler : IRequestHandler<GetByIdEmployeeQueries, EmployeeDto>
    {
        private readonly IEmployee _EmployeeRepository;

        public GetByIdEmployeeQueriesHandler(IEmployee EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
        }

        public async Task<EmployeeDto> Handle(GetByIdEmployeeQueries request, CancellationToken cancellationToken)
        {
            var result = await _EmployeeRepository.GetById(request.Id);
            return result;
        }


    }
}
