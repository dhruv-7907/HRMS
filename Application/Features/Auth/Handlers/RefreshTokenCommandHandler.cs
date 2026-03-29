using Application.Features.Auth.Commands;
using Application.Interfaces;
using Application.ModelDto.Responce;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Handlers
{
    public class RefreshTokenCommandHandler
     : IRequestHandler<RefreshTokenCommand, LoginResponse>
    {
        private readonly IUserRepository _refreshTokenRepo;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshToken _refreshTokenService;

        public RefreshTokenCommandHandler(
            IUserRepository refreshTokenRepo,
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshToken refreshTokenService)
        {
            _refreshTokenRepo = refreshTokenRepo;
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<LoginResponse> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            // 1️⃣ Get token
            var existingToken = await _refreshTokenRepo
                .GetByTokenAsync(request.RefreshToken);

            if (existingToken == null)
                throw new Exception("Invalid refresh token");

            // 2️⃣ Check revoked
            if (existingToken.RevokedAt != null)
                throw new Exception("Token already used");

            // 3️⃣ Check expiry
            if (existingToken.ExpiresAt < DateTime.UtcNow)
                throw new Exception("Token expired");

            //var user = existingToken.Users; // 🔥 FIX

            // 4️⃣ Rotate token
            existingToken.RevokedAt = DateTime.UtcNow;

            var newRefreshToken = _refreshTokenService.GenerateToken();

            existingToken.ReplacedByToken = newRefreshToken;

            await _refreshTokenRepo.RefreshTokenUpdateAsync(existingToken);

            // 5️⃣ Save new token
            var newToken = new RefreshToken

            {
                UserId = existingToken.UserId,
                Token = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };

         await _refreshTokenRepo.RefreshTokenAddAsync(newToken);
         var user = await _refreshTokenRepo.GetByUser(existingToken.UserId);

            // 6️⃣ Generate new access token
            var newAccessToken = _jwtTokenGenerator.GenerateToken(user);

            return new LoginResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(15)
            };
        }
    }
}
