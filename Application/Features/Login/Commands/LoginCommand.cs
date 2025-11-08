using Application.Interfaces;
using Application.ModelDto.Request;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Login.Commands
{
    public class LoginCommand : IRequest<Users> // returns User, not token
    {
        public LoginRequestDto LoginDto { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, Users>
        {
            private readonly IApplicationDbContext _context;

            public LoginCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Users> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Name == request.LoginDto.Name
                                           && u.Id == request.LoginDto.Id,
                                         cancellationToken);

                if (user == null)
                    throw new UnauthorizedAccessException("Invalid credentials");

                return user; // ✅ return user object, not token
            }
        }
    }
}
