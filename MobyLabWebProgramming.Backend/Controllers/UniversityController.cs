using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Handlers;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UniversityController(IUniversityService universityService) : BaseResponseController {
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<UniversityDTO>>> GetById([FromRoute] Guid id)
    {
        return FromServiceResponse(await universityService.GetUniversity(id));
    }
    
    [HttpGet("{name}")]
    public async Task<ActionResult<RequestResponse<UniversityDTO>>> GetByName([FromRoute] string name)
    {
        return FromServiceResponse(await universityService.GetUniversityByName(name));
    }
    
    [HttpGet("{article}")]
    public async Task<ActionResult<RequestResponse<ICollection<University>>>> GetByArticle([FromRoute] string article)
    {
        return FromServiceResponse(await universityService.GetUniversitiesByArticle(article));
    }
    
    [HttpGet("{firstName},{lastName}")]
    public async Task<ActionResult<RequestResponse<ICollection<University>>>> GetByProf([FromRoute] string firstName, string lastName)
    {
        return FromServiceResponse(await universityService.GetUniversitiesByProfessor(firstName, lastName));
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] UniversityAddDTO university)
    {
        return FromServiceResponse(await universityService.AddUniversity(university));
    }
}