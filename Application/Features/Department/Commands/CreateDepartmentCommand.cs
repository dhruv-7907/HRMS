using Application.ModelDto.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Department.Commands
{

    public record CreateDepartmentCommand(DepartmentDto Department) : IRequest<int>;
}
