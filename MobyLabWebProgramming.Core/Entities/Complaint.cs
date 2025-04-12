using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

public class Complaint : BaseEntity
{
    public ComplaintType ComplaintType { get; set; }
    public Severity Severity { get; set; }
    public string Name { get; set; } = null!;
    
    public Guid ArticleId { get; set; }
    public Article Article { get; set; } = null!;
}
