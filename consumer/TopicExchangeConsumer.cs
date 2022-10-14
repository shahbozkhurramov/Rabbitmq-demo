using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace consumer
{
    public static class TopicExchangeConsumer
    {
        public static void ConsumeTopicExchange(this IModel channel)
        {
            channel.ExchangeDeclare("topic-exchange", ExchangeType.Topic);
            channel.QueueDeclare("topic-queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            channel.QueueBind("topic-queue", "topic-exchange", "topic-exchange-key.*");
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("topic-queue", true, consumer);
            Console.ReadLine();
        }
    }
}