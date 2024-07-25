using System.Text;
using RabbitMQ.Client;

namespace PocketMoney.Integrations.MessageBroker;

public class MessageBrokerContext(IConfiguration configuration) : IMessageBrokerContext
{
    private IModel? _channel;

    public void PublishMessage(string message, string exchange)
    {
        BuildConnection();

        _channel.BasicPublish(exchange: string.Empty,
            routingKey: null,
            basicProperties: null,
            body: Encoding.UTF8.GetBytes(message));
    }

    public void PublishMessage(string message, string exchange, string routingKey)
    {
        BuildConnection();

        _channel.BasicPublish(exchange: string.Empty,
            routingKey: routingKey,
            basicProperties: null,
            body: Encoding.UTF8.GetBytes(message));
    }

    private void BuildConnection()
    {
        if (_channel == null)
        {
            var hostname = configuration["queueServerAddress"];
            var connectionFactory = new ConnectionFactory { HostName = hostname };
            var connection = connectionFactory.CreateConnection();
            _channel = connection.CreateModel();
        }
    }
}