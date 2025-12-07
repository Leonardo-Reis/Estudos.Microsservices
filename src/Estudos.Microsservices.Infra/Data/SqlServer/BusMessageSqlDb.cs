namespace Estudos.Microsservices.Infra.Data.SqlServer;

public class BusMessageSqlDb(string message, DateTime dataInclusao)
{
    public int Id { get; set; }
    public string Message { get; set; } = message;
    public DateTime DataInclusao { get; set; } = dataInclusao;
}
