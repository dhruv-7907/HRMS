using Application.Features.Auth.Commands;
using Application.Interfaces;
using Application.ModelDto.Responce;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Handlers
{
    public class LoginUserCommandHandler
    : IRequestHandler<LoginUserCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtToken;
        private readonly IUserRepository _refreshTokenRepository;
        private readonly IRefreshToken _refreshTokenService;

        public LoginUserCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtToken,
            IUserRepository refreshTokenRepository,
            IRefreshToken refreshTokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtToken = jwtToken;
            _refreshTokenRepository = refreshTokenRepository;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<LoginResponse> Handle(
            LoginUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                throw new Exception("Invalid credentials");

            // 🔒 Lockout check
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.UtcNow)
                throw new Exception("Account locked. Try later.");

            // 🔐 FIX: use PasswordHash
            var isValid = _passwordHasher.VerifyPassword(
                user.Password,
                request.Password);

            if (!isValid)
            {
                user.FailedLoginAttempts++;

                if (user.FailedLoginAttempts >= 5)
                    user.LockoutEnd = DateTime.UtcNow.AddMinutes(15);

                await _userRepository.UpdateAsync(user);

                throw new Exception("Invalid credentials");
            }

            // ✅ Reset
            user.FailedLoginAttempts = 0;
            user.LockoutEnd = null;
            user.LastLoginAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);

            // 🔐 Access Token
            var accessToken = _jwtToken.GenerateToken(user);

            // 🔄 Refresh Token
            var refreshToken = _refreshTokenService.GenerateToken();

            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };

            await _refreshTokenRepository.RefreshTokenAddAsync(refreshTokenEntity);

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(15)
            };
        }
    }
}
