namespace TddKatas.SupermarketReceipt.Tests;

public class ReciboSpecification
{
    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_producto()
    {
        var recibo = new Reciboo();
        
        recibo.Adicionar("Cepillo de dientes");
        
        Assert.Equal(@"Factura
Cepillo de dientes: $3.000
TOTAL FACTURA: $3.000", recibo.ToString());
    }
}

public class Reciboo
{
    public void Adicionar(string producto)
    {
        throw new NotImplementedException();
    }
}