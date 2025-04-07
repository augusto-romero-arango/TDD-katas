namespace TddKatas.SupermarketReceipt.Tests;

public record DescuentoAplicado(
    string Producto,
    string FormatoDescuento, 
    int ValorDescuento)
{
    public override string ToString()
    {
        return $"{Producto} ({FormatoDescuento}): {ValorDescuento:C0}";
    }
}

public interface IDescuento
{
    TipoDescuento TipoDescuento { get; }
    string Producto { get; }
    DescuentoAplicado[] DescuentoAAplicar(string producto, int cantidadComprada, int precio);
}

public record DescuentoCompraXPorYDinero(
    string Producto,
    int UnidadesAComprar,
    int ValorAPagar,
    TipoDescuento TipoDescuento = TipoDescuento.CompraXPorYDinero) : IDescuento
{
    public DescuentoAplicado[] DescuentoAAplicar(string producto, int cantidadComprada, int precio)
    {
        if (UnidadesAComprar == cantidadComprada)
        {
            return
            [
                new DescuentoAplicado(Producto, $"Combo {UnidadesAComprar}",
                    ValorAPagar - (precio * UnidadesAComprar))
            ];
        }

        return [];
    }
}

public record DescuentoPorPorcentaje(
    string Producto,
    decimal PorcentajeDescuento,
    TipoDescuento TipoDescuento = TipoDescuento.Porcentaje) : IDescuento
{
    public DescuentoAplicado[] DescuentoAAplicar(string producto, int cantidadComprada, int precio)
    {
        return
        [
            new DescuentoAplicado(Producto, $"{PorcentajeDescuento:P0}", (int)(PorcentajeDescuento * -1m * precio))
        ];
    }
}

public record DescuentoPagaXLlevaY(
    string Producto,
    int UnidadesAComprar,
    int UnidadesGratis,
    TipoDescuento TipoDescuento = TipoDescuento.LlevaXPagaY) : IDescuento
{
    public DescuentoAplicado[] DescuentoAAplicar(string producto, int cantidadComprada, int precio)
    {
        if (cantidadComprada % UnidadesAComprar != 0)
            return [];

        return Enumerable.Repeat(
                new DescuentoAplicado(producto,
                    $"{UnidadesAComprar}X{UnidadesAComprar - UnidadesGratis}", precio * -1), 
                UnidadesGratis)
            .ToArray();
    }
}

public enum TipoDescuento
{
    Porcentaje,
    LlevaXPagaY,
    CompraXPorYDinero
}

public class Recibo
{
    private readonly IDescuento[] _descuentos = [];

    private readonly List<string> _productosFacturados = [];

    private readonly List<DescuentoAplicado>
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

        var descuentoAplicado = descuentoAAplicarONull
            .DescuentoAAplicar(producto, cantidadComprada, _precios[producto]);

        _descuentosAplicados.AddRange(descuentoAplicado);
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
        return _descuentosAplicados
            .Select(d => d.ValorDescuento * -1)
            .Sum();
    }
}