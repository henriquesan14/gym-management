using GymManagementSystem.Application.Auth;
using GymManagementSystem.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GymManagementSystem.Infra.Services;

public class TokenService(IConfiguration _configuration) : ITokenService
{
    public TokenResponse GenerateAccessToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["TokenSettings:Secret"]!);
        var accessTokenExpiration = DateTime.Now.AddMinutes(15);
        var refreshTokenExpiration = DateTime.Now.AddDays(7);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            NotBefore = DateTime.Now,
            Expires = accessTokenExpiration,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);

        var refreshToken = Guid.NewGuid().ToString();

        return new TokenResponse(accessToken, refreshToken, accessTokenExpiration, refreshTokenExpiration);
    }
}
