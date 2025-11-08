using Application.ModelDto.Responce;
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
    }
}
