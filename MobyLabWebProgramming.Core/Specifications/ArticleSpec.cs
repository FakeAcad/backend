using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ArticleSpec : Specification<Article>
{
    public ArticleSpec(Guid id) => Query.Where(e => e.Id == id);
}