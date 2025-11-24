using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RabbitMQ.Client;
using System.Text;

var rabbitHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
var factory = new ConnectionFactory() { HostName = rabbitHost };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(
    queue: "SegundoTeste",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null
    );

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("WebApi"))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddSource("RabbitMQ.Client")
        .AddOtlpExporter());

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", async () => {
    Console.WriteLine("Teste 3");
    var datetime = DateTime.Now;
    var message = $"Mensagem de teste! {datetime}";
    var body = Encoding.UTF8.GetBytes(message);
    await channel.BasicPublishAsync(
        exchange: "",
        routingKey: "SegundoTeste",
        mandatory: true,
        basicProperties: new BasicProperties { Persistent = true },
        body: body
        );
    Console.WriteLine($"Enviado {datetime}");
    return "Hello World!";
});

app.MapControllers();

app.Run();
