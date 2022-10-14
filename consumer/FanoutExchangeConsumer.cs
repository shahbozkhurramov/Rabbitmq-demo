using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace consumer
{
    public static class FanoutExchangeConsumer
    {
        public static void ConsumeFanoutExchange(this IModel channel)
        {
            channel.ExchangeDeclare("fanout-exchange", ExchangeType.Fanout);
            channel.QueueDeclare("fanout-queue",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);


            channel.QueueBind("fanout-queue", "fanout-exchange", "fanout-exchange-routing-key");
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("fanout-queue", true, consumer);
            Console.ReadKey();
        }
    }
}