using RabbitMQ.Client;
using static MassTransit.MessageHeaders;

var rabbitHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
var factory = new ConnectionFactory() { HostName = rabbitHost };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();


//Host(args)
//    .ConfigureServices(services =>
//    {
//        services.AddHostedService<Worker>();
//    })
//    .Build()
//    .Run();

//builder.Services.AddMassTransit(busConfigurator =>
//{
//    busConfigurator.SetKebabCaseEndpointNameFormatter();

//    busConfigurator.UsingRabbitMq((context, cfg) =>
//    {
//        cfg.Host("", h =>
//        {
//            h.Username(builder.Configuration["MessageBroker:Username"]!);
//            h.Password(builder.Configuration["MessageBroker:Password"]!);
//        });

//        cfg.ConfigureEndpoints(context);
//    });
//});

//var consumer = new AsyncEventingBasicConsumer(channel);
//consumer.ReceivedAsync += async (model, eventArgs) =>
//{
//    var body = eventArgs.Body.ToArray();
//    var message = Encoding.UTF8.GetString(body);
//    Console.WriteLine($"Mensagem recebida! {message}");
//};

//await channel.BasicConsumeAsync("SegundoTeste", false, consumer);

Console.ReadLine();