using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Estudos.Microsservices.Consumer.Entities;

public class BusMessageDb
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime DataInclusao { get; set; }
}
