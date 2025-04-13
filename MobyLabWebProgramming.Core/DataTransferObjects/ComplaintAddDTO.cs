using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ComplaintAddDTO
{
    public ComplaintType ComplaintType { get; set; }
    public Severity Severity { get; set; }
    public string Name { get; set; } = null!;
    
    public Guid ArticleId { get; set; }
}