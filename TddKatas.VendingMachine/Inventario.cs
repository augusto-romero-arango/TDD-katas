namespace TddKatas.VendingMachine;

public class Inventario(List<Producto>? inventarioInicial = null)
{
    private readonly List<Producto> _inventarioProductos = inventarioInicial ?? [];

    private static readonly Dictionary<Producto, decimal> ListaDePrecios = new()
    {
        {Producto.Chips, 0.5m},
        {Producto.Cola, 1m},
        {Producto.Candy, 0.65m}
    };

    public void QuitarProductoDelInventario(Producto producto)
    {
        _inventarioProductos.Remove(producto);
    }

    public static decimal ObtenerPrecioDe(Producto producto)
    {
        return ListaDePrecios[producto];
    }

    public bool HayProductoEnInventario(Producto producto)
    {
        return _inventarioProductos.Contains(producto);
    }
}