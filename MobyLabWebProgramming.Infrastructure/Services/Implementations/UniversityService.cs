using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class UniversityService(IRepository<WebAppDatabaseContext> repository) : IUniversityService
{
    public async Task<ServiceResponse<UniversityDTO>> GetUniversity(Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new UniversityProjectionSpec(id), cancellationToken); // Get a user using a specification on the repository.

        return result != null ? 
            ServiceResponse.ForSuccess(result) : 
            ServiceResponse.FromError<UniversityDTO>(CommonErrors.UniversityNotFound); // Pack the result or error into a ServiceResponse.
    }
    
    public async Task<ServiceResponse<ICollection<University>>> GetUniversitiesByProfessor(string firstName, string lastName, CancellationToken cancellationToken = default)
    {
        var prof = await repository.GetAsync(new ProfessorProjectionSpec(firstName, lastName),
            cancellationToken); // Get a user using a specification on the repository.

        return prof != null
            ? ServiceResponse.ForSuccess(prof.Universities)
            : ServiceResponse.FromError<ICollection<University>>(CommonErrors
                .ProfessorNotFound); // Pack the result or error into a ServiceResponse.
    }
    
    public async Task<ServiceResponse<ICollection<University>>> GetUniversitiesByArticle(string article, CancellationToken cancellationToken = default)
    {
        var ar = await repository.GetAsync(new ArticleProjectionSpec(article),
            cancellationToken); // Get a user using a specification on the repository.

        return ar != null
            ? ServiceResponse.ForSuccess(ar.Universities)
            : ServiceResponse.FromError<ICollection<University>>(CommonErrors
                .ArticleNotFound); // Pack the result or error into a ServiceResponse.
    }
}