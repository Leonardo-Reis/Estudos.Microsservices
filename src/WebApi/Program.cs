using DnsClient;
using Estudos.Microsservices.Contratos;
using MassTransit;
using MassTransit.Logging;
using MassTransit.RabbitMqTransport;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
//using RabbitMQ.Client;
using System.Text;

//var rabbitHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
//var factory = new ConnectionFactory() { HostName = rabbitHost };
//using var connection = await factory.CreateConnectionAsync();
//using var channel = await connection.CreateChannelAsync();

//await channel.QueueDeclareAsync(
//    queue: "SegundoTeste",
//    durable: true,
//    exclusive: false,
//    autoDelete: false,
//    arguments: null
//    );

var builder = WebApplication.CreateBuilder(args);

var rabbitHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST")
                 ?? "localhost";
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitHost, 5672, "/", h =>
        {
            h.Username(builder.Configuration["MessageBroker:Username"]!);
            h.Password(builder.Configuration["MessageBroker:Password"]!);
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("WebApi"))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .SetErrorStatusOnException()
        .AddHttpClientInstrumentation()
        .AddSource("MongoDB.Driver.Core.Extensions.DiagnosticSources")
        .AddSource(DiagnosticHeaders.DefaultListenerName)
        .AddOtlpExporter());

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", async (ISendEndpointProvider sendEndpointProvider) => {
    Console.WriteLine("Teste 3");
    var datetime = DateTime.Now;
    var message = $"Mensagem de teste! {datetime}";
    var busMessage = new BusMessage()
    {
        Message = $"Mensagem de teste! {datetime}"
    };
    var body = Encoding.UTF8.GetBytes(message);
    var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:SegundoTeste"));
    await endpoint.Send(busMessage);
    Console.WriteLine($"Enviado {datetime}");
    return "Hello World!";
});

app.MapControllers();

app.Run();
