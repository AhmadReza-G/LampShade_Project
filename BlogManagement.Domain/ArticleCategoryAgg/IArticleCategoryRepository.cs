using _0_Framework.Domain;
using BlogManagement.Application.Contracts.ArticleCategory;

namespace BlogManagement.Domain.ArticleCategoryAgg;
public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
{
    List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    EditArticleCategory GetDetails(long id);
    List<ArticleCategoryViewModel> GetArticleCategories();
    string GetSlugBy(long id);
}
