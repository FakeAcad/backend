using Ardalis.Specification;
using FakeAcad.Core.Entities;

namespace FakeAcad.Core.Specifications;

public sealed class ComplaintSpec : Specification<Complaint>
{
    public ComplaintSpec(Guid id) => Query.Where(e => e.Id == id);
    
    public ComplaintSpec(string name) => Query.Where(e => e.Name == name);
}