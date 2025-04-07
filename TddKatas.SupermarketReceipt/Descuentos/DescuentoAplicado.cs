namespace TddKatas.SupermarketReceipt.Descuentos;

public record DescuentoAplicado(
    string Producto,
    string FormatoDescuento, 
    int ValorDescuento)
{
    public override string ToString()
    {
        return $"{Producto} ({FormatoDescuento}): {ValorDescuento:C0}";
    }
}