using _0_Framework.Infrastructure;

namespace BlogManagement.Configuration.Permissions;
internal class BlogPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<PermissionDto>> Expose() => new()
    {
        ["مقاله"] = new List<PermissionDto>
        {
            new(BlogPermissions.CreateArticle, "ایجاد مقاله"),
            new(BlogPermissions.EditArticle, "ویرایش مقاله"),
            new(BlogPermissions.SearchArticles, "جستجوی مقالات")
        },
        ["گروه مقاله"] = new List<PermissionDto>
        {
            new(BlogPermissions.CreateArticleCategory, "ایجاد گروه مقاله"),
            new(BlogPermissions.EditArticleCategory, "ویرایش گروه مقاله"),
            new(BlogPermissions.ListArticleCategories, "لیست گروه‌های مقاله"),
            new(BlogPermissions.SearchArticleCategories, "جستجوی گروه‌های ‌مقاله")
        }
    };
}