using Estudos.Microsservices.Consumer.Data;
using Estudos.Microsservices.Consumer.Entities;
using Estudos.Microsservices.Contratos;
using System.Text.Json;
using MassTransit;
namespace Estudos.Microsservices.Consumer;

public sealed class BusMessageConsumer(MongoDbService mongoService) : IConsumer<BusMessage>
{
    private readonly MongoDbService _mongoService = mongoService;

    public async Task Consume(ConsumeContext<BusMessage> context)
    {
        Console.WriteLine($"Mensagem recebida! {context.Message.Message}");
        var collection = _mongoService.Database.GetCollection<BusMessageDb>("BusMessage");
        await collection.InsertOneAsync(new BusMessageDb() { Message = context.Message.Message, DataInclusao = DateTime.Now });
    }
}
