namespace TddKatas.SupermarketReceipt.Tests;

public interface IDescuento
{
    TipoDescuento TipoDescuento { get; }
    string Producto { get; }

    (string Producto, (TipoDescuento TipoDescuento, decimal PorcentajeDescuento))? CrearDescuentoAAplicar(
        string producto, int cantidadComprada);
}

public record DescuentoPorPorcentaje(
    string Producto,
    decimal PorcentajeDescuento,
    TipoDescuento TipoDescuento) : IDescuento
{
    public (string Producto, (TipoDescuento TipoDescuento, decimal PorcentajeDescuento))? CrearDescuentoAAplicar(
        string producto, int cantidadComprada)
    {
        return (Producto, (TipoDescuento, PorcentajeDescuento));
    }
}

public record DescuentoPagaXLlevaY(
    string Producto,
    int UnidadesAComprar,
    TipoDescuento TipoDescuento) : IDescuento
{
    public (string Producto, (TipoDescuento TipoDescuento, decimal PorcentajeDescuento))? CrearDescuentoAAplicar(
        string producto, int cantidadComprada)
    {
        if (cantidadComprada % UnidadesAComprar == 0)
        {
            return (producto, (TipoDescuento, 1));
        }

        //TODO: No me gusta retornar null
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
    private readonly IDescuento[] _descuentos = [];

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
        var descuentoAAplicarONull = _descuentos.FirstOrDefault(d => d.Producto == producto);
        
        if (descuentoAAplicarONull == null)
            return;

        var cantidadComprada = _productosFacturados.Count(p => p == producto);
        
        var descuentoONull = descuentoAAplicarONull
            .CrearDescuentoAAplicar(producto, cantidadComprada);
        
        if (descuentoONull == null)
            return;
        
        _descuentosAplicados.Add(descuentoONull.Value);
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