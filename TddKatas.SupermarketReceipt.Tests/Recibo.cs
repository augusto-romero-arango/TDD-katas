namespace TddKatas.SupermarketReceipt.Tests;

public record DescuentoAplicado(
    string Producto,
    TipoDescuento TipoDescuento,
    decimal PorcentajeDescuento,
    string FormatoDescuento,
    int Precio)
{
    public override string ToString()
    {
        return $"{Producto} ({FormatoDescuento}): {PorcentajeDescuento * -1 * Precio:C0}";
    }
}

public interface IDescuento
{
    TipoDescuento TipoDescuento { get; }
    string Producto { get; }

    DescuentoAplicado? DescuentoAAplicar(string producto, int cantidadComprada, int precio);
}

public record DescuentoPorPorcentaje(
    string Producto,
    decimal PorcentajeDescuento,
    TipoDescuento TipoDescuento = TipoDescuento.Porcentaje) : IDescuento
{
    public DescuentoAplicado? DescuentoAAplicar(string producto, int cantidadComprada, int precio)
    {
        return new DescuentoAplicado(Producto, TipoDescuento, PorcentajeDescuento, $"{PorcentajeDescuento:P0}", precio);
    }
}

public record DescuentoPagaXLlevaY(
    string Producto,
    int UnidadesAComprar,
    int UnidadesGratis,
    TipoDescuento TipoDescuento = TipoDescuento.LlevaXPagaY) : IDescuento
{
    public DescuentoAplicado? DescuentoAAplicar(string producto, int cantidadComprada, int precio)
    {
        if (cantidadComprada % UnidadesAComprar == 0)
        {
            return new DescuentoAplicado(producto, TipoDescuento, 1, $"{UnidadesAComprar}X{UnidadesAComprar - UnidadesGratis}", precio);
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

    private readonly List<DescuentoAplicado>
        _descuentosAplicadosCorrecto = [];

    
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
        
        var descuentoAplicado = descuentoAAplicarONull
            .DescuentoAAplicar(producto, cantidadComprada, _precios[producto]);
        if (descuentoAplicado == null)
            return; 
        _descuentosAplicadosCorrecto.Add(descuentoAplicado);
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
        if (_descuentosAplicadosCorrecto.Count == 0)
            return string.Empty;

        var encabezadoDescuentos = $"{Environment.NewLine}DESCUENTOS APLICADOS:{Environment.NewLine}";

        var detalleDescuentos = _descuentosAplicadosCorrecto
            .Select(descuento => descuento.ToString())
            .ToArray()
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
        return _descuentosAplicadosCorrecto
            .Select(d => (int) (_precios[d.Producto] * d.PorcentajeDescuento))
            .Sum();
    }
}