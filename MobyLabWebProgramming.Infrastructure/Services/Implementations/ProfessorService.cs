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
                cancellationToken); // Get a user using a specification on the repository.

        return result != null
            ? ServiceResponse.ForSuccess(result)
            : ServiceResponse.FromError<ProfessorDTO>(CommonErrors
                .ProfessorNotFound); // Pack the result or error into a ServiceResponse.
    }
    
    public async Task<ServiceResponse<ICollection<Professor>>> GetProfessorsByUniversity(string university, CancellationToken cancellationToken = default)
    {
        var uni = await repository.GetAsync(new UniversityProjectionSpec(university),
            cancellationToken); // Get a user using a specification on the repository.

        return uni != null
            ? ServiceResponse.ForSuccess(uni.Professors)
            : ServiceResponse.FromError<ICollection<Professor>>(CommonErrors
                .UniversityNotFound); // Pack the result or error into a ServiceResponse.
    }
    
    public async Task<ServiceResponse<ICollection<Professor>>> GetProfessorsByArticle(string article, CancellationToken cancellationToken = default)
    {
        var ar = await repository.GetAsync(new ArticleProjectionSpec(article),
            cancellationToken); // Get a user using a specification on the repository.

        return ar != null
            ? ServiceResponse.ForSuccess(ar.Professors)
            : ServiceResponse.FromError<ICollection<Professor>>(CommonErrors
                .ArticleNotFound); // Pack the result or error into a ServiceResponse.
    }
}