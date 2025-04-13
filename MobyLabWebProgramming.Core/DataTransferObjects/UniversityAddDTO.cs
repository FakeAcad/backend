namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class UniversityAddDTO
{
    public string Name { get; set; }
    public ICollection<Guid> ProfessorsIds { get; set; } = new List<Guid>();
}