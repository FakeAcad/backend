using System.Net;

namespace FakeAcad.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ArticleNotFound => new(HttpStatusCode.NotFound, "Article doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage UniversityNotFound => new(HttpStatusCode.NotFound, "University doesn't exist!", ErrorCodes.EntityNotFound);
    
    public static ErrorMessage ProfessorNotFound => new(HttpStatusCode.NotFound, "Professor doesn't exist!", ErrorCodes.EntityNotFound);
    
    public static ErrorMessage ComplaintNotFound => new(HttpStatusCode.NotFound, "Complaint doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage FileNotFound => new(HttpStatusCode.NotFound, "File not found on disk!", ErrorCodes.PhysicalFileNotFound);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);
}
