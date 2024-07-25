namespace PocketMoney.Integrations.Persistance;

public interface IDataContext<T>
{
    IEnumerable<T> GetAllEntries();

    Task<IEnumerable<T>> GetAllEntriesAsync();

    IEnumerable<T> GetAllEntries(IDictionary<string, dynamic>? filters);

    Task<IEnumerable<T>> GetAllEntriesAsync(Dictionary<string, dynamic?> filters);

    T GetEntry(int entryId);

    Task<T> GetEntryAsync(int entryId);

    T SaveEntry(T entry);

    Task<T> SaveEntryAsync(T entry);

    void RemoveEntry(int entryId);

    Task RemoveEntryAsync(int entryId);

    T UpdateEntry(int entryId, IDictionary<string, dynamic> updates);

    Task<T> UpdateEntryAsync(int entryId, IDictionary<string, dynamic> updates);
}