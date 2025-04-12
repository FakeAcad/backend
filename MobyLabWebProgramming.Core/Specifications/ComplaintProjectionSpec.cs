using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

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
}