//using Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace Domain.Common
//{
//    public string GenerateJwtToken(Users users)
//    {
//        var claims = new List<Claim>
//    {
//        new Claim(ClaimTypes.NameIdentifier, users.Id.ToString()),
//        new Claim(ClaimTypes.Email, users.Name),
//        // Add other claims like roles
//    };

//        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
//        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//        var token = new JwtSecurityToken(
//            issuer: _configuration["Jwt:Issuer"],
//            audience: _configuration["Jwt:Audience"],
//            claims: claims,
//            expires: DateTime.Now.AddDays(7),
//            signingCredentials: creds
//        );

//        return new JwtSecurityTokenHandler().WriteToken(token);
//    }
//}
