using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using CommentManagement.Application.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel(IArticleQuery articleQuery,
        IArticleCategoryQuery articleCategoryQuery,
        ICommentApplication commentApplication) : PageModel
    {
        public ArticleQueryModel Article = new();
        public List<ArticleQueryModel> LatestArticles = new();
        public List<ArticleCategoryQueryModel> ArticleCategories = new();
        private readonly IArticleQuery _articleQuery = articleQuery;
        private readonly ICommentApplication _commentApplication = commentApplication;
        private readonly IArticleCategoryQuery _articleCategoryQuery = articleCategoryQuery;

        public void OnGet(string id)
        {
            Article = _articleQuery.GetArticleDetails(id);
            LatestArticles = _articleQuery.LatestArticles();
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();
        }

        public IActionResult OnPost(AddComment command, string articleSlug)
        {
            command.Type = (int)CommentType.Article;
            var result = _commentApplication.Add(command);
            return RedirectToPage("/Article", new { Id = articleSlug });
        }
    }
}