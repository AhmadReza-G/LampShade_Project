using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategories;

public class IndexModel : PageModel
{
    public ArticleCategorySearchModel SearchModel;
    public List<ArticleCategoryViewModel> ArticleCategories;

    private readonly IArticleCategoryApplication _articleCategoryApplication;

    public IndexModel(IArticleCategoryApplication articleCategoryApplication)
    {
        _articleCategoryApplication = articleCategoryApplication;
    }
    [NeedsPermission(BlogPermissions.ListArticleCategories)]
    public void OnGet(ArticleCategorySearchModel searchModel)
    {
        ArticleCategories = _articleCategoryApplication.Search(searchModel);
    }

    public IActionResult OnGetCreate()
    {
        return Partial("./Create", new CreateArticleCategory());
    }
    [NeedsPermission(BlogPermissions.CreateArticleCategory)]
    public JsonResult OnPostCreate(CreateArticleCategory command)
    {
        var result = _articleCategoryApplication.Create(command);
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var productCategory = _articleCategoryApplication.GetDetails(id);
        return Partial("Edit", productCategory);
    }
    [NeedsPermission(BlogPermissions.EditArticleCategory)]
    public JsonResult OnPostEdit(EditArticleCategory command)
    {
        var result = _articleCategoryApplication.Edit(command);
        return new JsonResult(result);
    }
}