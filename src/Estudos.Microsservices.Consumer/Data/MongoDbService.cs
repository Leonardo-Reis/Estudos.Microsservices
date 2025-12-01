using Estudos.Microsservices.Consumer.Entities;
using Estudos.Microsservices.Contratos;
using MongoDB.Driver;
using MongoDB.Driver.Core.Extensions.DiagnosticSources;

namespace Estudos.Microsservices.Consumer.Data;

public class MongoDbService
{
    private readonly IConfiguration _configuration;
    private readonly IMongoDatabase _database;


    public MongoDbService(IConfiguration configuration)
    {
        try
        {
            _configuration = configuration;

            var connectionString = _configuration.GetConnectionString("DbConnection");
            var mongoUrl = MongoUrl.Create(connectionString);
            var clientSettings = MongoClientSettings.FromUrl(mongoUrl);

            // Adicione esta linha crucial para habilitar a emissão de eventos de diagnóstico
            clientSettings.ClusterConfigurator = cb => cb.Subscribe(new DiagnosticsActivityEventSubscriber());

            var mongoClient = new MongoClient(clientSettings);
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            //var a = _database.GetCollection<BusMessageDb>("testecollection");
            //a.InsertOne(new BusMessageDb() { Message = "1234123" });
        }
        catch(Exception ex)
        {
            var b = ex.Message;
        }
        
    }

    public IMongoDatabase Database => _database;
}
