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
}