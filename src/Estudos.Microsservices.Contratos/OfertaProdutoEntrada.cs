using System;
using System.Collections.Generic;
using System.Text;

namespace Estudos.Microsservices.Contratos;

public class OfertaProdutoEntrada
{
    public Produto Produto { get; set; }
    public decimal ValorOferecido { get; set; }
    public int NumeroParcercelas { get; set; }
}
