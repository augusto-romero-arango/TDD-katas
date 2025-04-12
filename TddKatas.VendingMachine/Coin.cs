namespace TddKatas.VendingMachine;

public enum Coin
{
    Quarter = 25,
    Dime = 10,
    Nickel = 5
}

public static class CoinExtensions
{
    public static decimal Valor(this Coin moneda)
    {
        return (int)moneda / 100m;
    }
    
    public static List<Coin> InferioresA(this List<Coin> monedas, decimal diferencia)
    {
        return monedas
            .Where(m => m.Valor() <= diferencia)
            .OrderByDescending(m => m.Valor())
            .ToList();
    }
    
    public static List<Coin> ObtenerHastaCompletar(this IEnumerable<Coin> monedas, decimal valorACompletar)
    {
        var vueltas = new List<Coin>();
        decimal acumulado = 0;

        foreach (var moneda in monedas)
        {
            if (acumulado >= valorACompletar)
                break;

            vueltas.Add(moneda);
            acumulado += moneda.Valor();
        }

        return vueltas;
    }
    
    public static decimal Totalizar(this IEnumerable<Coin> monedas)
    {
        return monedas
            .Select(m => m.Valor())
            .Sum();
    }
}