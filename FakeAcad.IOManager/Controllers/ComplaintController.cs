using Microsoft.AspNetCore.Mvc;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Handlers;
using FakeAcad.Core.Responses;
using FakeAcad.Infrastructure.Repositories.Interfaces;
using FakeAcad.Infrastructure.Database;
using FakeAcad.Core.Specifications;
using FakeAcad.Core.Errors;
using FakeAcad.Core.Entities;

namespace FakeAcad.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ComplaintController(IRepository<WebAppDatabaseContext> repository) : BaseResponseController {
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ComplaintDTO>>> GetById([FromRoute] Guid id)
    {
        var result = await repository.GetAsync(new ComplaintProjectionSpec(id));

        return result != null
            ? Ok(result)
            : ErrorMessageResult<ComplaintDTO>(CommonErrors.ComplaintNotFound);
    }
    
    [HttpGet("{name}")]
    public async Task<ActionResult<RequestResponse<ComplaintDTO>>> GetById([FromRoute] string name)
    {
        var result = await repository.GetAsync(new ComplaintProjectionSpec(name));

        return result != null
            ? Ok(result)
            : ErrorMessageResult<ComplaintDTO>(CommonErrors.ComplaintNotFound);
    }
    
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] ComplaintAddDTO complaint)
    {
        var result = await repository.GetAsync(new ComplaintSpec(complaint.Name));
        if (result != null)
        {
            return ErrorMessageResult(CommonErrors.ComplaintAlreadyExists);
        }
        
        var a = await repository.GetAsync(new ArticleSpec(complaint.ArticleId));
        if (a == null)
        { 
            return ErrorMessageResult(CommonErrors.ArticleNotFound);
        }

        await repository.AddAsync(new Complaint()
        {
            ComplaintType = complaint.ComplaintType,
            Severity = complaint.Severity,
            Name = complaint.Name,
            ArticleId = complaint.ArticleId,
        }, default);

        return Ok();
    }
}
