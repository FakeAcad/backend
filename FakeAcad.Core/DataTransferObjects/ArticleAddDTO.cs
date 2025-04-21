using FakeAcad.Core.Entities;

namespace FakeAcad.Core.DataTransferObjects;

public class ArticleAddDTO
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Content { get; set; } = null!;
    public Guid UserId { get; set; }
    public ICollection<Guid> UniversitiesIds { get; set; } = new List<Guid>();
    public ICollection<Guid> ProfessorsIds { get; set; } = new List<Guid>();
}