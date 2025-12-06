using DnsClient;
using Estudos.Microsservices.Contratos;
using MassTransit;
using MassTransit.Logging;
using MassTransit.RabbitMqTransport;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Text;


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

app.MapControllers();

app.Run();
