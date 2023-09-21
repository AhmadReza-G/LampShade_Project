namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string Role { get; set; }
        public string Fullname { get; set; }
        public string Mobile { get; set; }
        public string Username { get; set; }
        public string? ProfilePhoto { get; set; }
        public List<int> Permissions { get; set; }

        public AuthViewModel()
        {

        }
        public AuthViewModel(long id, long roleId, string fullname,
            string username, string role, string? profilePhoto, List<int> permissions, string mobile)
        {
            Id = id;
            RoleId = roleId;
            Fullname = fullname;
            Username = username;
            Role = role;
            ProfilePhoto = profilePhoto;
            Permissions = permissions;
            Mobile = mobile;
        }
    }
}