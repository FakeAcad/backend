using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Entities;
using FakeAcad.Core.Requests;
using FakeAcad.Core.Responses;

namespace FakeAcad.Infrastructure.Services.Interfaces;

public interface IArticleService
{
    public Task<ServiceResponse<ArticleDTO>> GetArticle(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<ArticleDTO>> GetArticleByTitle(string title, CancellationToken cancellationToken = default);
    
    // public Task<ServiceResponse<ICollection<ArticleDTO>>> GetArticlesByUser(string university, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse<ICollection<Article>>> GetArticlesByUniversity(string university, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse<ICollection<Article>>> GetArticlesByProfessor(string firstName, string lastName, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse> AddArticle(ArticleAddDTO article, UserDTO requestingUser, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse> DeleteArticle(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
}