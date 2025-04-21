using Ardalis.Specification;
using FakeAcad.Core.Entities;

namespace FakeAcad.Core.Specifications;

public sealed class ArticleSpec : Specification<Article>
{
    public ArticleSpec(Guid id) => Query.Where(e => e.Id == id);
}