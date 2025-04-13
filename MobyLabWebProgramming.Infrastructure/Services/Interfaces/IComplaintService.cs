using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IComplaintService
{
    public Task<ServiceResponse<ComplaintDTO>> GetComplaint(Guid id, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse<ComplaintDTO>> GetComplaintByName(string name, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddComplaint(ComplaintAddDTO complaint, CancellationToken cancellationToken = default);
}