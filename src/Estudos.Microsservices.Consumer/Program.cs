using Estudos.Microsservices.Consumer;
using MassTransit;
using MassTransit.Logging;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddHostedService<Worker>();

var rabbitHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST")
                 ?? "localhost";

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<BusMessageConsumer>();

    busConfigurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitHost, 5672, "/", h =>
        {
            h.Username(builder.Configuration["MessageBroker:Username"]!);
            h.Password(builder.Configuration["MessageBroker:Password"]!);
        });

        cfg.ReceiveEndpoint("SegundoTeste", e =>
        {
            e.ConfigureConsumer<BusMessageConsumer>(context);
        });
        //cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("WebApi"))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddSource(DiagnosticHeaders.DefaultListenerName)
        .AddOtlpExporter());

var host = builder.Build();
await host.RunAsync();
