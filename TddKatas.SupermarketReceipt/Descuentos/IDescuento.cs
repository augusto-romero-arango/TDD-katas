namespace TddKatas.SupermarketReceipt.Descuentos;

public interface IDescuento
{
    TipoDescuento TipoDescuento { get; }
    string Producto { get; }
    DescuentoAplicado[] DescuentoAAplicar(string producto, int cantidadComprada, int precio);
}