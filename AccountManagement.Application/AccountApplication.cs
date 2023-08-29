using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using Framework.Application;

namespace AccountManagement.Application;
public class AccountApplication : IAccountApplication
{
    private readonly IAccountRepository _accountRepository;
    private readonly IFileUploader _fileUploader;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthHelper _authHelper;
    private readonly IRoleRepository _roleRepository;
    public AccountApplication(IAccountRepository accountRepository, IFileUploader fileUploader,
        IPasswordHasher passwordHasher, IAuthHelper authHelper, IRoleRepository roleRepository)
    {
        _accountRepository = accountRepository;
        _fileUploader = fileUploader;
        _passwordHasher = passwordHasher;
        _authHelper = authHelper;
        _roleRepository = roleRepository;
    }

    public OperationResult ChangePassword(ChangePassword command)
    {
        var operation = new OperationResult();
        var account = _accountRepository.GetBy(command.Id);
        if (account is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (command.Password != command.RePassword)
            return operation.Failed(ApplicationMessages.PasswordNotMatch);


        var password = _passwordHasher.Hash(command.Password);
        account.ChangePassword(password);

        _accountRepository.SaveChanges();
        return operation.Succeded();
    }

    public OperationResult Register(RegisterAccount command)
    {
        var operation = new OperationResult();

        if (_accountRepository.IsExists(x => x.Username == command.Username || x.Mobile == command.Mobile))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var picturePath = $"profilePhotos";
        var fileName = _fileUploader.Upload(command.ProfilePhoto, picturePath);
        var password = _passwordHasher.Hash(command.Password);
        var account = new Account(command.Fullname, command.Username, password, command.RoleId, command.Mobile, fileName);

        _accountRepository.Create(account);
        _accountRepository.SaveChanges();

        return operation.Succeded();
    }

    public OperationResult Edit(EditAccount command)
    {
        var operation = new OperationResult();
        var account = _accountRepository.GetBy(command.Id);
        if (account is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (_accountRepository.IsExists(x =>
        (x.Username == command.Username || x.Mobile == command.Mobile)
        && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var picturePath = $"profilePhotos";
        var fileName = _fileUploader.Upload(command.ProfilePhoto, picturePath);
        account.Edit(command.Fullname, command.Username, command.RoleId, command.Mobile, fileName);

        _accountRepository.SaveChanges();
        return operation.Succeded();
    }

    public EditAccount GetDetails(long id)
    {
        return _accountRepository.GetDetails(id);
    }

    public OperationResult Login(Login command)
    {
        var operation = new OperationResult();
        var account = _accountRepository.GetBy(command.Username);
        if (account is null)
            return operation.Failed(ApplicationMessages.WrongUserPass);

        (bool Verified, bool NeedsUpgrade) result = _passwordHasher.Check(account.Password, command.Password);
        if (!result.Verified)
            return operation.Failed(ApplicationMessages.WrongUserPass);

        var permissions = _roleRepository.GetBy(account.RoleId)
            .Permissions
            .Select(x => x.Code)
            .ToList();

        var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Fullname,
            account.Username, Roles.GetRoleBy(account.RoleId), account.ProfilePhoto, permissions);
        _authHelper.Signin(authViewModel);

        return operation.Succeded();
    }

    public void Logout()
    {
        _authHelper.SignOut();
    }

    public List<AccountViewModel> Search(AccountSearchModel searchModel)
    {
        return _accountRepository.Search(searchModel);
    }

    public List<AccountViewModel> GetAccounts()
    {
        return _accountRepository.GetAccounts();
    }
}
