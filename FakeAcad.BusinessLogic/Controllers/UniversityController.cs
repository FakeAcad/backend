using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Entities;
using FakeAcad.Core.Handlers;
using FakeAcad.Core.Responses;
using FakeAcad.Infrastructure.Services.Interfaces;

namespace FakeAcad.Backend.Controllers;

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
    public async Task<ActionResult<RequestResponse<ICollection<UniversityDTO>>>> GetByArticle([FromRoute] string article)
    {
        return FromServiceResponse(await universityService.GetUniversitiesByArticle(article));
    }
    
    [HttpGet("{firstName},{lastName}")]
    public async Task<ActionResult<RequestResponse<ICollection<UniversityDTO>>>> GetByProf([FromRoute] string firstName, string lastName)
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