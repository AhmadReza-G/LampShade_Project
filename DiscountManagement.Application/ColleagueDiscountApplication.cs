using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application;
public class ColleagueDiscountApplication : IColleagueDiscountApplication
{
    private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

    public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
    {
        _colleagueDiscountRepository = colleagueDiscountRepository;
    }

    public OperationResult Define(DefineColleagueDiscount command)
    {
        var operation = new OperationResult();

        if (_colleagueDiscountRepository.IsExists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var colleagueDiscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);

        _colleagueDiscountRepository.Create(colleagueDiscount);
        _colleagueDiscountRepository.SaveChanges();

        return operation.Succeded();
    }

    public OperationResult Edit(EditColleagueDiscount command)
    {
        var operation = new OperationResult();
        var colleagueDiscount = _colleagueDiscountRepository.GetBy(command.Id);

        if (colleagueDiscount is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (_colleagueDiscountRepository.IsExists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        colleagueDiscount.Edit(command.ProductId, command.DiscountRate);

        _colleagueDiscountRepository.SaveChanges();

        return operation.Succeded();
    }

    public EditColleagueDiscount GetDetails(long id)
    {
        return _colleagueDiscountRepository.GetDetails(id);
    }

    public OperationResult Remove(long id)
    {
        var operation = new OperationResult();
        var colleagueDiscount = _colleagueDiscountRepository.GetBy(id);

        if (colleagueDiscount is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        colleagueDiscount.Remove();

        _colleagueDiscountRepository.SaveChanges();

        return operation.Succeded();
    }

    public OperationResult Restore(long id)
    {
        var operation = new OperationResult();
        var colleagueDiscount = _colleagueDiscountRepository.GetBy(id);

        if (colleagueDiscount is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        colleagueDiscount.Restore();

        _colleagueDiscountRepository.SaveChanges();

        return operation.Succeded();
    }

    public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
    {
        return _colleagueDiscountRepository.Search(searchModel);
    }
}
