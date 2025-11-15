using Application.Features.Department.Queries;
using Application.Features.Designations.Queries;
using Application.Interfaces;
using Application.ModelDto.Responce;
using MediatR;

namespace Application.Features.Department.Handler
{
    public class GetByIdDepartmentQueriesHandler : IRequestHandler<GetByIdDepartmentQueries,DepartmentDto>
    {
        private readonly IDepartment _department;

        public GetByIdDepartmentQueriesHandler(IDepartment department)
        {
            _department = department;
        }
        public async Task<DepartmentDto> Handle(GetByIdDepartmentQueries request, CancellationToken cancellationToken)
        {
            var result = await _department.GetById(request.Id);
            return result;
        }
    }
}
