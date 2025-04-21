using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Entities;

namespace FakeAcad.Core.Specifications;

public class ProfessorProjectionSpec : Specification<Professor, ProfessorDTO>
{
    public ProfessorProjectionSpec(bool orderByCreatedAt = false) =>
        Query.Select(e => new ProfessorDTO()
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Position = e.Position,
                Articles = e.Articles,
                Universities = e.Universities,
            })
            .OrderByDescending(x => x.CreatedAt, orderByCreatedAt);

    public ProfessorProjectionSpec(Guid id) : this() => Query.Where(e => e.Id == id);
    
    public ProfessorProjectionSpec(string? firstName, string? lastName) : this(true) // This constructor will call the first declared constructor with 'true' as the parameter. 
    {
        firstName = !string.IsNullOrWhiteSpace(firstName) ? firstName.Trim() : null;
        lastName = !string.IsNullOrWhiteSpace(lastName) ? lastName.Trim() : null;
        
        if (firstName == null)
        {
            return;
        }
        
        if (lastName == null)
        {
            return;
        }

        var searchExpr1 = $"%{firstName.Replace(" ", "%")}%";
        var searchExpr2 = $"%{lastName.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.FirstName, searchExpr1) && EF.Functions.ILike(e.LastName, searchExpr2)); // This is an example on how database specific expressions can be used via C# expressions.
        // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }

    public static ProfessorProjectionSpec byUniversity(Guid universityId) {
        var spec = new ProfessorProjectionSpec(false);
        spec.Query.Where(e => e.Universities.Any(u => u.Id == universityId));
        return spec;
    }

    public static ProfessorProjectionSpec byArticle(Guid articleId) {
        var spec = new ProfessorProjectionSpec(false);
        spec.Query.Where(e => e.Articles.Any(a => a.Id == articleId));
        return spec;
    }
}