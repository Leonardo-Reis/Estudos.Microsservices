using Estudos.Microsservices.Infra.Data.MongoDb;
using Estudos.Microsservices.Consumer.Setup;

var builder = Host.CreateApplicationBuilder(args);

var rabbitHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST")
                 ?? "localhost";

builder.Services.AddSingleton<MongoDbService>();

builder.ConfigureMassTransitService(rabbitHost);

builder.ConfigureOpenTelemetry(rabbitHost);

var host = builder.Build();
await host.RunAsync();