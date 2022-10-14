using System.Text;
using producer;
using RabbitMQ.Client;

public class Program
{
    static void Main(string[] args)
    {
        var uri = new Uri("amqp://guest:guest@localhost:5672");
        var factory = new ConnectionFactory() { Uri = uri };
        using var connection = factory.CreateConnection();;
        using var channel = connection.CreateModel();

        channel.PublishDirectExchange("Hello World!");
        channel.PublishFanoutExchange("Hello World!");
    }
}
