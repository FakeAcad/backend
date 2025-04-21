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
public class ProfessorController(IProfessorService professorService) : BaseResponseController {
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ProfessorDTO>>> GetById([FromRoute] Guid id)
    {
        return FromServiceResponse(await professorService.GetProfessor(id));
    }
    
    [HttpGet("{firstName},{lastName}")]
    public async Task<ActionResult<RequestResponse<ProfessorDTO>>> GetByName([FromRoute] string firstName, string lastName)
    {
        return FromServiceResponse(await professorService.GetProfessorByName(firstName, lastName));
    }
    
    [HttpGet("{article}")]
    public async Task<ActionResult<RequestResponse<ICollection<Professor>>>> GetByArticle([FromRoute] string article)
    {
        return FromServiceResponse(await professorService.GetProfessorsByArticle(article));
    }
    
    [HttpGet("{univ}")]
    public async Task<ActionResult<RequestResponse<ICollection<Professor>>>> GetByUniv([FromRoute] string univ)
    {
        return FromServiceResponse(await professorService.GetProfessorsByUniversity(univ));
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] ProfessorAddDTO professor)
    {
        return FromServiceResponse(await professorService.AddProfessor(professor));
    }
}