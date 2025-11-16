using Application.Features.Employee.Commands;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Employee.Handler
{
    
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IEmployee _employee;

        public CreateEmployeeCommandHandler(IEmployee employee)
        {
            _employee = employee;
        }

        public async Task<int> Handle(CreateEmployeeCommand request,CancellationToken cancellationToken)
        {
            var result = await _employee.Create(request.EmployeeDto);
            return result;
        }
    }
}
