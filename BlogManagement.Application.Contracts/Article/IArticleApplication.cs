using _0_Framework.Application;

namespace BlogManagement.Application.Contracts.Article;
public interface IArticleApplication
{
    OperationResult Create(CreateArticle command);
    OperationResult Edit(EditArticle command);
    EditArticle GetDetails(long id);
    List<ArticleViewModel> Search(ArticleSearchModel searchModel);
}
