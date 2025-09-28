using PocketMoney.Models.Accounting;

namespace PocketMoney.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAllAccountsAsync();

    Task<Account> GetAccountAsync(int accountId);

    Task<Account> CreateAccountAsync(string name);

    Task DeleteAccountAsync(int accountId);

    Task<Account> CreateAccountAsync(Account account);

    Task<bool> DoesAccountExistAsync(Account account);
}