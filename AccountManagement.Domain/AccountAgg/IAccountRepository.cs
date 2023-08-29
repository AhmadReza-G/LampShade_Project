﻿using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Account;

namespace AccountManagement.Domain.AccountAgg;
public interface IAccountRepository : IRepository<long, Account>
{
    Account GetBy(string username);
    EditAccount GetDetails(long id);
    List<AccountViewModel> Search(AccountSearchModel searchModel);
    List<AccountViewModel> GetAccounts();
}
