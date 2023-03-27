using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ShopManagement.Application;
public class SlideApplication : ISlideApplication
{
    private readonly ISlideRepository _slideRepository;

    public SlideApplication(ISlideRepository slideRepository)
    {
        _slideRepository = slideRepository;
    }

    public OperationResult Create(CreateSlide command)
    {
        var operation = new OperationResult();
        var slide = new Slide(command.Picture, command.PictureAlt, command.PictureTitle,
            command.Heading, command.Title, command.Text, command.BtnText, command.Link);
        _slideRepository.Create(slide);
        _slideRepository.SaveChanges();
        return operation.Succeded();
    }

    public OperationResult Edit(EditSlide command)
    {
        var operation = new OperationResult();
        var slide = _slideRepository.GetBy(command.Id);
        if (slide is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        slide.Edit(command.Picture, command.PictureAlt, command.PictureTitle,
            command.Heading, command.Title, command.Text, command.BtnText, command.Link);
        _slideRepository.SaveChanges();
        return operation.Succeded();
    }

    public EditSlide GetDetails(long id)
    {
        return _slideRepository.GetDetails(id);
    }

    public List<SlideViewModel> GetList()
    {
        return _slideRepository.GetList();
    }

    public OperationResult Remove(long id)
    {
        var operation = new OperationResult();
        var slide = _slideRepository.GetBy(id);
        if (slide is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        slide.Remove();
        _slideRepository.SaveChanges();
        return operation.Succeded();
    }

    public OperationResult Restore(long id)
    {
        var operation = new OperationResult();
        var slide = _slideRepository.GetBy(id);
        if (slide is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        slide.Restore();
        _slideRepository.SaveChanges();
        return operation.Succeded();
    }
}
