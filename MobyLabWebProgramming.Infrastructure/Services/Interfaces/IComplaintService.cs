using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IComplaintService
{
    public Task<ServiceResponse<ComplaintDTO>> GetComplaint(Guid id, CancellationToken cancellationToken = default);
}