using Ardalis.Specification;
using FakeAcad.Core.Entities;

namespace FakeAcad.Core.Specifications;

public sealed class UniversitySpec : Specification<University>
{
    public UniversitySpec(Guid id) => Query.Where(e => e.Id == id);
    public UniversitySpec(string name) => Query.Where(e => e.Name == name);
}