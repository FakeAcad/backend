using System.Net;
using FakeAcad.Core.Constants;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Entities;
using FakeAcad.Core.Enums;
using FakeAcad.Core.Errors;
using FakeAcad.Core.Requests;
using FakeAcad.Core.Responses;
using FakeAcad.Core.Specifications;
using FakeAcad.Infrastructure.HttpClients;
using FakeAcad.Infrastructure.Services.Interfaces;

namespace FakeAcad.Infrastructure.Services.Implementations;

/// <summary>
/// Inject the required services through the constructor.
/// </summary>
public class UserService(UserHttpClient userHttpClient, IMailService mailService)
    : IUserService
{
    public async Task<ServiceResponse<UserDTO>> GetUser(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await userHttpClient.GetByIdAsync(id);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<UserDTO>(result.ErrorMessage);
        }

        var user = result.Response;

        return user != null ?
            ServiceResponse.ForSuccess(user) :
            ServiceResponse.FromError<UserDTO>(CommonErrors.UserNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<UserDTO>>> GetUsers(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await userHttpClient.GetPageAsync(pagination);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<PagedResponse<UserDTO>>(result.ErrorMessage);
        }

        var users = result.Response;

        return users != null ?
            ServiceResponse.ForSuccess(users) :
            ServiceResponse.FromError<PagedResponse<UserDTO>>(CommonErrors.UserNotFound);
    }

    public async Task<ServiceResponse<int>> GetUserCount(CancellationToken cancellationToken = default)
    {
        var result = await userHttpClient.GetCountAsync();

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<int>(result.ErrorMessage);
        }

        var count = result.Response;

        return ServiceResponse.ForSuccess(count);
    }

    public async Task<ServiceResponse> AddUser(UserAddDTO user, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Moderator) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add the user!", ErrorCodes.CannotAdd));
        }

        var result = await userHttpClient.GetByEmailAsync(user.Email); // Check if the user already exists.

        if (result.Response != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The user already exists!", ErrorCodes.UserAlreadyExists));
        }

        var response = await userHttpClient.AddUserAsync(user); // Add the user.

        if (response.ErrorMessage != null)
        {
            return ServiceResponse.FromError(response.ErrorMessage);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateUser(UserUpdateDTO user, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Unauthorized, "You must be logged in to update the user!", ErrorCodes.Unauthorized));
        }

        if (requestingUser.Role != UserRoleEnum.Moderator && requestingUser.Id != user.Id)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can update the user!", ErrorCodes.CannotUpdate));
        }

        var result = await userHttpClient.GetByIdAsync(user.Id);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError(result.ErrorMessage);
        }

        var response = await userHttpClient.UpdateUserAsync(user);

        if (response.ErrorMessage != null)
        {
            return ServiceResponse.FromError(response.ErrorMessage);
        }

        await mailService.SendMail(requestingUser.Email, "User updated", $"The user {user.Name} has been updated!");

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteUser(Guid id, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Moderator && requestingUser.Id != id)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can delete the user!", ErrorCodes.CannotDelete));
        }

        var result = await userHttpClient.GetByIdAsync(id);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError(result.ErrorMessage);
        }

        var response = await userHttpClient.DeleteUserAsync(id);

        if (response.ErrorMessage != null)
        {
            return ServiceResponse.FromError(response.ErrorMessage);
        }

        if (requestingUser != null)
        {
            await mailService.SendMail(requestingUser.Email, "User deleted", $"The user {result.Response?.Name} has been deleted!");
        }

        return ServiceResponse.ForSuccess();
    }
}
