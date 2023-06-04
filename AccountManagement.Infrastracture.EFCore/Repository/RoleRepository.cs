using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastracture.EFCore;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository;
public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
{
    private readonly AccountContext _context;

    public RoleRepository(AccountContext context) : base(context)
    {
        _context = context;
    }

    public EditRole? GetDetails(long id)
    {
        var role = _context.Roles.Select(x => new EditRole
        {
            Id = x.Id,
            Name = x.Name,
            MappedPermissions = MapPermissions(x.Permissions)
        }).AsNoTracking()
        .FirstOrDefault(x => x.Id == id);

        role!.Permissions = role.MappedPermissions.Select(x => x.Code).ToList();

        return role;
    }

    private static List<PermissionDto> MapPermissions(IEnumerable<Permission> permissions)
    {
        return permissions.Select(x => new PermissionDto(x.Code, x.Name))
            .ToList();
    }

    public List<RoleViewModel> List()
    {
        return _context.Roles.Select(x => new RoleViewModel
        {
            Id = x.Id,
            Name = x.Name,
            CreationDate = x.CreationDate.ToFarsi()
        }).ToList();
    }
}
