using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<Users?> GetByEmailAsync(string email);
        Task AddAsync(Users user);
        Task UpdateAsync(Users user);
        Task RefreshTokenAddAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task RefreshTokenUpdateAsync(RefreshToken token);
        //add
        Task<Users> GetByUser(int Id);
    }
}
