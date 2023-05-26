using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.Comment;
using BlogManagement.Infrastracture.EFCore;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastracture.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampshadeQuery.Query;
public class ArticleQuery : IArticleQuery
{
    private readonly BlogContext _context;
    private readonly CommentContext _commentContext;

    public ArticleQuery(BlogContext context, CommentContext commentContext)
    {
        _context = context;
        _commentContext = commentContext;
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
                ShortDescription = x.ShortDescription,
                Id = x.Id
            }).FirstOrDefault(x => x.Slug == slug);

        if (!string.IsNullOrWhiteSpace(article.Keywords))
            article.KeywordList = article.Keywords.Split(",").ToList();

        var comments = _commentContext.Comments
            .Where(x => x.IsConfirmed)
            .Where(x => !x.IsCanceled)
            .Where(x => x.Type == ((int)CommentType.Article))
            .Where(x => x.OwnerRecordId == article.Id)
            .Select(x => new CommentQueryModel
            {
                Id = x.Id,
                Message = x.Message,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi(),
                ParentId = x.ParentId
            })
            .ToList();

        foreach (var comment in comments)
        {
            if (comment.ParentId > 0)
                comment.ParentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
        }

        article.Comments = comments;

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
