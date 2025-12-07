namespace Estudos.Microsservices.Contratos;

public class Produto
{
    public string Nome { get; set; }
    public TipoProduto Tipo { get; set; }
    public decimal Preco { get; set; }
    public IEnumerable<string> Tags { get; set; }
}
