using _0_Framework.Domain;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Domain.RoleAgg;
public class Role : EntityBase
{
    public string Name { get; private set; }
    public List<Account> Accounts { get; private set; }
    public List<Permission> Permissions { get; private set; }
    public Role(string name, List<Permission> permissions)
    {
        Name = name;
        Permissions = permissions;
    }
    protected Role()
    {
        
    }

    public void Edit(string name, List<Permission> permissions)
    {
        Name = name;
        Permissions = permissions;
    }
}
