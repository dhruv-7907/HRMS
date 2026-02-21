using Application.Features.Designations.Queries;
using Application.Features.Employee.Queries;
using Application.Interfaces;
using Application.ModelDto.Request;
using Domain.Common;
using MediatR;


namespace Application.Features.Employee.Handler
{
    public class GetAllEmployeeQueriesHandler : IRequestHandler<GetAllEmployeeQueries, ApiResponse<PagedResponse<EmployeeDto>>>
    {
        private readonly IEmployee _EmployeeRepository;

        public GetAllEmployeeQueriesHandler(IEmployee EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
        }

        public async Task<ApiResponse<PagedResponse<EmployeeDto>>> Handle(GetAllEmployeeQueries request, CancellationToken cancellationToken)
        {
            var result = await _EmployeeRepository.GetAll(request.paginationParams);
            return result;
        }
    }
}
