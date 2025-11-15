using Application.ModelDto.Responce;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Department.Queries
{
    public record GetByIdDepartmentQueries(int Id) : IRequest<DepartmentDto>;
}
