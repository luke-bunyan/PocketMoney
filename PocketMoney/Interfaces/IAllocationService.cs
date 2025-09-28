using PocketMoney.Models.Accounting;
using PocketMoney.Models.Accounting.Requests;

namespace PocketMoney.Interfaces;

public interface IAllocationService
{
    Task<List<Allocation>> GetAllocationsAsync(int transactionId);
    
    Task<IDictionary<int, List<Allocation>>> GetAllocationsAsync(IEnumerable<int> transactionIds);

    Task<List<Allocation>> SetAllocationsAsync(Transaction transaction, List<AllocationRequest>? allocations);
}