using _0_Framework.Domain;
using BlogManagement.Domain.ArticleAgg;

namespace BlogManagement.Domain.ArticleCategoryAgg;
public class ArticleCategory : EntityBase
{
    public ArticleCategory(string name, string? description, string? picture, string? pictureAlt,
        string? pictureTitle, string slug, string keywords, string metaDescription,
        string canonicalAddress, int showOrder)
    {
        Name = name;
        Description = description;
        Picture = picture;
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        Slug = slug;
        Keywords = keywords;
        MetaDescription = metaDescription;
        CanonicalAddress = canonicalAddress;
        ShowOrder = showOrder;
    }

    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? Picture { get; private set; }
    public string? PictureAlt { get; private set; }
    public string? PictureTitle { get; private set; }
    public string Slug { get; private set; }
    public string Keywords { get; private set; }
    public string MetaDescription { get; private set; }
    public string? CanonicalAddress { get; private set; }
    public int ShowOrder { get; private set; }
    public List<Article> Articles { get; private set; }

    public void Edit(string name, string? description, string? picture, string? pictureAlt,
        string? pictureTitle, string slug, string keywords, string metaDescription,
        string canonicalAddress, int showOrder)
    {
        Name = name;
        Description = description;
        if (!string.IsNullOrWhiteSpace(picture))
            Picture = picture;
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        Slug = slug;
        Keywords = keywords;
        MetaDescription = metaDescription;
        CanonicalAddress = canonicalAddress;
        ShowOrder = showOrder;
    }
}
