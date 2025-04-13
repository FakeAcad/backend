using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ProfessorSpec : Specification<Professor>
{
    public ProfessorSpec(Guid id) => Query.Where(e => e.Id == id);
    
    public ProfessorSpec(string firstName, string lastName) => Query.Where(e => e.FirstName == firstName && e.LastName == lastName);
}