using _0_Framework.Infrastructure;

namespace AccountManagement.Application.Contracts.Role;

public class EditRole : CreateRole
{
    public long Id { get; set; }
    public List<PermissionDto> MappedPermissions { get; set; }
}
