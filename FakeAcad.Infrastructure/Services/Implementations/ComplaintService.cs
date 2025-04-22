using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Errors;
using FakeAcad.Core.Responses;
using FakeAcad.Infrastructure.HttpClients;
using FakeAcad.Infrastructure.Services.Interfaces;

namespace FakeAcad.Infrastructure.Services.Implementations;

public class ComplaintService(ComplaintHttpClient complaintHttpClient) : IComplaintService
{
    public async Task<ServiceResponse<ComplaintDTO>> GetComplaint(Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await complaintHttpClient.GetByIdAsync(id);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<ComplaintDTO>(result.ErrorMessage);
        }

        var complaint = result.Response;

        return complaint != null ?
            ServiceResponse.ForSuccess(complaint) :
            ServiceResponse.FromError<ComplaintDTO>(CommonErrors.ComplaintNotFound);
    }

    public async Task<ServiceResponse<ComplaintDTO>> GetComplaintByName(string name,
        CancellationToken cancellationToken = default)
    {
        var result = await complaintHttpClient.GetByNameAsync(name);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<ComplaintDTO>(result.ErrorMessage);
        }

        var complaint = result.Response;

        return complaint != null ?
            ServiceResponse.ForSuccess(complaint) :
            ServiceResponse.FromError<ComplaintDTO>(CommonErrors.ComplaintNotFound);
    }

    public async Task<ServiceResponse> AddComplaint(ComplaintAddDTO complaint,
        CancellationToken cancellationToken = default)
    {
        var result = await complaintHttpClient.AddComplaintAsync(complaint);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError(result.ErrorMessage);
        }

        return result.Response != null ?
            ServiceResponse.ForSuccess() :
            ServiceResponse.FromError(CommonErrors.ComplaintNotFound);
    }
}