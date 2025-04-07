namespace TddKatas.SupermarketReceipt.Descuentos;

public record DescuentoCompraXPorYDinero(
    string Producto,
    int UnidadesAComprar,
    int ValorAPagar) : IDescuento
{
    public DescuentoAplicado[] DescuentoAAplicar(string producto, int cantidadComprada, int precio)
    {
        if (cantidadComprada % UnidadesAComprar == 0)
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