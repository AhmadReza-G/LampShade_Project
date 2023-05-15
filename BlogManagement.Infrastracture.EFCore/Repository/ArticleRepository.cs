using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastracture.EFCore.Repository;
public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
{
    private readonly BlogContext _context;

    public ArticleRepository(BlogContext context) : base(context)
    {
        _context = context;
    }

    public EditArticle GetDetails(long id)
    {
        return _context.Articles.Select(x => new EditArticle
        {
            Id = x.Id,
            CategoryId = x.CategoryId,
            Description = x.Description,
            CanonicalAddress = x.CanonicalAddress,
            Keywords = x.Keywords,
            MetaDescription = x.MetaDescription,
            Title = x.Title,
            Slug = x.Slug,
            ShortDescription = x.ShortDescription,
            PublishDate = x.PublishDate.ToFarsi(),
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle
        }).AsNoTracking().FirstOrDefault(x => x.Id == id);
    }

    public Article GetWithCategory(long id)
    {
        return _context.Articles.Include(x => x.Category).AsNoTracking().FirstOrDefault(x => x.Id == id);
    }

    public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
    {
        var query = _context.Articles.Include(x => x.Category).Select(x => new ArticleViewModel
        {
            Id = x.Id,
            Title = x.Title,
            ShortDescription = x.ShortDescription,
            PublishDate = x.PublishDate.ToFarsi(),
            Picture = x.Picture,
            Category = x.Category.Name
        }).AsNoTracking();
        if (!string.IsNullOrWhiteSpace(searchModel.Title))
            query = query.Where(x => x.Title.Contains(searchModel.Title));

        if (searchModel.CategoryId > 0)
            query = query.Where(x => x.CategoryId == searchModel.CategoryId);

        return query.OrderByDescending(x => x.Id).ToList();
    }
}
