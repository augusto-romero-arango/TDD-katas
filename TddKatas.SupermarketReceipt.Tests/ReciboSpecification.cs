namespace TddKatas.SupermarketReceipt.Tests;

public class ReciboSpecification
{
    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_cepillo_totalizar_en_3000()
    {
        var recibo = new Recibo();

        recibo.Adicionar("Cepillo de dientes");

        Assert.Equal(@"Factura
Cepillo de dientes: $ 3.000
TOTAL FACTURA: $ 3.000", recibo.ToString());
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_jabon_totalizar_en_2000()
    {
        var recibo = new Recibo();

        recibo.Adicionar("Jabón");

        Assert.Equal(@"Factura
Jabón: $ 2.000
TOTAL FACTURA: $ 2.000", recibo.ToString());
    }

    [Fact]
    public void Debe_lanzar_excepcion_cuando_adiciono_un_producto_no_existente()
    {
        var recibo = new Recibo();

        Action accionAdicionar = () => recibo.Adicionar("Cerveza");
        
        var ex = Assert.Throws<ArgumentException>(accionAdicionar);
        Assert.Equal("El producto Cerveza no existe en el sistema.", ex.Message);
    }

    [Fact]
    public void Debe_lanzar_excepcion_cuando_adiciono_un_producto_es_vacio()
    {
        var recibo = new Recibo();

        Action accionAdicionar = () => recibo.Adicionar("");
        
        var ex = Assert.Throws<ArgumentException>(accionAdicionar);
        Assert.Equal("Debe ingresar un producto.", ex.Message);
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_cepillo_y_un_jabon_totalizar_en_5000()
    {
        var recibo = new Recibo();

        recibo.Adicionar("Cepillo de dientes");
        recibo.Adicionar("Jabón");

        Assert.Equal(@"Factura
Cepillo de dientes: $ 3.000
Jabón: $ 2.000
TOTAL FACTURA: $ 5.000", recibo.ToString());
    }
}

public class Recibo
{
    private readonly List<string> _productosFacturados = new();

    private readonly Dictionary<string, int> _precios = new()
    {
        {"Cepillo de dientes", 3000},
        {"Jabón", 2000}
    };

    public void Adicionar(string producto)
    {
        if (producto == "")
            throw new ArgumentException("Debe ingresar un producto.");

        if (!_precios.ContainsKey(producto))
            throw new ArgumentException($"El producto {producto} no existe en el sistema.");

        _productosFacturados.Add(producto);
    }

    public override string ToString()
    {
        return $"""
                Factura
                {CrearDetalleDeProductosParaLaFactura()}
                TOTAL FACTURA: {CalcularTotalFactura():C0}
                """;
    }

    private string CrearDetalleDeProductosParaLaFactura()
    {
        var detallePorProducto = _productosFacturados
            .Select(producto => $"{producto}: {_precios[producto]:C0}")
            .ToArray();

        return string.Join(Environment.NewLine, detallePorProducto);
    }

    private int CalcularTotalFactura()
    {
        int totalFactura = _productosFacturados
            .Select(producto => _precios[producto])
            .Sum();
        return totalFactura;
    }
}