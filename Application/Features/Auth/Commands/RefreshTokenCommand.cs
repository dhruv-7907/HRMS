using Application.ModelDto.Responce;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<LoginResponse>
    {
        public string RefreshToken { get; set; } = default!;
    }
}
