using Application.ModelDto.Request;
using Domain.Common;
using MediatR;


namespace Application.Features.Employee.Queries
{
    public record GetAllEmployeeQueries(PaginationParams paginationParams):IRequest<ApiResponse<PagedResponse<EmployeeDto>>>;
   
}
