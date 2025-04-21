using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Entities;
using FakeAcad.Core.Requests;
using FakeAcad.Core.Responses;

namespace FakeAcad.Infrastructure.Services.Interfaces;

public interface IProfessorService
{
    public Task<ServiceResponse<ProfessorDTO>> GetProfessor(Guid id, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse<ProfessorDTO>> GetProfessorByName(string firstName, string lastName, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse<ICollection<Professor>>> GetProfessorsByUniversity(string university, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<ICollection<Professor>>> GetProfessorsByArticle(string article, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse> AddProfessor(ProfessorAddDTO professor, CancellationToken cancellationToken = default);
}