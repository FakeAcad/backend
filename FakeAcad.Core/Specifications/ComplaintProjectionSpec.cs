using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Entities;

namespace FakeAcad.Core.Specifications;

public sealed class ComplaintProjectionSpec : Specification<Complaint, ComplaintDTO>
{
    public ComplaintProjectionSpec(bool orderByCreatedAt = false) =>
        Query.Select(e => new ComplaintDTO()
            {
                Id = e.Id,
                ComplaintType = e.ComplaintType,
                Severity = e.Severity,
                Name = e.Name,
                ArticleId = e.ArticleId
            })
            .OrderByDescending(x => x.CreatedAt, orderByCreatedAt);

    public ComplaintProjectionSpec(Guid id) : this() => Query.Where(e => e.Id == id);
    
    public ComplaintProjectionSpec(string? search) : this(true) 
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr));
    }
}