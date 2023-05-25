using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastracture.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampshadeQuery.Query;
public class ArticleCategoryQuery : IArticleCategoryQuery
{
    private readonly BlogContext _context;

    public ArticleCategoryQuery(BlogContext context)
    {
        _context = context;
    }

    public List<ArticleCategoryQueryModel> GetArticleCategories()
    {
        return _context.ArticleCategories
            .Include(x => x.Articles)
            .Select(x => new ArticleCategoryQueryModel
            {
                Name = x.Name,
                Slug = x.Slug,
                ArticlesCount = x.Articles.Count
            }).ToList();
    }

    public ArticleCategoryQueryModel GetArticleCategoryBy(string slug)
    {
        var articleCategory = _context.ArticleCategories
            .Include(x => x.Articles)
            .Select(x => new ArticleCategoryQueryModel
            {
                Name = x.Name,
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Description = x.Description,
                CanonicalAddress = x.CanonicalAddress,
                Articles = MapArticles(x.Articles),
                ArticlesCount = x.Articles.Count
            }).FirstOrDefault(x => x.Slug == slug);
        if (!string.IsNullOrWhiteSpace(articleCategory.Keywords))
            articleCategory.KeywordList = articleCategory.Keywords
                .Split(",")
                .ToList();

        return articleCategory;
    }

    private static List<ArticleQueryModel> MapArticles(List<Article> articles)
    {
        return articles.Select(x => new ArticleQueryModel
        {
            Title = x.Title,
            Slug = x.Slug,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            PublishDate = x.PublishDate.ToFarsi(),
            ShortDescription = x.ShortDescription,
        }).ToList();
    }
}
