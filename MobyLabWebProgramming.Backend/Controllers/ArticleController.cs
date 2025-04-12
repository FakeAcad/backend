using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Handlers;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ArticleController(IArticleService articleService) : BaseResponseController {
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ArticleDTO>>> GetById([FromRoute] Guid id)
    {
        return FromServiceResponse(await articleService.GetArticle(id));
    }
    
    [HttpGet("{title}")] // This attribute will make the controller respond to a HTTP GET request on the route /api/User/GetById/<some_guid>.
    public async Task<ActionResult<RequestResponse<ArticleDTO>>> GetByTitle([FromRoute] string title) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        return FromServiceResponse(await articleService.GetArticleByTitle(title));
    }
    
    [HttpGet("{univ}")] // This attribute will make the controller respond to a HTTP GET request on the route /api/User/GetById/<some_guid>.
    public async Task<ActionResult<RequestResponse<ICollection<Article>>>> GetByUniv([FromRoute] string univ) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        return FromServiceResponse(await articleService.GetArticlesByUniversity(univ));
    }
    
    [HttpGet("{firstName},{lastName}")] // This attribute will make the controller respond to a HTTP GET request on the route /api/User/GetById/<some_guid>.
    public async Task<ActionResult<RequestResponse<ICollection<Article>>>> GetByProf([FromRoute] string firstName, string lastName) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        return FromServiceResponse(await articleService.GetArticlesByProfessor(firstName, lastName));
    }
}