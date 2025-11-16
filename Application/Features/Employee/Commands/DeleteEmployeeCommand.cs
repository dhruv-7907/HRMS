using MediatR;

namespace Application.Features.Employee.Commands
{
    public  record DeleteEmployeeCommand(int Id):IRequest<int>;
}
