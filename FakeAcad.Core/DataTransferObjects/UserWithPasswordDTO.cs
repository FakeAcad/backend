using FakeAcad.Core.Enums;

namespace FakeAcad.Core.DataTransferObjects;

public class UserWithPasswordDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserRoleEnum Role { get; set; }
}
