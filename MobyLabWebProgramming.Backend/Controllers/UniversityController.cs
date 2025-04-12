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
    
    [HttpGet("{article}")] // This attribute will make the controller respond to a HTTP GET request on the route /api/User/GetById/<some_guid>.
    public async Task<ActionResult<RequestResponse<ICollection<University>>>> GetByArticle([FromRoute] string article) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        return FromServiceResponse(await universityService.GetUniversitiesByArticle(article));
    }
    
    [HttpGet("{firstName},{lastName}")] // This attribute will make the controller respond to a HTTP GET request on the route /api/User/GetById/<some_guid>.
    public async Task<ActionResult<RequestResponse<ICollection<University>>>> GetByProf([FromRoute] string firstName, string lastName) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        return FromServiceResponse(await universityService.GetUniversitiesByProfessor(firstName, lastName));
    }
}