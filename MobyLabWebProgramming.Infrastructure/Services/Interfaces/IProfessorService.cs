using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IProfessorService
{
    public Task<ServiceResponse<ProfessorDTO>> GetProfessor(Guid id, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse<ICollection<Professor>>> GetProfessorsByUniversity(string university, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<ICollection<Professor>>> GetProfessorsByArticle(string article, CancellationToken cancellationToken = default);
}