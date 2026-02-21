using Application.ModelDto.Request;
using MediatR;


namespace Application.Features.Employee.Commands
{
    public record UpdateEmployeeCommand(EmployeeDto employeeDto):IRequest<int>;
}
