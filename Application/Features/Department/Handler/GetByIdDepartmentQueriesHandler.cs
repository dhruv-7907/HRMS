using Application.Features.Department.Queries;
using Application.Features.Designations.Queries;
using Application.Interfaces;
using Application.ModelDto.Responce;
using Domain.Entities;
using Domain;
using MediatR;

namespace Application.Features.Department.Handler
{
    public class GetByIdDepartmentQueriesHandler(IDepartment department) : IRequestHandler<GetByIdDepartmentQueries,DepartmentDto>
    {
        private readonly IDepartment _department = department;

        public async Task<DepartmentDto> Handle(GetByIdDepartmentQueries request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
                throw new KeyNotFoundException("User");
            var result = await _department.GetById(request.Id);
            return result;
        }
    }
}
