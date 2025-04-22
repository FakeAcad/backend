using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Errors;
using FakeAcad.Core.Responses;
using FakeAcad.Infrastructure.HttpClients;
using FakeAcad.Infrastructure.Services.Interfaces;

namespace FakeAcad.Infrastructure.Services.Implementations;

public class UniversityService(UniversityHttpClient universityHttpClient) : IUniversityService
{
    public async Task<ServiceResponse<UniversityDTO>> GetUniversity(Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await universityHttpClient.GetByIdAsync(id);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<UniversityDTO>(result.ErrorMessage);
        }
        var university = result.Response;

        return university != null ?
            ServiceResponse.ForSuccess(university) :
            ServiceResponse.FromError<UniversityDTO>(CommonErrors.UniversityNotFound);
    }

    public async Task<ServiceResponse<UniversityDTO>> GetUniversityByName(string name,
        CancellationToken cancellationToken = default)
    {
        var result = await universityHttpClient.GetByNameAsync(name);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<UniversityDTO>(result.ErrorMessage);
        }

        var university = result.Response;
        return university != null ?
            ServiceResponse.ForSuccess(university) :
            ServiceResponse.FromError<UniversityDTO>(CommonErrors.UniversityNotFound);
    }

    public async Task<ServiceResponse<ICollection<UniversityDTO>>> GetUniversitiesByProfessor(string firstName, string lastName, CancellationToken cancellationToken = default)
    {
        var result = await universityHttpClient.GetByProfessorAsync(firstName, lastName);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<ICollection<UniversityDTO>>(result.ErrorMessage);
        }

        var universities = result.Response;

        return universities != null ?
            ServiceResponse.ForSuccess(universities) :
            ServiceResponse.FromError<ICollection<UniversityDTO>>(CommonErrors.UniversityNotFound);
    }

    public async Task<ServiceResponse<ICollection<UniversityDTO>>> GetUniversitiesByArticle(string article, CancellationToken cancellationToken = default)
    {
        var result = await universityHttpClient.GetByArticleAsync(article);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError<ICollection<UniversityDTO>>(result.ErrorMessage);
        }

        var universities = result.Response;

        return universities != null ?
            ServiceResponse.ForSuccess(universities) :
            ServiceResponse.FromError<ICollection<UniversityDTO>>(CommonErrors.UniversityNotFound);
    }

    public async Task<ServiceResponse> AddUniversity(UniversityAddDTO university,
        CancellationToken cancellationToken = default)
    {
        var result = await universityHttpClient.AddUniversityAsync(university);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError(result.ErrorMessage);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> AddProfessorToUniversity(Guid uni, Guid prof, CancellationToken cancellationToken = default)
    {
        var result = await universityHttpClient.AddProfessorToUniversityAsync(uni, prof);

        if (result.ErrorMessage != null)
        {
            return ServiceResponse.FromError(result.ErrorMessage);
        }

        return ServiceResponse.ForSuccess();
    }
}