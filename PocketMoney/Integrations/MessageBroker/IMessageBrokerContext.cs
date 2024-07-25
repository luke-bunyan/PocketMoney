namespace PocketMoney.Integrations.MessageBroker;

public interface IMessageBrokerContext
{
    void PublishMessage(string message, string exchange);
}