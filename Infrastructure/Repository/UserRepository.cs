using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _context;

        public UserRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Users?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task AddAsync(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Users user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task RefreshTokenAddAsync(RefreshToken refreshToken)
        {
            await _context.RefreshToken.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshToken
       .FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task RefreshTokenUpdateAsync(RefreshToken token)
        {
            _context.RefreshToken.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task<Users> GetByUser(int Id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}
