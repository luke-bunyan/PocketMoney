using PocketMoney.Models.Accounting;
using PocketMoney.Models.Accounting.Requests;
using PocketMoney.Models.Classification;

namespace PocketMoney.Interfaces;

public interface ITransactionService
{
    Task<Transaction?> GetTransactionAsync(int transactionId);

    Task<IEnumerable<Transaction>> GetTransactionsAsync(Account account);

    Task<IEnumerable<Transaction>> GetTransactionsAsync(Vendor vendor);

    Task<IEnumerable<Transaction>> GetTransactionsAsync(Category category);

    Task<IEnumerable<Transaction>> GetTransactionsAsync();

    Task<Transaction> CreateTransactionAsync(TransactionRequest transaction);

    Task RemoveTransaction(Transaction transaction);
}
