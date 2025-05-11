using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Responses;
using FakeAcad.Infrastructure.Authorization;
using FakeAcad.Infrastructure.Services.Interfaces;

namespace FakeAcad.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ArticleController(IUserService userService, IArticleService articleService) : AuthorizedController(userService) {

    [HttpGet]
    public async Task<ActionResult<RequestResponse<ICollection<ArticleDTO>>>> GetAll()
    {
        return FromServiceResponse(await articleService.GetAllArticles());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ArticleDTO>>> GetById([FromRoute] Guid id)
    {
        return FromServiceResponse(await articleService.GetArticle(id));
    }
    
    [HttpGet("{title}")]
    public async Task<ActionResult<RequestResponse<ArticleDTO>>> GetByTitle([FromRoute] string title)
    {
        return FromServiceResponse(await articleService.GetArticleByTitle(title));
    }
    
    [HttpGet("{univ}")]
    public async Task<ActionResult<RequestResponse<ICollection<ArticleDTO>>>> GetByUniv([FromRoute] string univ)
    {
        return FromServiceResponse(await articleService.GetArticlesByUniversity(univ));
    }
    
    [HttpGet("{firstName},{lastName}")]
    public async Task<ActionResult<RequestResponse<ICollection<ArticleDTO>>>> GetByProf([FromRoute] string firstName, string lastName)
    {
        return FromServiceResponse(await articleService.GetArticlesByProfessor(firstName, lastName));
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] ArticleAddDTO article)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null ?
            FromServiceResponse(await articleService.AddArticle(article, currentUser.Result)) :
            ErrorMessageResult(currentUser.Error);
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ? FromServiceResponse(await articleService.DeleteArticle(id, currentUser.Result)) :
            ErrorMessageResult(currentUser.Error);
    }
}