namespace PocketMoney.Integrations.Persistance;

public interface IDataContextFactory
{
    IDataContext<T> Get<T>() where T : class;
}