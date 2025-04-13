using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class UniversitySpec : Specification<University>
{
    public UniversitySpec(Guid id) => Query.Where(e => e.Id == id);
    public UniversitySpec(string name) => Query.Where(e => e.Name == name);
}