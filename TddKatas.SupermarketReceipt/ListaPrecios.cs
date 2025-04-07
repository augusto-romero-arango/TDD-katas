namespace TddKatas.SupermarketReceipt;

public class ListaPrecios
{
    private readonly Dictionary<string, int> _precios = new()
    {
        {"Cepillo de dientes", 3000},
        {"Jabón", 2000}
    };

    public bool Hay(string producto)
    {
        return _precios.ContainsKey(producto);
    }

    public int ObtenerPrecioDe(string producto)
    {
        return _precios[producto];
    }
}