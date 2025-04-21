namespace FakeAcad.Core.Entities;

public class University : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<Article> Articles { get; set; } = new List<Article>();
    public ICollection<Professor> Professors { get; set; } = new List<Professor>();
}