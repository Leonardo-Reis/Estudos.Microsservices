using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using MassTransit.Logging;

namespace Estudos.Microsservices.Consumer.Setup
{
    public static class SetupExtensions
    {
        public static void ConfigureMassTransitService(this HostApplicationBuilder builder, string rabbitHost)
        {
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
                });
            });
        }

        public static void ConfigureOpenTelemetry(this HostApplicationBuilder builder, string rabbitHost)
        {
            builder.Services.AddOpenTelemetry()
                    .ConfigureResource(resource => resource.AddService("BrokerConsumer"))
                    .WithTracing(tracing => tracing
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .SetErrorStatusOnException()
                        .AddSource("MongoDB.Driver.Core.Extensions.DiagnosticSources")
                        .AddSource(DiagnosticHeaders.DefaultListenerName)
                        .AddOtlpExporter());

        }
    }
}
