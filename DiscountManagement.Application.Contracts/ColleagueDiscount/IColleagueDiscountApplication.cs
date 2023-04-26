using _0_Framework.Application;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount;

public interface IColleagueDiscountApplication
{
    OperationResult Define(DefineColleagueDiscount command);
    OperationResult Edit(EditColleagueDiscount command);
    OperationResult Remove(long id);
    OperationResult Restore(long id);
    EditColleagueDiscount GetDetails(long id);
    List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
}