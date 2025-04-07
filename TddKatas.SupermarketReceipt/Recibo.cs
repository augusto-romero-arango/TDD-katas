using TddKatas.SupermarketReceipt.Descuentos;

namespace TddKatas.SupermarketReceipt;

public class Recibo
{
    public const string TextoDescuentosAplicados = "DESCUENTOS APLICADOS:";
    public const string TextoTotalAPagar = "TOTAL A PAGAR:";
    public const string TextoEncabezadoRecibo = "Factura";

    private readonly IDescuento[] _descuentos = [];

    private readonly List<string> _productosFacturados = [];
    private readonly List<DescuentoAplicado> _descuentosAplicados = [];

    // Si necesitáramos cambiar la lista de precios, la podríamos inyectar en el constructor
    private readonly ListaPrecios _listaPrecios = new ();

    public Recibo(IDescuento[]? descuentos = null)
    {
        if (descuentos == null)
            return;

        _descuentos = descuentos;
    }
    public void Adicionar(string producto)
    {
        LanzarExcepcionSiProductoSolicitadoEsIncorrecto(producto);

        _productosFacturados.Add(producto);

        AplicarDescuento(producto);
    }

    
    public override string ToString()
    {
        return $"""
                {TextoEncabezadoRecibo}
                {CrearDetalleProductosDelRecibo()}{CrearDetallesDescuentos()}
                {TextoTotalAPagar} {CalcularTotalRecibo():C0}
                """;
    }
    
    private void AplicarDescuento(string producto)
    {
        var descuentoAAplicarONull = _descuentos.FirstOrDefault(d => d.Producto == producto);

        if (descuentoAAplicarONull == null)
            return;

        var cantidadComprada = _productosFacturados.Count(p => p == producto);

        var descuentoAplicado = descuentoAAplicarONull
            .DescuentoAAplicar(producto, cantidadComprada, _listaPrecios.ObtenerPrecioDe(producto));

        _descuentosAplicados.AddRange(descuentoAplicado);
    }

    private string CrearDetallesDescuentos()
    {
        if (_descuentosAplicados.Count == 0)
            return string.Empty;

        var encabezadoDescuentos = $"{Environment.NewLine}{TextoDescuentosAplicados}{Environment.NewLine}";

        var detalleDescuentos = _descuentosAplicados
            .Select(descuento => descuento.ToString())
            .ToArray()
            .Aggregate((acumulado, detalle) => $"{acumulado}{Environment.NewLine}{detalle}");

        return encabezadoDescuentos + detalleDescuentos;
    }

    private string CrearDetalleProductosDelRecibo()
    {
        return _productosFacturados
            .Select(producto => $"{producto}: {_listaPrecios.ObtenerPrecioDe(producto):C0}")
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
            .Select(producto => _listaPrecios.ObtenerPrecioDe(producto))
            .Sum();
    }

    private int CalcularTotalDescuentos()
    {
        return _descuentosAplicados
            .Select(d => d.ValorDescuento * -1)
            .Sum();
    }
    
    private void LanzarExcepcionSiProductoSolicitadoEsIncorrecto(string producto)
    {
        if (producto == "")
            throw new ArgumentException("Debe ingresar un producto.");

        if (!_listaPrecios.Hay(producto))
            throw new ArgumentException($"El producto {producto} no existe en el sistema.");
    }

}