using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RefreshTokenRepository : IRefreshToken
    {
        public Task AddAsync(RefreshToken token)
        {
            throw new NotImplementedException();
        }

        public string GenerateToken()
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken?> GetByTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(RefreshToken token)
        {
            throw new NotImplementedException();
        }
    }
}
