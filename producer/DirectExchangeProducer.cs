using System.Text;
using RabbitMQ.Client;

namespace producer
{
    public static class DirectExchange
    {
        public static void PublishDirectExchange(this IModel channel, string message)
        {
            channel.ExchangeDeclare(exchange: "direct-exchange",
                                    type: ExchangeType.Direct);

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "direct-exchange",
                                routingKey: "direct-exchange-routing-key",
                                basicProperties: null,
                                body: body);
        }
    }
}