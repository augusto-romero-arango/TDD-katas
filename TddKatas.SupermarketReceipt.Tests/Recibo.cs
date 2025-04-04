namespace TddKatas.SupermarketReceipt.Tests;

public class Recibo
{
    private readonly List<string> _productosFacturados = new();

    private readonly Dictionary<string, int> _precios = new()
    {
        {"Cepillo de dientes", 3000},
        {"Jabón", 2000}
    };

    public void Adicionar(string producto)
    {
        if (producto == "")
            throw new ArgumentException("Debe ingresar un producto.");

        if (!_precios.ContainsKey(producto))
            throw new ArgumentException($"El producto {producto} no existe en el sistema.");

        _productosFacturados.Add(producto);
    }

    public override string ToString()
    {
        return $"""
                Factura
                {CrearDetalleProductosDelRecibo()}
                TOTAL A PAGAR: {CalcularTotalRecibo():C0}
                """;
    }

    private string CrearDetalleProductosDelRecibo()
    {
        return  _productosFacturados
            .Select(producto => $"{producto}: {_precios[producto]:C0}")
            .ToArray()
            .Aggregate((detalleAcumulado, detallePorProducto)=> $"{detalleAcumulado}{Environment.NewLine}{detallePorProducto}");
    }

    private int CalcularTotalRecibo()
    {
        return _productosFacturados
            .Select(producto => _precios[producto])
            .Sum();
    }
}