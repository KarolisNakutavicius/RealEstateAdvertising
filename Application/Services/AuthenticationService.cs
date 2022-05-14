using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs;
using Application.DTOs.InputModels;
using Application.DTOs.ViewModels;
using Application.Services.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public AuthenticationService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<Result<AuthenticateResponse>> Login(AuthenticateRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Email);

        if (user == null) return Result<AuthenticateResponse>.Fail("User was not found");

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            return Result<AuthenticateResponse>.Fail("Incorrect password");

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            _configuration["JWT:ValidIssuer"],
            _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return Result<AuthenticateResponse>.Ok(
            new AuthenticateResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            });
    }

    public async Task<Result<AuthenticateResponse>> Register(AuthenticateRequest request)
    {
        var userExists = await _userManager.FindByNameAsync(request.Email);

        if (userExists != null) return Result<AuthenticateResponse>.Fail("User already exists");

        var user = new User
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return Result<AuthenticateResponse>.Fail(result.Errors.Select(e => e.Description).ToList());

        return await Login(request);
    }
}