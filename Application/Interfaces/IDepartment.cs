using Domain.Common;
using DepartmentDtoRequest = Application.ModelDto.Request.DepartmentDto;
using DepartmentDtoResponce = Application.ModelDto.Responce.DepartmentDto;

namespace Application.Interfaces
{
    public interface IDepartment
    {
     Task<ApiResponse<PagedResponse<DepartmentDtoResponce>>> GetAll(PaginationParams pagination);
     Task<DepartmentDtoResponce> GetById(int Id);
     Task<int>Create(DepartmentDtoRequest department);
     Task<int>Update(DepartmentDtoRequest department);
     Task<int>Delete(int Id);
     Task<IEnumerable<DepartmentDtoResponce>> GetDepartments();
    }
}
