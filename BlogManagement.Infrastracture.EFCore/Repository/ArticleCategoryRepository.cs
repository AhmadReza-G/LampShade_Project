using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastracture.EFCore.Repository;
public class ArticleCategoryRepository : RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
{
    private readonly BlogContext _context;

    public ArticleCategoryRepository(BlogContext context) : base(context)
    {
        _context = context;
    }

    public List<ArticleCategoryViewModel> GetArticleCategories()
    {
        return _context.ArticleCategories
            .Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).AsNoTracking()
            .ToList();
    }

    public EditArticleCategory GetDetails(long id)
    {
        return _context.ArticleCategories
            .Select(x => new EditArticleCategory
            {
                Id = id,
                CanonicalAddress = x.CanonicalAddress,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Name = x.Name,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShowOrder = x.ShowOrder,
                Slug = x.Slug
            }).AsNoTracking()
        .FirstOrDefault(x => x.Id == id);
    }

    public string GetSlugBy(long id)
    {
        return _context.ArticleCategories
            .Select(x => new { x.Id, x.Slug })
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id).Slug;
    }

    public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
    {
        var query = _context.ArticleCategories.Select(x => new ArticleCategoryViewModel
        {
            Id = x.Id,
            CreationDate = x.CreationDate.ToFarsi(),
            Name = x.Name,
            Picture = x.Picture,
            Description = x.Description,
            ShowOrder = x.ShowOrder
        }).AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchModel.Name))
            query = query.Where(x => x.Name.Contains(searchModel.Name));

        return query.OrderByDescending(x => x.ShowOrder).ToList();
    }
}
