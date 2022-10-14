using System.Text;
using RabbitMQ.Client;

namespace producer
{
    public static class TopicExchangeProducer
    {
        public static void PublishTopicExchange(this IModel channel, string message)
        {
            channel.ExchangeDeclare(exchange: "topic-exchange",
                                    type: ExchangeType.Topic);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "topic-exchange",
                                routingKey: "topic-exchange-key.someValue",
                                basicProperties: properties,
                                body: body);
        }
    }
}