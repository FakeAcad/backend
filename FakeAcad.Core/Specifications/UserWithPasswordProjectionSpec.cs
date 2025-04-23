using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Entities;

namespace FakeAcad.Core.Specifications;

public sealed class UserWithPasswordProjectionSpec : Specification<User, UserWithPasswordDTO>
{
    public UserWithPasswordProjectionSpec(bool orderByCreatedAt = false) =>
        Query.Select(e => new()
        {
            Id = e.Id,
            Email = e.Email,
            Name = e.Name,
            Role = e.Role,
            Password = e.Password
        })
        .OrderByDescending(x => x.CreatedAt, orderByCreatedAt);

    public UserWithPasswordProjectionSpec(Guid id) : this() => Query.Where(e => e.Id == id);

    public UserWithPasswordProjectionSpec(string email) : this(true) =>
        Query.Where(e => e.Email == email);
}
