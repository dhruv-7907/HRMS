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

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, int>
    {
        private readonly IEmployee _EmployeeRepository;

        public DeleteEmployeeCommandHandler(IEmployee EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
        }

        public async Task<int> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var result = await _EmployeeRepository.Delete(request.Id);
            return result;
        }
    }
}
