using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles;

public class IndexModel : PageModel
{
    public ArticleSearchModel SearchModel;
    public List<ArticleViewModel> Articles;
    public SelectList ArticleCategories;

    private readonly IArticleApplication _articleApplication;
    private readonly IArticleCategoryApplication _articleCategoryApplication;

    public IndexModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
    {
        _articleApplication = articleApplication;
        _articleCategoryApplication = articleCategoryApplication;
    }
    [NeedsPermission(BlogPermissions.SearchArticles)]
    public void OnGet(ArticleSearchModel searchModel)
    {
        ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
        Articles = _articleApplication.Search(searchModel);
    }
}