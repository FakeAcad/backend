using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Errors;
using FakeAcad.Core.Responses;
using FakeAcad.Infrastructure.HttpClients;
using FakeAcad.Infrastructure.Services.Interfaces;

namespace FakeAcad.Infrastructure.Services.Implementations;

public class ArticleService(ArticleHttpClient articleHttpClient)
    : IArticleService
{
    public async Task<ServiceResponse<ArticleDTO>> GetArticle(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await articleHttpClient.GetByIdAsync(id);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<ArticleDTO>(result.ErrorMessage);
        }

        var article = result.Response;

        return article != null
            ? ServiceResponse.ForSuccess(article)
            : ServiceResponse.FromError<ArticleDTO>(CommonErrors.ArticleNotFound);
    }

    public async Task<ServiceResponse<ArticleDTO>> GetArticleByTitle(string title, CancellationToken cancellationToken = default)
    {
        var result = await articleHttpClient.GetByTitleAsync(title);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<ArticleDTO>(result.ErrorMessage);
        }

        var article = result.Response;

        return article != null
            ? ServiceResponse.ForSuccess(article)
            : ServiceResponse.FromError<ArticleDTO>(CommonErrors.ArticleNotFound);
    }

    public async Task<ServiceResponse<ICollection<ArticleDTO>>> GetArticlesByUniversity(string university, CancellationToken cancellationToken = default)
    {
        var result = await articleHttpClient.GetByUniversityAsync(university);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<ICollection<ArticleDTO>>(result.ErrorMessage);
        }

        var articles = result.Response;

        return articles != null
            ? ServiceResponse.ForSuccess(articles)
            : ServiceResponse.FromError<ICollection<ArticleDTO>>(CommonErrors.ArticleNotFound);
    }

    public async Task<ServiceResponse<ICollection<ArticleDTO>>> GetArticlesByProfessor(string firstName, string lastName, CancellationToken cancellationToken = default)
    {
        var result = await articleHttpClient.GetByProfessorAsync(firstName, lastName);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<ICollection<ArticleDTO>>(result.ErrorMessage);
        }

        var articles = result.Response;

        return articles != null
            ? ServiceResponse.ForSuccess(articles)
            : ServiceResponse.FromError<ICollection<ArticleDTO>>(CommonErrors.ArticleNotFound);
    }

    public async Task<ServiceResponse> AddArticle(ArticleAddDTO article, UserDTO requestingUser,
        CancellationToken cancellationToken = default)
    {
        var result = await articleHttpClient.AddArticleAsync(article);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError(result.ErrorMessage);
        }

        return result.Response != null
            ? ServiceResponse.ForSuccess()
            : ServiceResponse.FromError(CommonErrors.TechnicalSupport);
    }

    public async Task<ServiceResponse> DeleteArticle(Guid article, UserDTO requestingUser,
        CancellationToken cancellationToken = default)
    {
        var result = await articleHttpClient.DeleteArticleAsync(article);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError(result.ErrorMessage);
        }

        return result.Response != null
            ? ServiceResponse.ForSuccess()
            : ServiceResponse.FromError(CommonErrors.TechnicalSupport);
    }

    public async Task<ServiceResponse<ICollection<ArticleDTO>>> GetAllArticles(CancellationToken cancellationToken = default)
    {
        var result = await articleHttpClient.GetAllArticlesAsync();

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<ICollection<ArticleDTO>>(result.ErrorMessage);
        }

        var articles = result.Response;

        return articles != null
            ? ServiceResponse.ForSuccess(articles)
            : ServiceResponse.FromError<ICollection<ArticleDTO>>(CommonErrors.ArticleNotFound);
    }
}