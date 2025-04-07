namespace TddKatas.SupermarketReceipt.Descuentos;

public interface IDescuento
{
    string Producto { get; }
    DescuentoAplicado[] DescuentoAAplicar(string producto, int cantidadComprada, int precio);
}