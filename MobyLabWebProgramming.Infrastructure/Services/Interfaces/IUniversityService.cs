using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IUniversityService
{
    public Task<ServiceResponse<UniversityDTO>> GetUniversity(Guid id, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse<UniversityDTO>> GetUniversityByName(string name, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<ICollection<University>>> GetUniversitiesByProfessor(string firstName, string lastName, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse<ICollection<University>>> GetUniversitiesByArticle(string article, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse> AddUniversity(UniversityAddDTO university, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddProfessorToUniversity(Guid prof, Guid uni, CancellationToken cancellationToken = default);
}