using Application.Features.Designations.Commands;
using Application.Features.Employee.Commands;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employee.Handler
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, int>
    {
        private readonly IEmployee _EmployeeRepository;

        public UpdateEmployeeCommandHandler(IEmployee EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
        }

        public async Task<int> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Pass DTO to repository for data saving
            var result = await _EmployeeRepository.Update(request.EmployeeDto);
            return result;
        }
    }
 }
