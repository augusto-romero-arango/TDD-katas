namespace TddKatas.SupermarketReceipt.Descuentos;

public record DescuentoPorPorcentaje(
    string Producto,
    decimal PorcentajeDescuento,
    TipoDescuento TipoDescuento = TipoDescuento.Porcentaje) : IDescuento
{
    public DescuentoAplicado[] DescuentoAAplicar(string producto, int cantidadComprada, int precio)
    {
        return
        [
            new DescuentoAplicado(Producto, $"{PorcentajeDescuento:P0}", (int)(PorcentajeDescuento * -1m * precio))
        ];
    }
}