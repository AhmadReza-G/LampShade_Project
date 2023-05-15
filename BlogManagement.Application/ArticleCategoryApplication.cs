using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Application;
public class ArticleCategoryApplication : IArticleCategoryApplication
{
    private readonly IFileUploader _fileUploader;
    private readonly IArticleCategoryRepository _articleCategoryRepository;

    public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
    {
        _articleCategoryRepository = articleCategoryRepository;
        _fileUploader = fileUploader;
    }

    public OperationResult Create(CreateArticleCategory command)
    {
        var operationResult = new OperationResult();
        if (_articleCategoryRepository.IsExists(x => x.Name == command.Name))
            return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
        var slug = command.Slug.Slugify();
        var picturePath = $"{slug}";
        var fileName = _fileUploader.Upload(command.Picture, picturePath);
        var article = new ArticleCategory(command.Name, command.Description, fileName,
            command.PictureAlt, command.PictureTitle, slug, command.Keywords,
            command.MetaDescription, command.CanonicalAddress, command.ShowOrder);

        _articleCategoryRepository.Create(article);
        _articleCategoryRepository.SaveChanges();

        return operationResult.Succeded();
    }

    public OperationResult Edit(EditArticleCategory command)
    {
        var operationResult = new OperationResult();

        var article = _articleCategoryRepository.GetBy(command.Id);
        if (article is null)
            return operationResult.Failed(ApplicationMessages.RecordNotFound);

        if (_articleCategoryRepository.IsExists(x => x.Name == command.Name && x.Id != command.Id))
            return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
        var slug = command.Slug.Slugify();
        var picturePath = $"{slug}";
        var fileName = _fileUploader.Upload(command.Picture, picturePath);
        article.Edit(command.Name, command.Description, fileName,
            command.PictureAlt, command.PictureTitle, slug, command.Keywords,
            command.MetaDescription, command.CanonicalAddress, command.ShowOrder);

        _articleCategoryRepository.SaveChanges();

        return operationResult.Succeded();
    }

    public EditArticleCategory GetDetails(long id)
    {
        return _articleCategoryRepository.GetDetails(id);
    }

    public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
    {
        return _articleCategoryRepository.Search(searchModel);
    }
}
