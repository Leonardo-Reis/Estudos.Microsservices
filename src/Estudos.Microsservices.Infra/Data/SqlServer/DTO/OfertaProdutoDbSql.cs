namespace Estudos.Microsservices.Infra.Data.SqlServer.DTO;

public class OfertaProdutoDbSql
{
    public Produto Produto { get; set; }
    public decimal ValorOferecido { get; set; }
    public int NumeroParcercelas { get; set; }
}
