using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Handlers;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Services.Implementations;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

/// <summary>
/// This is a controller example for CRUD operations on users.
/// Inject the required services through the constructor.
/// </summary>
[ApiController] // This attribute specifies for the framework to add functionality to the controller such as binding multipart/form-data.
[Route("api/[controller]/[action]")] // The Route attribute prefixes the routes/url paths with template provides as a string, the keywords between [] are used to automatically take the controller and method name.
public class ComplaintController(IComplaintService complaintService) : BaseResponseController {
    /// <summary>
    /// This method implements the Read operation (R from CRUD) on a user. 
    /// </summary>
    [HttpGet("{id:guid}")] // This attribute will make the controller respond to a HTTP GET request on the route /api/User/GetById/<some_guid>.
    public async Task<ActionResult<RequestResponse<ComplaintDTO>>> GetById([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        return FromServiceResponse(await complaintService.GetComplaint(id));
    }
}
