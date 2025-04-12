using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class ComplaintService(IRepository<WebAppDatabaseContext> repository) : IComplaintService
{
    public async Task<ServiceResponse<ComplaintDTO>> GetComplaint(Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new ComplaintProjectionSpec(id), cancellationToken); // Get a user using a specification on the repository.

        return result != null ? 
            ServiceResponse.ForSuccess(result) : 
            ServiceResponse.FromError<ComplaintDTO>(CommonErrors.ComplaintNotFound); // Pack the result or error into a ServiceResponse.
    }
}