using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application;
public class SlideApplication : ISlideApplication
{
    private readonly IFileUploader _fileUploader;
    private readonly ISlideRepository _slideRepository;

    public SlideApplication(ISlideRepository slideRepository, IFileUploader fileUploader)
    {
        _slideRepository = slideRepository;
        _fileUploader = fileUploader;
    }

    public OperationResult Create(CreateSlide command)
    {
        var operation = new OperationResult();
        var fileName = _fileUploader.Upload(command.Picture, "Slides");
        var slide = new Slide(fileName, command.PictureAlt, command.PictureTitle,
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

        var fileName = _fileUploader.Upload(command.Picture, "Slides");

        slide.Edit(fileName, command.PictureAlt, command.PictureTitle,
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
