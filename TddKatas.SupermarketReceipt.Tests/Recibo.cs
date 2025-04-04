namespace TddKatas.SupermarketReceipt.Tests;

public class Recibo
{
    private enum TipoDescuento
    {
        Porcentaje,
        LlevaXPagaY
    }

    private readonly Dictionary<string, (int UnidadesAComprar, int UnidadesGratis)> _descuentosPagaXLlevaY;
    private readonly List<string> _productosFacturados = new();
    private readonly List<(string, (TipoDescuento, decimal))> _descuentosAplicados = new();

    private readonly Dictionary<string, int> _precios = new()
    {
        {"Cepillo de dientes", 3000},
        {"Jabón", 2000}
    };

    private readonly Dictionary<String, decimal> _descuentosPorPorcentaje;


    //TODO: Debemos mejorar los constructores para que reciban todos los tipos de descuento
    public Recibo()
    {
        _descuentosPorPorcentaje = new Dictionary<string, decimal>();
        _descuentosPagaXLlevaY = new Dictionary<string, (int UnidadesAComprar, int UnidadesGratis)>();
    }

    public Recibo(Dictionary<string, decimal> descuentosPorPorcentaje)
    {
        _descuentosPorPorcentaje = descuentosPorPorcentaje;
        _descuentosPagaXLlevaY = new Dictionary<string, (int UnidadesAComprar, int UnidadesGratis)>();
    }

    public Recibo(Dictionary<string, (int UnidadesAComprar, int UnidadesGratis)> descuentosPagaXLlevaY)
    {
        _descuentosPagaXLlevaY = descuentosPagaXLlevaY;
        _descuentosPorPorcentaje = new Dictionary<string, decimal>();
    }

    public void Adicionar(string producto)
    {
        if (producto == "")
            throw new ArgumentException("Debe ingresar un producto.");

        if (!_precios.ContainsKey(producto))
            throw new ArgumentException($"El producto {producto} no existe en el sistema.");

        
        _productosFacturados.Add(producto);

        //TODO: La lógica de aplciación de desceuntos está an el método Adicionar
        //TODO: Por cada tipo de descuento está saliendo un condicional
        if (_descuentosPorPorcentaje.TryGetValue(producto, out var descuento))
            _descuentosAplicados.Add((producto, (TipoDescuento.Porcentaje, descuento)));

        if (_descuentosPagaXLlevaY.TryGetValue(producto, out var descuento2X1))
        {
            var cantidadComprada = _productosFacturados.Count(x => x == producto);
            if(cantidadComprada % descuento2X1.UnidadesAComprar == 0)
            {
                _descuentosAplicados.Add((producto, (TipoDescuento.LlevaXPagaY, 1)));
            }
        }
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
            { 
                //TODO: Dependiendo del tipo de descunento se debe aplicar un formato diferente
                //TODO: El nombre de los ítems de las tuplas no son descriptivos
               string formatoDescuento = descuento.Item2.Item1 == TipoDescuento.Porcentaje ? $"{descuento.Item2.Item2:P0}" : "2X1";

               return $"{descuento.Item1} ({formatoDescuento}): {descuento.Item2.Item2 * -1 * _precios[descuento.Item1]:C0}";
            }
                
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
            //Los nombres Item1 e Item2 de las tuplas no son descriptivos
            var precioTotal = _precios[d.Item1];
            var descuento = d.Item2;
            return (int) (precioTotal *  descuento.Item2);
        }).Sum();
    }
}