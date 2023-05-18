using BlogManagement.Application.Contracts.ArticleCategory;

namespace BlogManagement.Application.Contracts.Article;

public class ArticleSearchModel
{
    public string Title { get; set; }
    public long CategoryId { get; set; }
}
