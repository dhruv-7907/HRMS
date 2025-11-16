using Application.ModelDto.Request;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEmployee
    {
        Task<ApiResponse<PagedResponse<EmployeeDto>>> GetAll(PaginationParams pagination);
        Task<EmployeeDto> GetById(int Id);
        Task<int> Create(EmployeeDto employeeDto);
        Task<int> Update(EmployeeDto employeeDto);
        Task<int> Delete(int Id);
    }
}
