using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ComplaintSpec : Specification<Complaint>
{
    public ComplaintSpec(Guid id) => Query.Where(e => e.Id == id);
    
    public ComplaintSpec(string name) => Query.Where(e => e.Name == name);
}