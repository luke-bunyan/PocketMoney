namespace PocketMoney.Interfaces;

public interface IAdapter<TResponse, in TRequest> where TResponse : class where TRequest : class
{
    Task<TResponse> Adapt(TRequest request);

    Task<IEnumerable<TResponse>> Adapt(IEnumerable<TRequest> request);
}