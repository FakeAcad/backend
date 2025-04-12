using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

public class Professor : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Position Position { get; set; }
    public ICollection<Article> Articles { get; set; } = new List<Article>();
    public ICollection<University> Universities { get; set; } = new List<University>();
}
