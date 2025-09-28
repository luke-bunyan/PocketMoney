using PocketMoney.Integrations.Persistance;
using PocketMoney.Interfaces;
using PocketMoney.Models.Accounting;
using PocketMoney.Models.Accounting.Requests;
using PocketMoney.Models.Classification;

namespace PocketMoney.Services;

public class TransactionService(IAccountService accountService, IDataContextFactory dataContextFactory, IAllocationService allocationService) : ITransactionService
{
    public async Task<Transaction?> GetTransactionAsync(int transactionId)
    {
        var transaction = await dataContextFactory.Get<Transaction>().GetEntryAsync(transactionId);

        return transaction;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
    {
        var transactions = (await dataContextFactory.Get<Transaction>().GetAllEntriesAsync()).ToList();

        return transactions;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync(Account account)
    {
        var transactions = (await dataContextFactory.Get<Transaction>().GetAllEntriesAsync(
            new Dictionary<string, dynamic?>()
            {
                {"AccountId", account.AccountId}
            })).ToList();

        return transactions;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync(Category category)
    {
        var transactions = (await dataContextFactory.Get<Transaction>().GetAllEntriesAsync(
            new Dictionary<string, dynamic?>()
            {
                {"CategoryId", category.CategoryId}
            })).ToList();

        return transactions;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync(Vendor vendor)
    {
        return await dataContextFactory.Get<Transaction>().GetAllEntriesAsync(
            new Dictionary<string, dynamic?>()
            {
                {"VendorId", vendor.VendorId}
            });
    }

    public async Task<Transaction> CreateTransactionAsync(TransactionRequest transaction)
    {
        if (await accountService.GetAccountAsync(transaction.AccountId) == null)
        {
            throw new ArgumentException($"Account {transaction.AccountId} does not exist");
        }

        var transactionContext = dataContextFactory.Get<Transaction>();
        var createdTransaction = await transactionContext.SaveEntryAsync(new Transaction
        {
            AccountId = transaction.AccountId,
            Amount = transaction.Amount,
            CreatedDateTime = DateTime.UtcNow,
            VendorId = transaction.VendorId,
            CategoryId = transaction.CategoryId
        });

        if (transaction.Allocations.Any())
        {
            try
            {
                await UpdateTransactionAllocationAsync(createdTransaction, transaction.Allocations);
            }
            catch(Exception)
            {
                await RemoveTransaction(createdTransaction);
                throw;
            }

        }

        return await GetTransactionAsync(createdTransaction.TransactionId);
    }

    public async Task<Transaction> UpdateTransactionAllocationAsync(Transaction transaction, List<AllocationRequest> allocations)
    {
        var currentTransaction = await GetTransactionAsync(transaction.TransactionId) ?? throw new ArgumentException($"Transaction {transaction.TransactionId} does not exist");

        if (allocations != null && !allocations.Any()) {
            throw new ArgumentException($"Invalid allocations argument");
        }

        if (currentTransaction.Amount != allocations.Sum(x => x.Amount))
        {
            throw new ArgumentException($"The allocations do not cover the full cost of the transaction");
        }

        var allocationContext = dataContextFactory.Get<Allocation>();

        var currentAllocations = await allocationService.GetAllocationsAsync(transaction.TransactionId);

        if (currentAllocations.Any())
        {
            foreach (var allocation in currentAllocations)
            {
                await allocationContext.RemoveEntryAsync(allocation.AllocationId);
            }
        }

        foreach (var allocation in allocations)
        {
            await allocationContext.SaveEntryAsync(new Allocation
            {
                TransactionId = currentTransaction.TransactionId,
                Amount = allocation.Amount,
                ProductId = allocation.ProductId
            });
        }

        return await GetTransactionAsync(transaction.TransactionId);
    }

    public async Task RemoveTransaction(Transaction transaction)
    {
        await dataContextFactory.Get<Transaction>().RemoveEntryAsync(transaction.TransactionId);
    }

    private async Task<IEnumerable<Allocation>> GetAllocationsAsync(int transactionId)
    {
        return await dataContextFactory.Get<Allocation>().GetAllEntriesAsync(
            new Dictionary<string, dynamic?>()
            {
                {"TransactionId", transactionId}
            });
    }
}