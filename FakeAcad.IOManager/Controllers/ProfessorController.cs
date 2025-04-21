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
public class ProfessorController(IRepository<WebAppDatabaseContext> repository) : BaseResponseController
{

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ProfessorDTO>>> GetById([FromRoute] Guid id)
    {
        var result = await repository.GetAsync(new ProfessorProjectionSpec(id));

        return result != null
            ? Ok(result)
            : ErrorMessageResult<ProfessorDTO>(CommonErrors.ProfessorNotFound);
    }

    [HttpGet("{firstName},{lastName}")]
    public async Task<ActionResult<RequestResponse<ProfessorDTO>>> GetByName([FromRoute] string firstName, string lastName)
    {
        var result = await repository.GetAsync(new ProfessorProjectionSpec(firstName, lastName));

        return result != null
            ? Ok(result)
            : ErrorMessageResult<ProfessorDTO>(CommonErrors.ProfessorNotFound);
    }

    [HttpGet("{article}")]
    public async Task<ActionResult<RequestResponse<ICollection<ProfessorDTO>>>> GetByArticle([FromRoute] string article)
    {
        var articleDTO = await repository.GetAsync(new ArticleProjectionSpec(article));
        if (articleDTO == null)
        {
            return ErrorMessageResult<ICollection<ProfessorDTO>>(CommonErrors.ArticleNotFound);
        }

        var result = await repository.GetAsync(new ArticleProjectionSpec(article));

        return result != null
            ? Ok(result.Professors)
            : ErrorMessageResult<ICollection<ProfessorDTO>>(CommonErrors.ArticleNotFound);
    }

    [HttpGet("{univ}")]
    public async Task<ActionResult<RequestResponse<ICollection<ProfessorDTO>>>> GetByUniv([FromRoute] string univ)
    {
        var universityDTO = await repository.GetAsync(new UniversityProjectionSpec(univ));
        if (universityDTO == null)
        {
            return ErrorMessageResult<ICollection<ProfessorDTO>>(CommonErrors.UniversityNotFound);
        }

        var result = await repository.GetAsync(ProfessorProjectionSpec.byUniversity(universityDTO.Id));

        return result != null
            ? Ok(result)
            : ErrorMessageResult<ICollection<ProfessorDTO>>(CommonErrors.UniversityNotFound);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] ProfessorAddDTO professor)
    {
        var result = await repository.GetAsync(new ProfessorSpec(professor.FirstName, professor.LastName));
        if (result != null)
        {
            return ErrorMessageResult(CommonErrors.ProfessorAlreadyExists);
        }

        ICollection<University> universities = new List<University>();
        foreach (var u in professor.UniversitiesIds)
        {
            var un = await repository.GetAsync(new UniversitySpec(u));
            if (un == null)
            {
                return ErrorMessageResult(CommonErrors.UniversityNotFound);
            }

            universities.Add(un);
        }

        await repository.AddAsync(new Professor()
        {
            FirstName = professor.FirstName,
            LastName = professor.LastName,
            Position = professor.Position,
            Universities = universities
        });

        return Ok();
    }
}