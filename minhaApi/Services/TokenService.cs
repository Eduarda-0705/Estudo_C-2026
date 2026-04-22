using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MinhaApi.Services;

public class TokenService{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config){
        _config = config;
    }

    public string GenerateToken(string username){
        var key = Encoding.UTF8.GetBytes(_config["Jwt:key"]);

        var claims = new[]{
            new Claim(ClaimTypes.Name, username)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);    
    }
}