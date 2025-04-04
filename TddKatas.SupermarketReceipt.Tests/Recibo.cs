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
        var descuentos = string.Empty;
        if(_descuentosAplicados.Any())
            descuentos = Environment.NewLine+_descuentosAplicados.Select( d => 
                $"{d.Key} ({d.Value:P0}): {d.Value*-1*_precios[d.Key]:C0}"
            ).ToArray()
                .Aggregate((total, detalle) => $"{total}{Environment.NewLine}{detalle}");
            // descuentos = $"{Environment.NewLine}{_descuentosAplicados.First().Key} ({_descuentosAplicados.First().Value:P0}): {_descuentosAplicados.First().Value*-1*_precios[_descuentosAplicados.First().Key]:C0}";
        
            
            
        return $"""
                Factura
                {CrearDetalleProductosDelRecibo()}{descuentos}
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