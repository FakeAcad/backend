using FakeAcad.Core.Entities;

namespace FakeAcad.Core.DataTransferObjects;

public class ArticleDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Content { get; set; } = null!;
    
    public Guid UserId { get; set; }
    
    public ICollection<UniversityDTO> Universities { get; set; } = new List<UniversityDTO>();
    public ICollection<ProfessorDTO> Professors { get; set; } = new List<ProfessorDTO>();
    public Guid ComplaintId { get; set; }
}