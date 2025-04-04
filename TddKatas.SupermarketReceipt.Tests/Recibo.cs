namespace TddKatas.SupermarketReceipt.Tests;

public class Recibo
{
    private readonly List<string> _productosFacturados = new();
    private readonly Dictionary<string, decimal> _descuentosAplicados = new();

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

    public void Adicionar(string producto)
    {
        if (producto == "")
            throw new ArgumentException("Debe ingresar un producto.");

        if (!_precios.ContainsKey(producto))
            throw new ArgumentException($"El producto {producto} no existe en el sistema.");

        _productosFacturados.Add(producto);

        if (_descuentos.TryGetValue(producto, out var descuento))
            _descuentosAplicados.Add(producto, descuento);
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
                $"{descuento.Key} ({descuento.Value:P0}): {descuento.Value * -1 * _precios[descuento.Key]:C0}"
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
        return _productosFacturados
            .Select(producto =>
            {
                _descuentosAplicados.TryGetValue(producto, out var descuento);

                if (descuento != 0)
                    return (int) (_precios[producto] * (1 - descuento));

                return _precios[producto];
            })
            .Sum();
    }
}