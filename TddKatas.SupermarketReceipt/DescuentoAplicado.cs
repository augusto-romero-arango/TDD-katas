using System.Globalization;

namespace TddKatas.SupermarketReceipt;

public record DescuentoAplicado(
    string Producto,
    string FormatoDescuento, 
    int ValorDescuento)
{
    public override string ToString()
    {
        return $"{Producto} ({FormatoDescuento}): {ValorDescuento.ToString("C0", new CultureInfo("es-CO"))}";
    }
}