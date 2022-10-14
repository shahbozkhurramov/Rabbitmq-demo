using System.Text;
using RabbitMQ.Client;

namespace producer
{
    public static class FanoutExchangeProducer
    {
        public static void PublishFanoutExchange(this IModel channel, string message)
        {
            channel.ExchangeDeclare(exchange: "fanout-exchange",type: ExchangeType.Fanout);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            var body = Encoding.UTF8.GetBytes(message);
            
            channel.BasicPublish(exchange: "fanout-exchange",
                                routingKey: "fanout-exchange-routing-key",
                                basicProperties: properties,
                                body: body);
        }
    }
}