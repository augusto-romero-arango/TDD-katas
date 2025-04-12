namespace TddKatas.VendingMachine;

public class VendingMachine(List<Producto>? inventarioInicialDeProductos = null, List<Coin>? saldoInicialDeCaja = null)
{
    private readonly Monedero _monedero = new (saldoInicialDeCaja);
    private readonly Inventario _inventario = new (inventarioInicialDeProductos);

    public VendingMachineRespuesta SeleccionarProducto(Producto producto)
    {
        var precio = Inventario.ObtenerPrecioDe(producto);
        var totalIngresado = _monedero.CalcularValorInsertado();
        var tieneProducto = _inventario.HayProductoEnInventario(producto);
        var puedeDarVueltas = _monedero.TryDarVueltas(totalIngresado, precio, out Coin[] vueltas);

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
        _monedero.InsertarMonedasAInventario(monedaIngresada);
        return VendingMachineRespuesta.CurrentAmount(_monedero.CalcularValorInsertado());
    }

    public VendingMachineRespuesta RetornarMonedas()
    {
        var monedasARetornar = _monedero.RetornarMonedasRecienIngresadas();
        return VendingMachineRespuesta.InsertCoin(monedasARetornar);
    }
    
    private VendingMachineRespuesta DispensarProducto(Producto producto, Coin[] monedasRetornadas)
    {
        _inventario.QuitarProductoDelInventario(producto);
        return VendingMachineRespuesta.ThankYou(producto, monedasRetornadas);
    }
}