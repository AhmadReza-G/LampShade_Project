using _0_Framework.Application;
using System.Collections;

namespace AccountManagement.Application.Contracts.Account;
public interface IAccountApplication
{
    OperationResult Register(RegisterAccount command);
    OperationResult Edit(EditAccount command);
    OperationResult ChangePassword(ChangePassword command);
    EditAccount GetDetails(long id);
    List<AccountViewModel> Search(AccountSearchModel searchModel);
    OperationResult Login(Login command);
    void Logout();
    List<AccountViewModel> GetAccounts();
    AccountViewModel? GetAccountBy(long id);
}
