﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using FakeAcad.Infrastructure.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Infrastructure.Services.Interfaces;
using FakeAcad.Core.Responses;
using FakeAcad.Infrastructure.HttpClients;
using FakeAcad.Core.Errors;

namespace FakeAcad.Infrastructure.Services.Implementations;

/// <summary>
/// Inject the required service configuration from the application.json or environment variables.
/// </summary>
public class LoginService(IOptions<JwtConfiguration> jwtConfiguration, UserHttpClient userHttpClient) : ILoginService
{
    private readonly JwtConfiguration _jwtConfiguration = jwtConfiguration.Value;

    public string GetToken(UserDTO user, DateTime issuedAt, TimeSpan expiresIn)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Key); // Use the configured key as the encryption key to sing the JWT.
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new([new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())]), // Set the user ID as the "nameid" claim in the JWT.
            Claims = new Dictionary<string, object> // Add any other claims in the JWT, you can even add custom claims if you want.
            {
                { ClaimTypes.Name, user.Name },
                { ClaimTypes.Email, user.Email }
            },
            IssuedAt = issuedAt, // This sets the "iat" claim to indicate then the JWT was emitted.
            Expires = issuedAt.Add(expiresIn), // This sets the "exp" claim to indicate when the JWT expires and cannot be used.
            Issuer = _jwtConfiguration.Issuer, // This sets the "iss" claim to indicate the authority that issued the JWT.
            Audience = _jwtConfiguration.Audience, // This sets the "aud" claim to indicate to which client the JWT is intended to.
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) // Sign the JWT, it will set the algorithm in the JWT header to "HS256" for HMAC with SHA256.
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)); // Create the token.
    }

    public async Task<ServiceResponse<LoginResponseDTO>> Login(LoginDTO login, CancellationToken cancellationToken = default)
    {
        var result = await userHttpClient.GetByEmailAsync(login.Email);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<LoginResponseDTO>(CommonErrors.UserNotFound);
        }

        var userEntity = result.Response;

        if (userEntity == null)
        {
            return ServiceResponse.FromError<LoginResponseDTO>(CommonErrors.UserNotFound);
        }

        if (userEntity.Password != login.Password)
        {
            return ServiceResponse.FromError<LoginResponseDTO>(CommonErrors.WrongPassword);
        }

        var user = new UserDTO
        {
            Id = userEntity.Id,
            Name = userEntity.Name,
            Email = userEntity.Email,
            Role = userEntity.Role
        };

        return ServiceResponse.ForSuccess(new LoginResponseDTO
        {
            User = user,
            Token = GetToken(user, DateTime.UtcNow, new(7, 0, 0, 0))
        });
    }
}
