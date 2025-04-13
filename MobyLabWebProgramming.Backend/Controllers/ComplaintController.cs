using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Handlers;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Services.Implementations;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ComplaintController(IComplaintService complaintService) : BaseResponseController {
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ComplaintDTO>>> GetById([FromRoute] Guid id)
    {
        return FromServiceResponse(await complaintService.GetComplaint(id));
    }
    
    [HttpGet("{name}")]
    public async Task<ActionResult<RequestResponse<ComplaintDTO>>> GetById([FromRoute] string name)
    {
        return FromServiceResponse(await complaintService.GetComplaintByName(name));
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] ComplaintAddDTO complaint)
    {
        return FromServiceResponse(await complaintService.AddComplaint(complaint));
    }
}
