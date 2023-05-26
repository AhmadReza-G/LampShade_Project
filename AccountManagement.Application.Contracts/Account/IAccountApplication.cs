using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account;
public interface IAccountApplication
{
    OperationResult Create(CreateAccount command);
    OperationResult Edit(EditAccount command);
    OperationResult ChangePassword(ChangePassword command);
    EditAccount GetDetails(long id);
    List<AccountViewModel> Search(AccountSearchModel searchModel);
    OperationResult Login(Login command);
    void Logout();
}
