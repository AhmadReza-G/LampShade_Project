using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles;

public class EditModel : PageModel
{
    public EditArticle Command;
    public SelectList ArticleCategories;

    private readonly IArticleApplication _articleApplication;
    private readonly IArticleCategoryApplication _articleCategoryApplication;

    public EditModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
    {
        _articleApplication = articleApplication;
        _articleCategoryApplication = articleCategoryApplication;
    }
    [NeedsPermission(BlogPermissions.EditArticle)]
    public void OnGet(long id)
    {
        Command = _articleApplication.GetDetails(id);
        ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
    }
    [NeedsPermission(BlogPermissions.EditArticle)]
    public IActionResult OnPost(EditArticle command)
    {
        var result = _articleApplication.Edit(command);
        return RedirectToPage("./Index");
    }
}