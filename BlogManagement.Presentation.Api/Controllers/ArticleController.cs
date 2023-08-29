using _01_LampshadeQuery.Contracts.Article;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.Presentation.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly IArticleQuery _articleQuery;

    public ArticleController(IArticleQuery articleQuery)
    {
        _articleQuery = articleQuery;
    }

    [HttpGet]
    public List<ArticleQueryModel> GetLatestArticles()
    {
        return _articleQuery.LatestArticles();
    }
}
