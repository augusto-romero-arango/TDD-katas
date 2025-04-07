namespace TddKatas.SupermarketReceipt.Descuentos;

public record DescuentoPagaXLlevaY(
    string Producto,
    int UnidadesAComprar,
    int UnidadesGratis
    ) : IDescuento
{
    public DescuentoAplicado[] DescuentoAAplicar(string producto, int cantidadComprada, int precio)
    {
        if (cantidadComprada % UnidadesAComprar != 0)
            return [];

        return Enumerable.Repeat(
                new DescuentoAplicado(producto,
                    $"{UnidadesAComprar}X{UnidadesAComprar - UnidadesGratis}", precio * -1), 
                UnidadesGratis)
            .ToArray();
    }
}