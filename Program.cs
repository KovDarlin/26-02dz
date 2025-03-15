using System;
using System.Diagnostics;

public class Shop : IDisposable
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string ShopType { get; set; }
    private bool _disposed = false;

    public Shop(string name, string address, string shopType)
    {
        Name = name;
        Address = address;
        ShopType = shopType;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Name supermarket: {Name}");
        Console.WriteLine($"Address supermarket: {Address}");
        Console.WriteLine($"Kind supermarket: {ShopType}");
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            Console.WriteLine($"Supermarket {Name} closed and resources released.");
            _disposed = true;
        }
    }

    ~Shop()
    {
        Dispose();
    }
}

class Program
{
    static void Main()
    {
        using (Shop shop = new Shop("ATB", "Kosmonavty", "Food"))
        {
            shop.ShowInfo();
        }
        Console.WriteLine("END");

        using (Shop shop = new Shop("Second-Hand", "Vyshenka", "Clothing"))
        {
            shop.ShowInfo();
        }
        Console.WriteLine("END");
    }
}
