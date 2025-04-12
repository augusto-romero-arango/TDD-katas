namespace TddKatas.VendingMachine;

public enum Coin
{
    Quarter = 25,
    Dime = 10,
    Nickel = 5
}

public static class CoinExtensions
{
    public static double Valor(this Coin moneda)
    {
        return (int)moneda / 100.0;
    }
}