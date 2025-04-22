using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Entities;
using FakeAcad.Core.Handlers;
using FakeAcad.Core.Responses;
using FakeAcad.Infrastructure.Repositories.Interfaces;
using FakeAcad.Infrastructure.Database;
using FakeAcad.Core.Specifications;
using FakeAcad.Core.Errors;

namespace FakeAcad.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ArticleController(IRepository<WebAppDatabaseContext> repository) : BaseResponseController
{

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ArticleDTO>>> GetById([FromRoute] Guid id)
    {
        var result = await repository.GetAsync(new ArticleProjectionSpec(id));

        return result != null
            ? Ok(result)
            : ErrorMessageResult<ArticleDTO>(CommonErrors.ArticleNotFound);
    }

    [HttpGet("{title}")]
    public async Task<ActionResult<RequestResponse<ArticleDTO>>> GetByTitle([FromRoute] string title)
    {
        var result = await repository.GetAsync(new ArticleProjectionSpec(title));

        return result != null
            ? Ok(result)
            : ErrorMessageResult<ArticleDTO>(CommonErrors.ArticleNotFound);
    }

    [HttpGet("{univ}")]
    public async Task<ActionResult<RequestResponse<ICollection<ArticleDTO>>>> GetByUniv([FromRoute] string university)
    {
        var uni = await repository.GetAsync(new UniversityProjectionSpec(university));
        if (uni == null)
        {
            return ErrorMessageResult<ICollection<ArticleDTO>>(CommonErrors.UniversityNotFound);
        }

        var result = await repository.GetAsync(ArticleProjectionSpec.byUniversity(uni.Id));

        return result != null
            ? Ok(result)
            : ErrorMessageResult<ICollection<ArticleDTO>>(CommonErrors.UniversityNotFound);
    }

    [HttpGet("{firstName},{lastName}")]
    public async Task<ActionResult<RequestResponse<ICollection<ArticleDTO>>>> GetByProf([FromRoute] string firstName, string lastName)
    {
        var prof = await repository.GetAsync(new ProfessorProjectionSpec(firstName, lastName));
        if (prof == null)
        {
            return ErrorMessageResult<ICollection<ArticleDTO>>(CommonErrors.ProfessorNotFound);
        }

        var result = await repository.GetAsync(ArticleProjectionSpec.byProfessor(prof.Id));

        return result != null
            ? Ok(result)
            : ErrorMessageResult<ICollection<ArticleDTO>>(CommonErrors.ProfessorNotFound);
    }

    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] ArticleAddDTO article)
    {
        var result = await repository.GetAsync(new ArticleSpec(article.Title));
        if (result != null)
        {
            return ErrorMessageResult(CommonErrors.ArticleAlreadyExists);
        }

        ICollection<Professor> professors = new List<Professor>();
        foreach (var p in article.ProfessorsIds)
        {
            var pr = await repository.GetAsync(new ProfessorSpec(p));
            if (pr == null)
            {
                return ErrorMessageResult(CommonErrors.ProfessorNotFound);
            }

            professors.Add(pr);
        }

        ICollection<University> universities = new List<University>();
        foreach (var u in article.UniversitiesIds)
        {
            var un = await repository.GetAsync(new UniversitySpec(u));
            if (un == null)
            {
                return ErrorMessageResult(CommonErrors.UniversityNotFound);
            }

            universities.Add(un);
        }

        await repository.AddAsync(new Article()
        {
            Title = article.Title,
            Description = article.Description,
            Content = article.Content,
            UserId = article.UserId,
            Universities = universities,
            Professors = professors
        }, default);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        await repository.DeleteAsync<Article>(id);
        return Ok();
    }
}