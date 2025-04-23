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
public class UniversityController(IRepository<WebAppDatabaseContext> repository) : BaseResponseController
{

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<UniversityDTO>>> GetById([FromRoute] Guid id)
    {
        var result = await repository.GetAsync(new UniversityProjectionSpec(id));

        return result != null
            ? Ok(result)
            : ErrorMessageResult<UniversityDTO>(CommonErrors.UniversityNotFound);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<RequestResponse<UniversityDTO>>> GetByName([FromRoute] string name)
    {
        var result = await repository.GetAsync(new UniversityProjectionSpec(name));

        return result != null
            ? Ok(result)
            : ErrorMessageResult<UniversityDTO>(CommonErrors.UniversityNotFound);
    }

    [HttpGet("{article}")]
    public async Task<ActionResult<RequestResponse<ICollection<UniversityDTO>>>> GetByArticle([FromRoute] string article)
    {
        var articleDTO = await repository.GetAsync(new ArticleProjectionSpec(article));
        if (articleDTO == null)
        {
            return ErrorMessageResult<ICollection<UniversityDTO>>(CommonErrors.ArticleNotFound);
        }

        var result = await repository.ListAsync(new ArticleProjectionSpec(article));

        return result != null
            ? Ok(result)
            : ErrorMessageResult<ICollection<UniversityDTO>>(CommonErrors.ArticleNotFound);
    }

    [HttpGet("{firstName},{lastName}")]
    public async Task<ActionResult<RequestResponse<ICollection<UniversityDTO>>>> GetByProf([FromRoute] string firstName, string lastName)
    {
        var professor = await repository.GetAsync(new ProfessorProjectionSpec(firstName, lastName));
        if (professor == null)
        {
            return ErrorMessageResult<ICollection<UniversityDTO>>(CommonErrors.ProfessorNotFound);
        }

        var result = await repository.ListAsync(new ProfessorProjectionSpec(firstName, lastName));

        return result != null
            ? Ok(result)
            : ErrorMessageResult<ICollection<UniversityDTO>>(CommonErrors.ProfessorNotFound);
    }

    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] UniversityAddDTO university)
    {
        var result = await repository.GetAsync(new UniversitySpec(university.Name));
        if (result != null)
        {
            return ErrorMessageResult(CommonErrors.UniversityAlreadyExists);
        }

        ICollection<Professor> professors = new List<Professor>();
        foreach (var p in university.ProfessorsIds)
        {
            var pr = await repository.GetAsync(new ProfessorSpec(p));
            if (pr == null)
            {
                return ErrorMessageResult(CommonErrors.ProfessorNotFound);
            }

            professors.Add(pr);
        }

        await repository.AddAsync(new University()
        {
            Name = university.Name,
            Professors = professors
        });

        return Ok();
    }

    [HttpPost("{uni},{prof}")]
    public async Task<ActionResult<RequestResponse>> AddProfessorToUniversity([FromRoute] Guid uni, Guid prof)
    {
        var p = await repository.GetAsync(new ProfessorSpec(prof));
        if (p == null)
        {
            return ErrorMessageResult(CommonErrors.ProfessorNotFound);
        }

        var u = await repository.GetAsync(new UniversitySpec(uni));
        if (u == null)
        {
            return ErrorMessageResult(CommonErrors.UniversityNotFound);
        }

        u.Professors.Add(p);

        return Ok();
    }
}