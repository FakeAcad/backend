using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Handlers;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProfessorController(IProfessorService professorService) : BaseResponseController {
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ProfessorDTO>>> GetById([FromRoute] Guid id)
    {
        return FromServiceResponse(await professorService.GetProfessor(id));
    }
    
    [HttpGet("{article}")] // This attribute will make the controller respond to a HTTP GET request on the route /api/User/GetById/<some_guid>.
    public async Task<ActionResult<RequestResponse<ICollection<Professor>>>> GetByArticle([FromRoute] string article) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        return FromServiceResponse(await professorService.GetProfessorsByArticle(article));
    }
    
    [HttpGet("{univ}")] // This attribute will make the controller respond to a HTTP GET request on the route /api/User/GetById/<some_guid>.
    public async Task<ActionResult<RequestResponse<ICollection<Professor>>>> GetByUniv([FromRoute] string univ) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        return FromServiceResponse(await professorService.GetProfessorsByUniversity(univ));
    }
}