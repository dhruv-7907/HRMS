using Application.Features.Auth.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Handlers
{
    public class RegisterUserCommandHandler
     : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<int> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            // 1️⃣ Check if email already exists
            var existingUser = await _userRepository
                .GetByEmailAsync(request.Email);

            if (existingUser != null)
                throw new Exception("Email already registered.");

            // 2️⃣ Hash password
            var passwordHash = _passwordHasher.HashPassword(request.Password);

            // 3️⃣ Create user
            var user = new Users
            {
                Name = request.Name,
                Email = request.Email,
                Password = passwordHash,
                RoleId = request.RoleId,
                //IsActive = true,
                CreatedOn = DateTime.UtcNow
            };

            // 4️⃣ Save
            await _userRepository.AddAsync(user);

            return user.Id;
        }
    }
}
