using Application.ModelDto.Responce;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDesignations
    {
        Task<int> CreateDesignations(DesignationsDto dto);
        Task<int> UpdateDesignations(DesignationsDto dto);
        Task<int> DeleteDesignations(int Id);
        Task<DesignationsDto> GetDesignationsById(int Id);
        Task<ApiResponse<PagedResponse<DesignationsDto>>> GetAllDesignations(PaginationParams pagination);
    }
}
