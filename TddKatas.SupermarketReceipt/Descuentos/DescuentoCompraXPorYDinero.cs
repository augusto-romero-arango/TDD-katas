namespace TddKatas.SupermarketReceipt.Descuentos;

public record DescuentoCompraXPorYDinero(
    string Producto,
    int UnidadesAComprar,
    int ValorAPagar,
    TipoDescuento TipoDescuento = TipoDescuento.CompraXPorYDinero) : IDescuento
{
    public DescuentoAplicado[] DescuentoAAplicar(string producto, int cantidadComprada, int precio)
    {
        if (UnidadesAComprar == cantidadComprada)
        {
            return
            [
                new DescuentoAplicado(Producto, $"Combo {UnidadesAComprar}",
                    ValorAPagar - (precio * UnidadesAComprar))
            ];
        }

        return [];
    }
}