using System.Net;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class ProfessorService(IRepository<WebAppDatabaseContext> repository) : IProfessorService
{
    public async Task<ServiceResponse<ProfessorDTO>> GetProfessor(Guid id,
        CancellationToken cancellationToken = default)
    {
        var result =
            await repository.GetAsync(new ProfessorProjectionSpec(id),
                cancellationToken);

        return result != null
            ? ServiceResponse.ForSuccess(result)
            : ServiceResponse.FromError<ProfessorDTO>(CommonErrors
                .ProfessorNotFound);
    }
    
    public async Task<ServiceResponse<ProfessorDTO>> GetProfessorByName(string firstName, string lastName,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new ProfessorProjectionSpec(firstName, lastName), cancellationToken);

        return result != null ? 
            ServiceResponse.ForSuccess(result) : 
            ServiceResponse.FromError<ProfessorDTO>(CommonErrors.ProfessorNotFound);
    }
    
    public async Task<ServiceResponse<ICollection<Professor>>> GetProfessorsByUniversity(string university, CancellationToken cancellationToken = default)
    {
        var uni = await repository.GetAsync(new UniversityProjectionSpec(university),
            cancellationToken);

        return uni != null
            ? ServiceResponse.ForSuccess(uni.Professors)
            : ServiceResponse.FromError<ICollection<Professor>>(CommonErrors
                .UniversityNotFound);
    }
    
    public async Task<ServiceResponse<ICollection<Professor>>> GetProfessorsByArticle(string article, CancellationToken cancellationToken = default)
    {
        var ar = await repository.GetAsync(new ArticleProjectionSpec(article),
            cancellationToken);

        return ar != null
            ? ServiceResponse.ForSuccess(ar.Professors)
            : ServiceResponse.FromError<ICollection<Professor>>(CommonErrors
                .ArticleNotFound);
    }
    
    public async Task<ServiceResponse> AddProfessor(ProfessorAddDTO professor,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new ProfessorSpec(professor.FirstName, professor.LastName), cancellationToken);
        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The professor already exists!", ErrorCodes.ProfessorAlreadyExists));
        }

        ICollection<University> universities = new List<University>();
        foreach (var u in professor.UniversitiesIds)
        {
            var un = await repository.GetAsync(new UniversitySpec(u), cancellationToken);
            if (un == null)
            {
                return ServiceResponse.FromError(CommonErrors.UniversityNotFound);
            }
            
            universities.Add(un);
        }

        await repository.AddAsync(new Professor()
        {
            FirstName = professor.FirstName,
            LastName = professor.LastName,
            Position = professor.Position,
            Universities = universities
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}