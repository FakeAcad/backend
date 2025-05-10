using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Requests;
using FakeAcad.Core.Responses;
using FakeAcad.Infrastructure.Database;
using FakeAcad.Infrastructure.Repositories.Interfaces;
using FakeAcad.Core.Handlers;
using FakeAcad.Core.Specifications;
using FakeAcad.Core.Errors;
using FakeAcad.Core.Entities;

namespace FakeAcad.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController(IRepository<WebAppDatabaseContext> repository) : BaseResponseController
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<UserDTO>>> GetById([FromRoute] Guid id)
    {
        var result = await repository.GetAsync(new UserProjectionSpec(id));

        return result != null ?
            Ok(result) :
            ErrorMessageResult<UserDTO>(CommonErrors.UserNotFound);
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<RequestResponse<UserWithPasswordDTO>>> GetByEmail([FromRoute] string email)
    {
        var result = await repository.GetAsync(new UserWithPasswordProjectionSpec(email));

        return result != null ?
            Ok(RequestResponse<UserWithPasswordDTO>.Success(result)) :
            ErrorMessageResult<UserWithPasswordDTO>(CommonErrors.UserNotFound);
    }

    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<UserDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)

    {
        var result = await repository.PageAsync(pagination, new UserProjectionSpec(pagination.Search));

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<RequestResponse<int>>> GetCount()
    {
        var result = await repository.GetCountAsync<User>();
        var response = RequestResponse<int>.Success(result);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] UserAddDTO user)
    {
        var result = await repository.GetAsync(new UserSpec(user.Email));

        if (result != null)
        {
            return ErrorMessageResult(CommonErrors.UserAlreadyExists);
        }

        await repository.AddAsync(new User
        {
            Email = user.Email,
            Name = user.Name,
            Role = user.Role,
            Password = user.Password
        });

        return Ok(RequestResponse.Success());
    }

    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] UserUpdateDTO user)
    {
        var entity = await repository.GetAsync(new UserSpec(user.Id));

        if (entity != null)
        {
            entity.Name = user.Name ?? entity.Name;
            entity.Password = user.Password ?? entity.Password;

            await repository.UpdateAsync(entity);
        }

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        await repository.DeleteAsync<User>(id);

        return Ok();
    }
}
