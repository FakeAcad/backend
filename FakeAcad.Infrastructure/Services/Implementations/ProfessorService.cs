using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Errors;
using FakeAcad.Core.Responses;
using FakeAcad.Infrastructure.HttpClients;
using FakeAcad.Infrastructure.Services.Interfaces;

namespace FakeAcad.Infrastructure.Services.Implementations;

public class ProfessorService(ProfessorHttpClient professorHttpClient) : IProfessorService
{
    public async Task<ServiceResponse<ProfessorDTO>> GetProfessor(Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await professorHttpClient.GetByIdAsync(id);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<ProfessorDTO>(result.ErrorMessage);
        }
        var professor = result.Response;

        return professor != null ?
            ServiceResponse.ForSuccess(professor) :
            ServiceResponse.FromError<ProfessorDTO>(CommonErrors.ProfessorNotFound);
    }

    public async Task<ServiceResponse<ProfessorDTO>> GetProfessorByName(string firstName, string lastName,
        CancellationToken cancellationToken = default)
    {
        var result = await professorHttpClient.GetByNameAsync(firstName, lastName);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<ProfessorDTO>(result.ErrorMessage);
        }

        var professor = result.Response;

        return professor != null ?
            ServiceResponse.ForSuccess(professor) :
            ServiceResponse.FromError<ProfessorDTO>(CommonErrors.ProfessorNotFound);
    }

    public async Task<ServiceResponse<ICollection<ProfessorDTO>>> GetProfessorsByUniversity(string university, CancellationToken cancellationToken = default)
    {
        var result = await professorHttpClient.GetByUniversityAsync(university);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<ICollection<ProfessorDTO>>(result.ErrorMessage);
        }

        var professors = result.Response;

        return professors != null ?
            ServiceResponse.ForSuccess(professors) :
            ServiceResponse.FromError<ICollection<ProfessorDTO>>(CommonErrors.ProfessorNotFound);
    }

    public async Task<ServiceResponse<ICollection<ProfessorDTO>>> GetProfessorsByArticle(string article, CancellationToken cancellationToken = default)
    {
        var result = await professorHttpClient.GetByArticleAsync(article);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<ICollection<ProfessorDTO>>(result.ErrorMessage);
        }

        var professors = result.Response;

        return professors != null ?
            ServiceResponse.ForSuccess(professors) :
            ServiceResponse.FromError<ICollection<ProfessorDTO>>(CommonErrors.ProfessorNotFound);
    }

    public async Task<ServiceResponse> AddProfessor(ProfessorAddDTO professor,
        CancellationToken cancellationToken = default)
    {
        var result = await professorHttpClient.AddProfessorAsync(professor);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError(result.ErrorMessage);
        }

        return result.Response != null ?
            ServiceResponse.ForSuccess() :
            ServiceResponse.FromError(CommonErrors.ProfessorNotFound);
    }
}