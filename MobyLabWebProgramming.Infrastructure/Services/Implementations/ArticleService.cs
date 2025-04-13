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
                cancellationToken);

        return result != null
            ? ServiceResponse.ForSuccess(result)
            : ServiceResponse.FromError<ArticleDTO>(CommonErrors
                .UserNotFound);
    }
    
    public async Task<ServiceResponse<ArticleDTO>> GetArticleByTitle(string title, CancellationToken cancellationToken = default)
    {
        var result =
            await repository.GetAsync(new ArticleProjectionSpec(title),
                cancellationToken);

        return result != null
            ? ServiceResponse.ForSuccess(result)
            : ServiceResponse.FromError<ArticleDTO>(CommonErrors
                .ArticleNotFound);
    }
    
    public async Task<ServiceResponse<ICollection<Article>>> GetArticlesByUniversity(string university, CancellationToken cancellationToken = default)
    {
        var uni = await repository.GetAsync(new UniversityProjectionSpec(university),
            cancellationToken);

        return uni != null
            ? ServiceResponse.ForSuccess(uni.Articles)
            : ServiceResponse.FromError<ICollection<Article>>(CommonErrors
                .UniversityNotFound);
    }
    
    public async Task<ServiceResponse<ICollection<Article>>> GetArticlesByProfessor(string firstName, string lastName, CancellationToken cancellationToken = default)
    {
        var prof = await repository.GetAsync(new ProfessorProjectionSpec(firstName, lastName),
            cancellationToken);

        return prof != null
            ? ServiceResponse.ForSuccess(prof.Articles)
            : ServiceResponse.FromError<ICollection<Article>>(CommonErrors
                .ProfessorNotFound);
    }
    
    public async Task<ServiceResponse> AddArticle(ArticleAddDTO article, UserDTO requestingUser,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new UniversitySpec(article.Title), cancellationToken);
        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The article already exists!", ErrorCodes.ArticleAlreadyExists));
        }

        ICollection<Professor> professors = new List<Professor>();
        foreach (var p in article.ProfessorsIds)
        {
            var pr = await repository.GetAsync(new ProfessorSpec(p), cancellationToken);
            if (pr == null)
            {
                return ServiceResponse.FromError(CommonErrors.ProfessorNotFound);
            }
            
            professors.Add(pr);
        }
        
        ICollection<University> universities = new List<University>();
        foreach (var u in article.UniversitiesIds)
        {
            var un = await repository.GetAsync(new UniversitySpec(u), cancellationToken);
            if (un == null)
            {
                return ServiceResponse.FromError(CommonErrors.UniversityNotFound);
            }
            
            universities.Add(un);
        }

        await repository.AddAsync(new Article()
        {
            Title = article.Title,
            Description = article.Description,
            Content = article.Content,
            UserId = requestingUser.Id,
            Universities = universities,
            Professors = professors
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteArticle(Guid article, UserDTO requestingUser,
        CancellationToken cancellationToken = default)
    {
        var user = await repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);
        if (user == null)
        {
            return ServiceResponse.FromError(CommonErrors.UserNotFound);
        }
        
        if (requestingUser.Role != UserRoleEnum.Moderator && requestingUser.Id != user.Id)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can delete the article!", ErrorCodes.CannotDelete));
        }
        
        await repository.DeleteAsync<Article>(article, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}