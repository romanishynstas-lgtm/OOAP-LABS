using System;



abstract class Phone
{
    public string Model;
    public double Price;

    public void Print()
    {
        Console.WriteLine("Телефон: " + Model + ", ціна: " + Price + " $");
    }
}


abstract class Delivery
{
    public string Country;
    public int Days;

    public void Print()
    {
        Console.WriteLine("Країна виробник: " + Country + ", термін доставки: " + Days + " днів");
    }
}



class UsaPhone : Phone
{
    public UsaPhone(string model, double basePrice)
    {
        Model = model;
        Price = basePrice; 
    }
}

class UsaDelivery : Delivery
{
    public UsaDelivery()
    {
        Country = "США";
        Days = 14;
    }
}


class ChinaPhone : Phone
{
    public ChinaPhone(string model, double basePrice)
    {
        Model = model;
        Price = basePrice * 0.7; 
    }
}

class ChinaDelivery : Delivery
{
    public ChinaDelivery()
    {
        Country = "Китай";
        Days = 30;
    }
}



interface IPhoneFactory
{
    Phone CreatePhone(string model, double basePrice);
    Delivery CreateDelivery();
}


class UsaFactory : IPhoneFactory
{
    public Phone CreatePhone(string model, double basePrice) { return new UsaPhone(model, basePrice); }
    public Delivery CreateDelivery() { return new UsaDelivery(); }
}

class ChinaFactory : IPhoneFactory
{
    public Phone CreatePhone(string model, double basePrice) { return new ChinaPhone(model, basePrice); }
    public Delivery CreateDelivery() { return new ChinaDelivery(); }
}


class OnlineShop
{
    private IPhoneFactory factory;

    public OnlineShop(IPhoneFactory factory)
    {
        this.factory = factory;
    }

    public void Order(string model, double basePrice)
    {
        Phone phone = factory.CreatePhone(model, basePrice);
        Delivery delivery = factory.CreateDelivery();

        Console.WriteLine("\n--- Ваше замовлення ---");
        phone.Print();
        delivery.Print();
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        // каталог моделей і базова ціна (у США)
        string[] models = { "Phone 15", "Phone 15 Pro", "Phone Mini" };
        double[] prices = { 800, 1100, 600 };

        Console.WriteLine("Оберіть модель:");
        for (int i = 0; i < models.Length; i++)
            Console.WriteLine((i + 1) + ". " + models[i]);
        int m = int.Parse(Console.ReadLine()) - 1;

        Console.WriteLine("Оберіть країну виробника:");
        Console.WriteLine("1. США");
        Console.WriteLine("2. Китай");
        string c = Console.ReadLine();

      
        IPhoneFactory factory;
        if (c == "1")
            factory = new UsaFactory();
        else
            factory = new ChinaFactory();

        OnlineShop shop = new OnlineShop(factory);
        shop.Order(models[m], prices[m]);

        Console.ReadKey();
    }
}