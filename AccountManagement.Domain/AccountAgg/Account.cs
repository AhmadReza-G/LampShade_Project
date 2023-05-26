using _0_Framework.Domain;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.AccountAgg;
public class Account : EntityBase
{
    public string Fullname { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public long RoleId { get; private set; }
    public Role Role { get; private set; }
    public string Mobile { get; private set; }
    public string? ProfilePhoto { get; private set; }
    public Account(string fullname, string username, string password, long roleId, string mobile, string profilePhoto)
    {
        Fullname = fullname;
        Username = username;
        Password = password;
        RoleId = roleId;
        Mobile = mobile;
        ProfilePhoto = profilePhoto;
    }
    public void Edit(string fullname, string username, long roleId, string mobile, string profilePhoto)
    {
        Fullname = fullname;
        Username = username;
        RoleId = roleId;
        Mobile = mobile;
        if (!string.IsNullOrWhiteSpace(profilePhoto))
            ProfilePhoto = profilePhoto;
    }
    public void ChangePassword(string password) => Password = password;
}
