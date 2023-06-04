using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles;

public class CreateModel : PageModel
{
    public CreateArticle? Command;
    public SelectList ArticleCategories;

    private readonly IArticleApplication _articleApplication;
    private readonly IArticleCategoryApplication _articleCategoryApplication;

    public CreateModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
    {
        _articleApplication = articleApplication;
        _articleCategoryApplication = articleCategoryApplication;
    }
    [NeedsPermission(BlogPermissions.CreateArticle)]
    public void OnGet()
    {
        ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
    }
    //public void OnGet(CreateArticle? command)
    //{
    //    ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
    //    Command = command;
    //}
    [NeedsPermission(BlogPermissions.CreateArticle)]
    public IActionResult OnPost(CreateArticle command)
    {
        //if(!ModelState.IsValid)
        //{
        //    return RedirectToPage("./Create", command);
        //}
        var result = _articleApplication.Create(command);
        return RedirectToPage("./Index");
    }
}