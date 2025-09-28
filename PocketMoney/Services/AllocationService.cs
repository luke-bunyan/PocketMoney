using PocketMoney.Interfaces;
using PocketMoney.Models.Accounting;
using PocketMoney.Models.Accounting.Requests;

namespace PocketMoney.Services;

public class AllocationService : IAllocationService
{
    public async Task<List<Allocation>> GetAllocationsAsync(int transactionId)
    {
        throw new NotImplementedException();
    }

    public async Task<IDictionary<int, List<Allocation>>> GetAllocationsAsync(IEnumerable<int> transactionIds)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Allocation>> SetAllocationsAsync(Transaction transaction, List<AllocationRequest>? allocations)
    {
        throw new NotImplementedException();
    }
}