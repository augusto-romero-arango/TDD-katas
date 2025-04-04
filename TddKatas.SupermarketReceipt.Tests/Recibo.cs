namespace TddKatas.SupermarketReceipt.Tests;

public class Recibo
{
    private readonly List<string> _productosFacturados = new();
    private readonly List<(string, decimal)> _descuentosAplicados = new();

    private readonly Dictionary<string, int> _precios = new()
    {
        {"Cepillo de dientes", 3000},
        {"Jabón", 2000}
    };

    private readonly Dictionary<String, decimal> _descuentos;


    public Recibo()
    {
        _descuentos = new Dictionary<string, decimal>();
    }

    public Recibo(Dictionary<string, decimal> descuentos)
    {
        _descuentos = descuentos;
    }

    public Recibo(object descuento2X1)
    {
        throw new NotImplementedException();
    }

    public void Adicionar(string producto)
    {
        if (producto == "")
            throw new ArgumentException("Debe ingresar un producto.");

        if (!_precios.ContainsKey(producto))
            throw new ArgumentException($"El producto {producto} no existe en el sistema.");

        _productosFacturados.Add(producto);

        if (_descuentos.TryGetValue(producto, out var descuento))
            _descuentosAplicados.Add((producto, descuento));
    }

    public override string ToString()
    {
        return $"""
                Factura
                {CrearDetalleProductosDelRecibo()}{CrearDetallesDescuentos()}
                TOTAL A PAGAR: {CalcularTotalRecibo():C0}
                """;
    }

    private string CrearDetallesDescuentos()
    {
        if (_descuentosAplicados.Count == 0)
            return string.Empty;

        var encabezadoDescuentos = $"{Environment.NewLine}DESCUENTOS APLICADOS:{Environment.NewLine}";

        var detalleDescuentos = _descuentosAplicados
            .Select(descuento =>
                $"{descuento.Item1} ({descuento.Item2:P0}): {descuento.Item2 * -1 * _precios[descuento.Item1]:C0}"
            ).ToArray()
            .Aggregate((acumulado, detalle) => $"{acumulado}{Environment.NewLine}{detalle}");
        
        return encabezadoDescuentos + detalleDescuentos;
    }

    private string CrearDetalleProductosDelRecibo()
    {
        return _productosFacturados
            .Select(producto => $"{producto}: {_precios[producto]:C0}")
            .ToArray()
            .Aggregate((acumulado, detalle) =>
                $"{acumulado}{Environment.NewLine}{detalle}");
    }

    private int CalcularTotalRecibo()
    {
        return CalcularTotalSinDescuentos() - CalcularTotalDescuentos();
    }

    private int CalcularTotalSinDescuentos()
    {
        return _productosFacturados
            .Select(producto => _precios[producto])
            .Sum();
    }

    private int CalcularTotalDescuentos()
    {
        return _descuentosAplicados.Select(d =>
        {
            var precioTotal = _precios[d.Item1];
            var descuento = d.Item2;
            return (int) (precioTotal *  descuento);
        }).Sum();
    }
}