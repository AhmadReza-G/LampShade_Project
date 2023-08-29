using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel(IArticleCategoryQuery articleCategoryQuery,
        IArticleQuery articleQuery) : PageModel
    {
        public List<ArticleQueryModel> LatestArticles = new();
        public ArticleCategoryQueryModel ArticleCategory = new();
        public List<ArticleCategoryQueryModel> ArticleCategories = new();

        private readonly IArticleQuery _articleQuery = articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery = articleCategoryQuery;


        public void OnGet(string id)
        {
            LatestArticles = _articleQuery.LatestArticles();
            ArticleCategory = _articleCategoryQuery.GetArticleCategoryBy(id);
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();
        }
    }
}