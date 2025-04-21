using FakeAcad.Core.Entities;
using FakeAcad.Core.Enums;

namespace FakeAcad.Core.DataTransferObjects;

public class ProfessorAddDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Position Position { get; set; }
    public ICollection<Guid> UniversitiesIds { get; set; } = new List<Guid>();
}