namespace TddKatas.VendingMachine;

public class VendingMachine(List<Producto>? inventarioInicialDeProductos = null, List<Coin>? inventarioInicialDeMonedas = null)
{
    private static readonly Dictionary<Producto, decimal> ListaDePrecios = new()
    {
        {Producto.Chips, 0.5m},
        {Producto.Cola, 1m},
        {Producto.Candy, 0.65m}
    };

    private readonly List<Producto> _inventarioProductos = inventarioInicialDeProductos ?? [];
    private readonly Monedero _monedero = new (inventarioInicialDeMonedas);

    public VendingMachineRespuesta SeleccionarProducto(Producto producto)
    {
        var precio = ObtenerPrecioDe(producto);
        var totalIngresado = _monedero.CalcularValorIngresado();
        var tieneProducto = _inventarioProductos.Contains(producto);
        var puedeDarVueltas = _monedero.TryDarVueltas(totalIngresado, precio, out var vueltas);

        return (tieneProducto, totalIngresado, puedeDarVueltas) switch
        {
            (tieneProducto: false, _, _) => VendingMachineRespuesta.SoldOut(),
            (tieneProducto: true, var valorIngresado, _) when valorIngresado < precio => VendingMachineRespuesta.Price(precio),
            (tieneProducto: true, var valorIngresado, _) when valorIngresado == precio => DispensarProducto(producto, []),
            (tieneProducto: true, _, puedeDarVueltas: true) => DispensarProducto(producto, vueltas),
            _ => VendingMachineRespuesta.ExactChangeOnly()
        };
    }

    public VendingMachineRespuesta InsertarMoneda(Coin monedaIngresada)
    {
        _monedero.IngresarMonedasAInventario(monedaIngresada);

        return VendingMachineRespuesta.CurrentAmount(_monedero.CalcularValorIngresado());
    }

    public VendingMachineRespuesta RetornarMonedas()
    {
        var monedasARetornar = _monedero.RetornarMonedasRecienIngresadas();

        return VendingMachineRespuesta.InsertCoin(monedasARetornar);
    }

    private VendingMachineRespuesta DispensarProducto(Producto producto, Coin[] monedasRetornadas)
    {
        _inventarioProductos.Remove(producto);
        return VendingMachineRespuesta.ThankYou(producto, monedasRetornadas);
    }

    private static decimal ObtenerPrecioDe(Producto producto)
    {
        return ListaDePrecios[producto];
    }

}