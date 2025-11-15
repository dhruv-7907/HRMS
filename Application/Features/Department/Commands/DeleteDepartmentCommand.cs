using MediatR;

namespace Application.Features.Department.Commands
{
    public record DeleteDepartmentCommand(int Id):IRequest<int>;
    
}
