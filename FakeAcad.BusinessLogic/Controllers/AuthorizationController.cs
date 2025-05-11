using Microsoft.AspNetCore.Mvc;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Handlers;
using FakeAcad.Core.Responses;
using FakeAcad.Infrastructure.Authorization;
using FakeAcad.Infrastructure.Services.Interfaces;
using FakeAcad.Infrastructure.HttpClients;

namespace FakeAcad.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthorizationController(LoginHttpClient loginHttpClient) : BaseResponseController // The controller must inherit ControllerBase or its derivations, in this case BaseResponseController.
{
    [HttpPost]
    public async Task<ActionResult<RequestResponse<LoginResponseDTO>>> Login([FromBody] LoginDTO login)
    {
        return await loginHttpClient.LoginAsync(login);
    }
}
