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

            clientSettings.ClusterConfigurator = cb => cb.Subscribe(new DiagnosticsActivityEventSubscriber());

            var mongoClient = new MongoClient(clientSettings);
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }
        catch(Exception ex)
        {
            var b = ex.Message;
        }
        
    }

    public IMongoDatabase Database => _database;
}
