using Application.ModelDto.Responce;
using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Designations.Queries
{
   public record GetAllDesignationsQueries(PaginationParams PaginationParams):IRequest<ApiResponse<PagedResponse<DepartmentDto>>>;
}
