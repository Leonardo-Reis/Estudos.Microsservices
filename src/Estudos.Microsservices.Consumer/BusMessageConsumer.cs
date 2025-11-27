using Estudos.Microsservices.Contratos;
using MassTransit;
namespace Estudos.Microsservices.Consumer;

public sealed class BusMessageConsumer : IConsumer<BusMessage>
{
    public Task Consume(ConsumeContext<BusMessage> context)
    {
        Console.WriteLine($"Mensagem recebida! {context.Message.Message}");
        return Task.CompletedTask;
    }
}
