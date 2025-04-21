using System.Net;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Entities;
using FakeAcad.Core.Errors;
using FakeAcad.Core.Responses;
using FakeAcad.Core.Specifications;
using FakeAcad.Infrastructure.Database;
using FakeAcad.Infrastructure.Repositories.Interfaces;
using FakeAcad.Infrastructure.Services.Interfaces;

namespace FakeAcad.Infrastructure.Services.Implementations;

public class ComplaintService(IRepository<WebAppDatabaseContext> repository) : IComplaintService
{
    public async Task<ServiceResponse<ComplaintDTO>> GetComplaint(Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new ComplaintProjectionSpec(id), cancellationToken);

        return result != null ? 
            ServiceResponse.ForSuccess(result) : 
            ServiceResponse.FromError<ComplaintDTO>(CommonErrors.ComplaintNotFound);
    }
    
    public async Task<ServiceResponse<ComplaintDTO>> GetComplaintByName(string name,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new ComplaintProjectionSpec(name), cancellationToken);

        return result != null ? 
            ServiceResponse.ForSuccess(result) : 
            ServiceResponse.FromError<ComplaintDTO>(CommonErrors.ComplaintNotFound);
    }
    
    public async Task<ServiceResponse> AddComplaint(ComplaintAddDTO complaint,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new ComplaintSpec(complaint.Name), cancellationToken);
        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The complaint already exists!", ErrorCodes.ComplaintAlreadyExists));
        }
        
        var a = await repository.GetAsync(new ArticleSpec(complaint.ArticleId), cancellationToken);
        if (a == null)
        { 
            return ServiceResponse.FromError(CommonErrors.ArticleNotFound);
        }

        await repository.AddAsync(new Complaint()
        {
            ComplaintType = complaint.ComplaintType,
            Severity = complaint.Severity,
            Name = complaint.Name,
            ArticleId = complaint.ArticleId,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}