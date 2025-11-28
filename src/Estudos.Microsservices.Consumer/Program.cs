using Estudos.Microsservices.Consumer;
using MassTransit;
using MassTransit.Logging;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Estudos.Microsservices.Consumer.Setup;

var builder = Host.CreateApplicationBuilder(args);

var rabbitHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST")
                 ?? "localhost";

builder.ConfigureMassTransitService(rabbitHost);

builder.ConfigureOpenTelemetry(rabbitHost);

var host = builder.Build();
await host.RunAsync();