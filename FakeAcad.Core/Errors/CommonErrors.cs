using System.Net;

namespace FakeAcad.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage UserAlreadyExists => new(HttpStatusCode.Conflict, "User already exists!", ErrorCodes.UserAlreadyExists);
    public static ErrorMessage ArticleNotFound => new(HttpStatusCode.NotFound, "Article doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage UniversityNotFound => new(HttpStatusCode.NotFound, "University doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage UniversityAlreadyExists => new(HttpStatusCode.Conflict, "University already exists!", ErrorCodes.UniversityAlreadyExists);

    public static ErrorMessage ArticleAlreadyExists => new(HttpStatusCode.Conflict, "Article already exists!", ErrorCodes.ArticleAlreadyExists);
    public static ErrorMessage ProfessorNotFound => new(HttpStatusCode.NotFound, "Professor doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ProfessorAlreadyExists => new(HttpStatusCode.Conflict, "Professor already exists!", ErrorCodes.ProfessorAlreadyExists);

    public static ErrorMessage ComplaintNotFound => new(HttpStatusCode.NotFound, "Complaint doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ComplaintAlreadyExists => new(HttpStatusCode.Conflict, "Complaint already exists!", ErrorCodes.ComplaintAlreadyExists);
    public static ErrorMessage FileNotFound => new(HttpStatusCode.NotFound, "File not found on disk!", ErrorCodes.PhysicalFileNotFound);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);

    public static ErrorMessage FailedToDeserialize => new(HttpStatusCode.BadRequest, "Failed to deserialize the request body!", ErrorCodes.TechnicalError);

    public static ErrorMessage WrongPassword => new(HttpStatusCode.Unauthorized, "Wrong password!", ErrorCodes.WrongPassword);
}
