using FakeAcad.Core.Errors;

namespace FakeAcad.Core.Responses;

/// <summary>
/// This class is used to encapsulate data or errors for the client as a response to the HTTP request.
/// </summary>
public class RequestResponse<T>
{
    /// <summary>
    /// This is the response to the request, if an error occurred this should be null. 
    /// </summary>
    public T? Response { get; private init; }
    /// <summary>
    /// This is the error message for the error that occurred while responding to the request, if no error occurred this should be null. 
    /// </summary>
    public ErrorMessage? ErrorMessage { get; private init; }

    public RequestResponse() { }

    public static RequestResponse<T> Success(T result)
    {
        return new RequestResponse<T>
        {
            Response = result,
            ErrorMessage = null
        };
    }

    public static RequestResponse<T> Success()
    {
        return new RequestResponse<T>
        {
            Response = default,
            ErrorMessage = null
        };
    }

    public static RequestResponse FromError(ErrorMessage? error)
    {
        return error != null
            ? new RequestResponse
            {
                ErrorMessage = error
            }
            : new()
            {
                Response = "Ok"
            };
    }

    public static RequestResponse<T> FromErrorAnyType(ErrorMessage? error)
    {
        return error != null
            ? new RequestResponse<T>
            {
                ErrorMessage = error
            }
            : new()
            {
                Response = default
            };
    }

    public static RequestResponse<string> FromServiceResponse(ServiceResponse serviceResponse)
    {
        return FromError(serviceResponse.Error);
    }

    public static RequestResponse<T> FromServiceResponse(ServiceResponse<T> serviceResponse)
    {
        return serviceResponse.Error != null
            ? new RequestResponse<T>
            {
                ErrorMessage = serviceResponse.Error
            }
            : new()
            {
                Response = serviceResponse.Result
            };
    }
}

public class RequestResponse : RequestResponse<string>
{
    public static RequestResponse OkRequestResponse => FromError(null);
}
