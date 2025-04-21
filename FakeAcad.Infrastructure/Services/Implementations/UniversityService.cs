using System.Net;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Entities;
using FakeAcad.Core.Errors;
using FakeAcad.Core.Responses;
using FakeAcad.Core.Specifications;
using FakeAcad.Infrastructure.Database;
using FakeAcad.Infrastructure.Repositories.Interfaces;
using FakeAcad.Infrastructure.Services.Interfaces;

namespace FakeAcad.Infrastructure.Services.Implementations;

public class UniversityService(IRepository<WebAppDatabaseContext> repository) : IUniversityService
{
    public async Task<ServiceResponse<UniversityDTO>> GetUniversity(Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new UniversityProjectionSpec(id), cancellationToken);

        return result != null ? 
            ServiceResponse.ForSuccess(result) : 
            ServiceResponse.FromError<UniversityDTO>(CommonErrors.UniversityNotFound);
    }
    
    public async Task<ServiceResponse<UniversityDTO>> GetUniversityByName(string name,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new UniversityProjectionSpec(name), cancellationToken);

        return result != null ? 
            ServiceResponse.ForSuccess(result) : 
            ServiceResponse.FromError<UniversityDTO>(CommonErrors.UniversityNotFound);
    }
    
    public async Task<ServiceResponse<ICollection<University>>> GetUniversitiesByProfessor(string firstName, string lastName, CancellationToken cancellationToken = default)
    {
        var prof = await repository.GetAsync(new ProfessorProjectionSpec(firstName, lastName),
            cancellationToken);

        return prof != null
            ? ServiceResponse.ForSuccess(prof.Universities)
            : ServiceResponse.FromError<ICollection<University>>(CommonErrors
                .ProfessorNotFound);
    }
    
    public async Task<ServiceResponse<ICollection<University>>> GetUniversitiesByArticle(string article, CancellationToken cancellationToken = default)
    {
        var ar = await repository.GetAsync(new ArticleProjectionSpec(article),
            cancellationToken);

        return ar != null
            ? ServiceResponse.ForSuccess(ar.Universities)
            : ServiceResponse.FromError<ICollection<University>>(CommonErrors
                .ArticleNotFound);
    }

    public async Task<ServiceResponse> AddUniversity(UniversityAddDTO university,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new UniversitySpec(university.Name), cancellationToken);
        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The university already exists!", ErrorCodes.UniversityAlreadyExists));
        }

        ICollection<Professor> professors = new List<Professor>();
        foreach (var p in university.ProfessorsIds)
        {
            var pr = await repository.GetAsync(new ProfessorSpec(p), cancellationToken);
            if (pr == null)
            {
                return ServiceResponse.FromError(CommonErrors.ProfessorNotFound);
            }
            
            professors.Add(pr);
        }

        await repository.AddAsync(new University()
        {
            Name = university.Name,
            Professors = professors
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> AddProfessorToUniversity(Guid uni, Guid prof, CancellationToken cancellationToken = default)
    {
        var p = await repository.GetAsync(new ProfessorSpec(prof), cancellationToken);
        if (p == null)
        {
            return ServiceResponse.FromError(CommonErrors.ProfessorNotFound);
        }
        
        var u = await repository.GetAsync(new UniversitySpec(uni), cancellationToken);
        if (u == null)
        {
            return ServiceResponse.FromError(CommonErrors.UniversityNotFound);
        }
        
        u.Professors.Add(p);
        await repository.UpdateAsync(u, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
}