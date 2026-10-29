using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims; 
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartOffice.Application.Interfaces.Auth;
using SmartOffice.Domain.Entities;

namespace SmartOffice.Infrastructure.Auth;
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
    private readonly JwtSettings _settings;

    public JwtTokenGenerator(IOptions<JwtSettings> options)
    {
        _settings = options.Value;
    }
    public (string Token, DateTime ExpiresAt) Generate(User user)
    {
        var claims = new List<Claim>
        {
            new("sub",user.Id.ToString()),
            new("name",user.UserName),
            new("email",user.Email),
            new("role",user.Role.Name),
            new("jti",Guid.NewGuid().ToString()),
        };

        if(user.EmployeeId is { } employeeId)
            claims.Add(new Claim("employeeId",employeeId.ToString()));

        claims.AddRange(user.Role.Permissions.Select(p => new Claim("permission", p.Name)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresAt = DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiresAt,
            signingCredentials: credentials);

        return (new JwtSecurityTokenHandler().WriteToken(token),expiresAt);
    }
    }
 