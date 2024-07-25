using PocketMoney.Exceptions;
using PocketMoney.Integrations.Persistance;
using PocketMoney.Interfaces;
using PocketMoney.Models.Accounting;

namespace PocketMoney.Services;

public class AccountService(IDataContextFactory dataContextFactory) : IAccountService
{
    public Task<IEnumerable<Account>> GetAllAccountsAsync()
    {
        return dataContextFactory.Get<Account>().GetAllEntriesAsync();
    }

    public async Task<Account> GetAccountAsync(int accountId)
    {
        try
        {
            return await dataContextFactory.Get<Account>().GetEntryAsync(accountId);
        }
        catch(DataContextException)
        {
            throw new ArgumentException($"{accountId} not found");
        }
    }

    public async Task<Account> CreateAccountAsync(string name)
    {
        if (await DoesAccountExistAsync(new Account {Name = name})) throw new ArgumentException($"{name} already exists");

        return await dataContextFactory.Get<Account>().SaveEntryAsync(new Account
        {
            Name = name
        });
    }

    public async Task<Account> CreateAccountAsync(Account account)
    {
        if (await DoesAccountExistAsync(account)) throw new ArgumentException($"{account.Name} already exists");

        return await dataContextFactory.Get<Account>().SaveEntryAsync(account);
    }

    public async Task DeleteAccountAsync(int accountId)
    {
        var account = await GetAccountAsync(accountId) ?? throw new ArgumentException($"AccountId {accountId} does not exist");

        await dataContextFactory.Get<Account>().RemoveEntryAsync(account.AccountId);
    }

    private async Task<bool> DoesAccountExistAsync(Account account)
    {
        //TODO: Update this to filter dynamically based passed object
        return (await dataContextFactory.Get<Account>().GetAllEntriesAsync(new Dictionary<string, dynamic>()
        {
            {"Name", account.Name}
        })).Any();
    }
}