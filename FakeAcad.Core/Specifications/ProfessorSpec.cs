using Ardalis.Specification;
using FakeAcad.Core.Entities;

namespace FakeAcad.Core.Specifications;

public sealed class ProfessorSpec : Specification<Professor>
{
    public ProfessorSpec(Guid id) => Query.Where(e => e.Id == id);
    
    public ProfessorSpec(string firstName, string lastName) => Query.Where(e => e.FirstName == firstName && e.LastName == lastName);
}