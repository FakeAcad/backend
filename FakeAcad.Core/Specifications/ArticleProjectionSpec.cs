using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Entities;

namespace FakeAcad.Core.Specifications;

public class ArticleProjectionSpec : Specification<Article, ArticleDTO>
{
    public ArticleProjectionSpec(bool orderByCreatedAt = false) =>
        Query.Select(e => new ArticleDTO()
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Content = e.Content,
                UserId = e.UserId,
                Universities = e.Universities.Select(u => new UniversityDTO()
                {
                    Id = u.Id,
                    Name = u.Name,
                }).ToList(),
                Professors = e.Professors.Select(p => new ProfessorDTO()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                }).ToList(),
                
            })
            .OrderByDescending(x => x.CreatedAt, orderByCreatedAt);

    public ArticleProjectionSpec(Guid id) : this() => Query.Where(e => e.Id == id);
    
    public ArticleProjectionSpec(string? search) : this(true) // This constructor will call the first declared constructor with 'true' as the parameter. 
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Title, searchExpr)); // This is an example on how database specific expressions can be used via C# expressions.
        // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }

    public static ArticleProjectionSpec byUniversity(Guid universityId) {
        var spec = new ArticleProjectionSpec(false);
        spec.Query.Where(e => e.Universities.Any(u => u.Id == universityId));
        return spec;
    }

    public static ArticleProjectionSpec byProfessor(Guid professorId) {
        var spec = new ArticleProjectionSpec(false);
        spec.Query.Where(e => e.Professors.Any(p => p.Id == professorId));
        return spec;
    }
}