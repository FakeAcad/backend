using FakeAcad.Core.Entities;
using FakeAcad.Core.Enums;

namespace FakeAcad.Core.DataTransferObjects;

public class ProfessorDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Position Position { get; set; }
    public ICollection<Article> Articles { get; set; } = new List<Article>();
    public ICollection<University> Universities { get; set; } = new List<University>();
}