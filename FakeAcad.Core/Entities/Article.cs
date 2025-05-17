namespace FakeAcad.Core.Entities;

public class Article : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Content { get; set; } = null!;
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    public ICollection<University> Universities { get; set; } = new List<University>();
    public ICollection<Professor> Professors { get; set; } = new List<Professor>();
}
