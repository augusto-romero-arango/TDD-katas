namespace TddKatas.SupermarketReceipt.Tests;

public class ReciboSpecification
{
    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_producto()
    {
        var recibo = new Recibo();
        
        recibo.Adicionar("Cepillo de dientes");
        
        Assert.Equal(@"Factura
Cepillo de dientes: $3.000
TOTAL FACTURA: $3.000", recibo.ToString());
    }
}

public class Recibo
{
    private string? _productoFacturado;

    public void Adicionar(string? producto)
    {
        _productoFacturado = producto;
    }

    public override string ToString()
    {
        return $"""
               Factura
               {_productoFacturado}: $3.000
               TOTAL FACTURA: $3.000
               """;
    }
}