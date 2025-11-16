using Application.ModelDto.Request;
using Application.ModelDto.Responce;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employee.Queries
{
    public record GetByIdEmployeeQueries(int Id) : IRequest<EmployeeDto>;
    
}
