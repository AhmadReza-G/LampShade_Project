using _0_Framework.Domain;
using BlogManagement.Application.Contracts.ArticleCategory;

namespace BlogManagement.Domain.ArticleCategoryAgg;
public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
{
    List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    EditArticleCategory GetDetails(long id);
    string GetSlugBy(long id);
}
