using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ComplaintDTO
{
    public Guid Id { get; set; }
    public ComplaintType ComplaintType { get; set; }
    public Severity Severity { get; set; }
    public string Name { get; set; } = null!;
    
    public Guid ArticleId { get; set; }
}