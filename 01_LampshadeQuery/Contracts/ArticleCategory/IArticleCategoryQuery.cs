namespace _01_LampshadeQuery.Contracts.ArticleCategory;
public interface IArticleCategoryQuery
{
    List<ArticleCategoryQueryModel> GetArticleCategories();
    ArticleCategoryQueryModel GetArticleCategoryBy(string slug);
}
