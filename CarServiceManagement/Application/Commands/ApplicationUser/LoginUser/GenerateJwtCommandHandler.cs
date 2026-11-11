using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Commands.ApplicationUser.LoginUser;

public class GenerateJwtCommandHandler : IRequestHandler<GenerateJwtCommand,string>
{
    private readonly UserManager<Infrastructure.Entities.ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public GenerateJwtCommandHandler(UserManager<Infrastructure.Entities.ApplicationUser> userManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> Handle(GenerateJwtCommand request, CancellationToken cancellationToken)
    {
        var login = request.LoginRequest;

        if (string.IsNullOrWhiteSpace(login.UserName) || string.IsNullOrWhiteSpace(login.Password))
        {
            throw new UnauthorizedAccessException("Invalid Login or  password");
        }

        var user = await _userManager.FindByNameAsync(login.UserName);

        if (user is null || !await _userManager.CheckPasswordAsync(user, login.Password))
        {
            throw new UnauthorizedAccessException("Invalid Login or  password");
        }

        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Name, user.UserName!)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var jwt = _configuration.GetSection("JwtConfiguration");

        var secret = jwt["Secret"]
                     ?? throw new InvalidOperationException("Secret Key is missing");

        if (!int.TryParse(jwt["ExpireInMinutes"], out var minutes) || minutes <= 0)
        {
            throw new InvalidOperationException("Invalid Expire In Minutes");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(minutes),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}