using System.Runtime;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class UniversityProjectionSpec : Specification<University, UniversityDTO>
    {
    public UniversityProjectionSpec(bool orderByCreatedAt = false) =>
        Query.Select(e => new UniversityDTO()
            {
                Id = e.Id,
                Name = e.Name,
                Professors = e.Professors,
                Articles = e.Articles,
            })
            .OrderByDescending(x => x.CreatedAt, orderByCreatedAt);

    public UniversityProjectionSpec(Guid id) : this() => Query.Where(e => e.Id == id);
    
    public UniversityProjectionSpec(string? search) : this(true) // This constructor will call the first declared constructor with 'true' as the parameter. 
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr)); // This is an example on how database specific expressions can be used via C# expressions.
        // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }
}