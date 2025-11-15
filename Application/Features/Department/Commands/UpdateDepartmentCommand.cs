using Application.ModelDto.Request;
using MediatR;

namespace Application.Features.Department.Commands
{
    public record UpdateDepartmentCommand(DepartmentDto DepartmentDto):IRequest<int>;
}
