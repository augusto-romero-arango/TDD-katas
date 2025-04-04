namespace TddKatas.SupermarketReceipt.Tests;

public record DescuentoPorPorcentaje(
    string Producto,
    decimal PorcentajeDescuento,
    TipoDescuento TipoDescuento);

public record DescuentoPagaXLlevaY(
    string Producto,
    int UnidadesAComprar,
    int UnidadesGratis,
    TipoDescuento TipoDescuento)
{
}

public  enum TipoDescuento
{
    Porcentaje,
    LlevaXPagaY
}
public class Recibo
{
    private readonly Dictionary<string, (int UnidadesAComprar, int UnidadesGratis)> _descuentosPagaXLlevaY;
    private readonly DescuentoPagaXLlevaY[] _descuentoPagaXLlevaIes = [];
    private readonly DescuentoPorPorcentaje[] _descuentosGenerales = [];


    private readonly List<string> _productosFacturados = new();
    private readonly List<(string Producto, (TipoDescuento TipoDescuento, decimal PorcentajeDescuento) Descuento)> _descuentosAplicados = [];

    private readonly Dictionary<string, int> _precios = new()
    {
        {"Cepillo de dientes", 3000},
        {"Jabón", 2000}
    };


    //TODO: Debemos mejorar los constructores para que reciban todos los tipos de descuento
    
    
    public Recibo()
    {
        new Dictionary<string, decimal>();
        _descuentosPagaXLlevaY = new Dictionary<string, (int UnidadesAComprar, int UnidadesGratis)>();
    }

    public Recibo(DescuentoPorPorcentaje[] descuentosGenerales)
    {
        _descuentosGenerales = descuentosGenerales;
        _descuentosPagaXLlevaY = new Dictionary<string, (int UnidadesAComprar, int UnidadesGratis)>();
    }

    public Recibo(Dictionary<string, (int UnidadesAComprar, int UnidadesGratis)> descuentosPagaXLlevaY,
        DescuentoPagaXLlevaY[] descuentoPagaXLlevaIes)
    {
        _descuentosPagaXLlevaY = descuentosPagaXLlevaY;
        _descuentoPagaXLlevaIes = descuentoPagaXLlevaIes;
        new Dictionary<string, decimal>();
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
     
        _descuentosGenerales
            .Where(d => d.Producto == producto)
            .ToList()
            .ForEach(d =>
            {
                _descuentosAplicados.Add((d.Producto, (d.TipoDescuento, d.PorcentajeDescuento)));
            });
        

        // if (_descuentosPagaXLlevaY.TryGetValue(producto, out var descuento2X1))
        // {
        //     var cantidadComprada = _productosFacturados.Count(x => x == producto);
        //     if(cantidadComprada % descuento2X1.UnidadesAComprar == 0)
        //     {
        //         _descuentosAplicados.Add((producto, (TipoDescuento.LlevaXPagaY, 1)));
        //     }
        // }
        
        _descuentoPagaXLlevaIes
            .Where(x => x.Producto == producto)
            .ToList()
            .ForEach(d =>
                {
                    var cantidadComprada = _productosFacturados.Count(x => x == producto);
                    if(cantidadComprada % d.UnidadesAComprar == 0)
                    {
                        _descuentosAplicados.Add((producto, (d.TipoDescuento, 1)));
                    }
                }
                );
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
               string formatoDescuento = descuento.Descuento.TipoDescuento == TipoDescuento.Porcentaje ? $"{descuento.Descuento.PorcentajeDescuento:P0}" : "2X1";

               return $"{descuento.Producto} ({formatoDescuento}): {descuento.Descuento.PorcentajeDescuento * -1 * _precios[descuento.Producto]:C0}";
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
            var precioTotal = _precios[d.Producto];
            var descuento = d.Descuento;
            return (int) (precioTotal *  descuento.PorcentajeDescuento);
        }).Sum();
    }
}