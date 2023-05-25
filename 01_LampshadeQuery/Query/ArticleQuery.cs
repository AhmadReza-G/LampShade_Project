using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Article;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastracture.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampshadeQuery.Query;
public class ArticleQuery : IArticleQuery
{
    private readonly BlogContext _context;

    public ArticleQuery(BlogContext context)
    {
        _context = context;
    }

    public ArticleQueryModel GetArticleDetails(string slug)
    {
        var article = _context.Articles
            .Where(x => x.PublishDate <= DateTime.Now)
            .Select(x => new ArticleQueryModel
            {
                Title = x.Title,
                Slug = x.Slug,
                CategoryName = x.Category.Name,
                CanonicalAddress = x.CanonicalAddress,
                CategorySlug = x.Category.Slug,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription
            }).FirstOrDefault(x => x.Slug == slug);

        if (!string.IsNullOrWhiteSpace(article.Keywords))
            article.KeywordList = article.Keywords.Split(",").ToList();

        return article;
    }

    public List<ArticleQueryModel> LatestArticles()
    {
        return _context.Articles
            .Where(x => x.PublishDate <= DateTime.Now)
            .Select(x => new ArticleQueryModel
            {
                Title = x.Title,
                PictureAlt = x.PictureAlt,
                ShortDescription = x.ShortDescription,
                PictureTitle = x.PictureTitle,
                Picture = x.Picture,
                PublishDate = x.PublishDate.ToFarsi(),
                Slug = x.Slug
            }).AsNoTracking()
            .ToList();
    }
}
