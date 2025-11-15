using Application.Features.Department.Queries;
using Application.Interfaces;
using Application.ModelDto.Responce;
using Domain.Common;
using MediatR;

namespace Application.Features.Department.Handler
{
    public class GetAllDepartmentQueriesHandler : IRequestHandler<GetAllDepartmentQueries, ApiResponse<PagedResponse<DepartmentDto>>>
    {
        private readonly IDepartment _department;

        public GetAllDepartmentQueriesHandler(IDepartment department)
        {
            _department = department;
        }

        public async Task<ApiResponse<PagedResponse<DepartmentDto>>> Handle(GetAllDepartmentQueries request, CancellationToken cancellationToken)
        {
            var result = await _department.GetAll(request.PaginationParams);
            return result;
        }
    }
}
