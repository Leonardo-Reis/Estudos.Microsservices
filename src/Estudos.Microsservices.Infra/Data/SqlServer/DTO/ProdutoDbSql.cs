namespace Estudos.Microsservices.Infra.Data.SqlServer.DTO;

public class ProdutoDbSql
{
    public string Nome { get; set; }
    public TipoProduto Tipo { get; set; }
    public decimal Preco { get; set; }
    public IEnumerable<string> Tags { get; set; }
}
