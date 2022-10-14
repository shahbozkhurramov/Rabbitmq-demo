using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace consumer
{
    public static class DirectExchangeConsumer
    {
        public static void ConsumeDirectExchange(this IModel channel)
        {
            channel.ExchangeDeclare("direct-exchange", ExchangeType.Direct);
            channel.QueueDeclare("direct-queue",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
            channel.QueueBind("direct-queue", "direct-exchange", "direct-exchange-routing-key");
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("direct-queue", true, consumer);
            Console.ReadKey();
        }
        
    }
}