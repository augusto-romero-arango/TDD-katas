namespace TddKatas.SupermarketReceipt.Tests;

public record DescuentoPorPorcentaje(
    string Producto,
    decimal PorcentajeDescuento,
    TipoDescuento TipoDescuento) : IDescuento
{
    public (string Producto, (TipoDescuento TipoDescuento, decimal PorcentajeDescuento))? CrearDescuentoAAplicar (string producto, int cantidadComprada)
    {
        return  (Producto, (TipoDescuento, PorcentajeDescuento));
    }
}

public interface IDescuento
{
    TipoDescuento TipoDescuento { get; }
    string Producto { get; }
    
    (string Producto, (TipoDescuento TipoDescuento, decimal PorcentajeDescuento))? CrearDescuentoAAplicar (string producto, int cantidadComprada);
    
}

public record DescuentoPagaXLlevaY(
    string Producto,
    int UnidadesAComprar,
    int UnidadesGratis,
    TipoDescuento TipoDescuento) : IDescuento
{
    public (string Producto, (TipoDescuento TipoDescuento, decimal PorcentajeDescuento))? CrearDescuentoAAplicar(string producto, int cantidadComprada)
    {
        if (cantidadComprada % UnidadesAComprar == 0)
        {
           return (producto, (TipoDescuento, 1));
        }
        return null;
    }
}

public enum TipoDescuento
{
    Porcentaje,
    LlevaXPagaY
}

public class Recibo
{
    private readonly IDescuento[]? _descuentos =[];
    private readonly List<IDescuento> _descuentoPagaXLlevaIes = [];
    private readonly List<IDescuento> _descuentosPorPorcentaje = [];


    private readonly List<string> _productosFacturados = new();

    private readonly List<(string Producto, (TipoDescuento TipoDescuento, decimal PorcentajeDescuento) Descuento)>
        _descuentosAplicados = [];

    private readonly Dictionary<string, int> _precios = new()
    {
        {"Cepillo de dientes", 3000},
        {"Jabón", 2000}
    };

    public Recibo(IDescuento[]? descuentos = null)
    {
        
        if (descuentos == null) 
            return;
        
        _descuentos = descuentos;
        
        foreach (var descuento in descuentos)
        {
            if (descuento.TipoDescuento == TipoDescuento.LlevaXPagaY)
                _descuentoPagaXLlevaIes.Add(descuento);
            else
                _descuentosPorPorcentaje.Add(descuento);
        }
    }


    public void Adicionar(string producto)
    {
        if (producto == "")
            throw new ArgumentException("Debe ingresar un producto.");

        if (!_precios.ContainsKey(producto))
            throw new ArgumentException($"El producto {producto} no existe en el sistema.");


        _productosFacturados.Add(producto);

        AplicarDescuento(producto);
    }

    private void AplicarDescuento(string producto)
    {
        //TODO: Por cada tipo de descuento está saliendo un condicional
        
        _descuentos
            .Where(d => d.Producto == producto)
            .ToList()
            .ForEach(d =>
                {
                    if (d.TipoDescuento == TipoDescuento.Porcentaje)
                    {
                        _descuentosAplicados.Add(d.CrearDescuentoAAplicar(d.Producto, 0).Value);
                    }
                    else if (d.TipoDescuento == TipoDescuento.LlevaXPagaY)
                    {
                        var crearDescuentoAAplicar = d.CrearDescuentoAAplicar(d.Producto, _productosFacturados.Count(p => p == producto));
                        if (crearDescuentoAAplicar != null)
                            _descuentosAplicados.Add(crearDescuentoAAplicar.Value);
                    }
                }
                );

        // _descuentosPorPorcentaje
        //     .Where(d => d.TipoDescuento == TipoDescuento.Porcentaje)
        //     .Select(d => (DescuentoPorPorcentaje) d)
        //     .Where(d => d.Producto == producto)
        //     .ToList()
        //     .ForEach(d => { _descuentosAplicados.Add((d.Producto, (d.TipoDescuento, d.PorcentajeDescuento))); });
        //
        //
        // _descuentoPagaXLlevaIes
        //     .Where(d => d.TipoDescuento == TipoDescuento.LlevaXPagaY)
        //     .Select(d => (DescuentoPagaXLlevaY) d)
        //     .Where(x => x.Producto == producto)
        //     .ToList()
        //     .ForEach(d =>
        //         {
        //             var cantidadComprada = _productosFacturados.Count(x => x == producto);
        //             if (cantidadComprada % d.UnidadesAComprar == 0)
        //             {
        //                 var valueTuple = (producto, (d.TipoDescuento, 1));
        //                 _descuentosAplicados.Add(valueTuple);
        //             }
        //         }
        //     );
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
                    string formatoDescuento = descuento.Descuento.TipoDescuento == TipoDescuento.Porcentaje
                        ? $"{descuento.Descuento.PorcentajeDescuento:P0}"
                        : "2X1";

                    return
                        $"{descuento.Producto} ({formatoDescuento}): {descuento.Descuento.PorcentajeDescuento * -1 * _precios[descuento.Producto]:C0}";
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
            return (int) (precioTotal * descuento.PorcentajeDescuento);
        }).Sum();
    }
}