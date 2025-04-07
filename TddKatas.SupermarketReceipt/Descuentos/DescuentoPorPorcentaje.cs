namespace TddKatas.SupermarketReceipt.Descuentos;

public record DescuentoPorPorcentaje(
    string Producto,
    decimal PorcentajeDescuento
    ) : IDescuento
{
    public DescuentoAplicado[] DescuentoAAplicar(string producto, int cantidadComprada, int precio)
    {
        return
        [
            new DescuentoAplicado(Producto, $"{PorcentajeDescuento:P0}", (int)(PorcentajeDescuento * -1m * precio))
        ];
    }
}