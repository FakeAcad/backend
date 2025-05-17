using Microsoft.AspNetCore.Mvc;
using FakeAcad.Core.Responses;
using FakeAcad.Infrastructure.Database;
using FakeAcad.Infrastructure.Repositories.Interfaces;
using FakeAcad.Core.Handlers;

namespace FakeAcad.Backend.Controllers;

[ApiController]
[Route("api/[action]")]
public class PingController(IRepository<WebAppDatabaseContext> repository) : BaseResponseController
{
    [HttpGet()]
    public ActionResult<RequestResponse> ping()
    {
        return OkRequestResponse();
    }
}
