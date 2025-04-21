using FakeAcad.Core.Entities;

namespace FakeAcad.Core.DataTransferObjects;

public class UniversityDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Professor> Professors { get; set; } = new List<Professor>();
    public ICollection<Article> Articles { get; set; } = new List<Article>();
}