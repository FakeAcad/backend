using System.Net;
using MobyLabWebProgramming.Core.Constants;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class ArticleService(IRepository<WebAppDatabaseContext> repository)
    : IArticleService
{
    public async Task<ServiceResponse<ArticleDTO>> GetArticle(Guid id, CancellationToken cancellationToken = default)
    {
        var result =
            await repository.GetAsync(new ArticleProjectionSpec(id),
                cancellationToken); // Get a user using a specification on the repository.

        return result != null
            ? ServiceResponse.ForSuccess(result)
            : ServiceResponse.FromError<ArticleDTO>(CommonErrors
                .UserNotFound); // Pack the result or error into a ServiceResponse.
    }
    
    public async Task<ServiceResponse<ArticleDTO>> GetArticleByTitle(string title, CancellationToken cancellationToken = default)
    {
        var result =
            await repository.GetAsync(new ArticleProjectionSpec(title),
                cancellationToken); // Get a user using a specification on the repository.

        return result != null
            ? ServiceResponse.ForSuccess(result)
            : ServiceResponse.FromError<ArticleDTO>(CommonErrors
                .ArticleNotFound); // Pack the result or error into a ServiceResponse.
    }
    
    public async Task<ServiceResponse<ICollection<Article>>> GetArticlesByUniversity(string university, CancellationToken cancellationToken = default)
    {
        var uni = await repository.GetAsync(new UniversityProjectionSpec(university),
            cancellationToken); // Get a user using a specification on the repository.

        return uni != null
            ? ServiceResponse.ForSuccess(uni.Articles)
            : ServiceResponse.FromError<ICollection<Article>>(CommonErrors
                .UniversityNotFound); // Pack the result or error into a ServiceResponse.
    }
    
    public async Task<ServiceResponse<ICollection<Article>>> GetArticlesByProfessor(string firstName, string lastName, CancellationToken cancellationToken = default)
    {
        var prof = await repository.GetAsync(new ProfessorProjectionSpec(firstName, lastName),
            cancellationToken); // Get a user using a specification on the repository.

        return prof != null
            ? ServiceResponse.ForSuccess(prof.Articles)
            : ServiceResponse.FromError<ICollection<Article>>(CommonErrors
                .ProfessorNotFound); // Pack the result or error into a ServiceResponse.
    }
}