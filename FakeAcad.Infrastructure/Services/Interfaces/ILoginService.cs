﻿using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Responses;

namespace FakeAcad.Infrastructure.Services.Interfaces;

/// <summary>
/// This service is used to emit a JWT token.
/// </summary>
public interface ILoginService
{
    /// <summary>
    /// GetToken returns a JWT token string for a user with an issue date and and expiration interval after issue.
    /// </summary>
    public string GetToken(UserDTO user, DateTime issuedAt, TimeSpan expiresIn);

    public Task<ServiceResponse<LoginResponseDTO>> Login(LoginDTO login, CancellationToken cancellationToken = default);
}
