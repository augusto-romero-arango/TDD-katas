namespace TddKatas.SupermarketReceipt.Tests;

public class ReciboSpecification
{
    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_cepillo_totalizar_en_3000()
    {
        var recibo = new Recibo();
        
        recibo.Adicionar("Cepillo de dientes");
        
        Assert.Equal(@"Factura
Cepillo de dientes: $ 3.000
TOTAL FACTURA: $ 3.000", recibo.ToString());
    }
    
    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_jabon_totalizar_en_2000()
    {
        var recibo = new Recibo();
        
        recibo.Adicionar("Jabón");
        
        Assert.Equal(@"Factura
Jabón: $ 2.000
TOTAL FACTURA: $ 2.000", recibo.ToString());
    }

    
}

public class Recibo
{
    private string? _productoFacturado;
    private Dictionary<string, int> _precios = new()
    {
        { "Cepillo de dientes", 3000 },
        { "Jabón", 2000 }
    };

    public void Adicionar(string? producto)
    {
        _productoFacturado = producto;
    }

    public override string ToString()
    {
        return $"""
               Factura
               {_productoFacturado}: {_precios[_productoFacturado]:C0}
               TOTAL FACTURA: {_precios[_productoFacturado]:C0}
               """;
    }
}