using _0_Framework.Infrastructure;

namespace CommentManagement.Configuration.Permissions;
public class CommentPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<PermissionDto>> Expose() => new()
    {
        ["کامنت"] = new List<PermissionDto>
        {
            new(CommentPermissions.AddComment, "افزودن کامنت"),
            new(CommentPermissions.ConfirmComment, "پذیرفتن کامنت"),
            new(CommentPermissions.CancelComment, "رد کامنت"),
            new(CommentPermissions.SearchComments, "جستجوی کامنت‌ها")
        }
    };
}