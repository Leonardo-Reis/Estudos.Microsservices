using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var rabbitHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
var factory = new ConnectionFactory() { HostName = rabbitHost };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Mensagem recebida! {message}");
};

await channel.BasicConsumeAsync("SegundoTeste", false, consumer);

Console.ReadLine();