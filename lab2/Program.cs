// Лабораторна робота №2. Варіант 5
// Інтернет-магазин телефонів. Шаблон Abstract Factory (Абстрактна фабрика)

using System;
using System.Text;

// Базовий клас для всіх телефонів
abstract class Phone
{
    public string Model { get; }
    public string Country { get; }
    public double Price { get; }
    public int DeliveryDays { get; }

    protected Phone(string model, string country, double price, int deliveryDays)
    {
        Model = model;
        Country = country;
        Price = price;
        DeliveryDays = deliveryDays;
    }

    public override string ToString()
    {
        return $"Телефон: {Model}\nКраїна виробника: {Country}\n" +
               $"Вартість: {Price} $\nТермін доставки: {DeliveryDays} днів";
    }
}

// Абстрактні продукти — моделі телефонів (однакові в обох країнах)
abstract class NovaX : Phone
{
    protected NovaX(string country, double price, int days) : base("Nova X", country, price, days) { }
}

abstract class NovaLite : Phone
{
    protected NovaLite(string country, double price, int days) : base("Nova Lite", country, price, days) { }
}

// Конкретні продукти: телефони, зібрані в США
class UsaNovaX : NovaX
{
    public UsaNovaX() : base("США", 999, 7) { }
}

class UsaNovaLite : NovaLite
{
    public UsaNovaLite() : base("США", 499, 7) { }
}

// Конкретні продукти: телефони, зібрані у філіалі в Китаї
class ChinaNovaX : NovaX
{
    public ChinaNovaX() : base("Китай", 699, 21) { }
}

class ChinaNovaLite : NovaLite
{
    public ChinaNovaLite() : base("Китай", 349, 21) { }
}

// Абстрактна фабрика: створює сімейство телефонів
interface IPhoneFactory
{
    NovaX CreateNovaX();
    NovaLite CreateNovaLite();
}

// Конкретна фабрика: завод у США
class UsaPhoneFactory : IPhoneFactory
{
    public NovaX CreateNovaX() { return new UsaNovaX(); }
    public NovaLite CreateNovaLite() { return new UsaNovaLite(); }
}

// Конкретна фабрика: філіал у Китаї
class ChinaPhoneFactory : IPhoneFactory
{
    public NovaX CreateNovaX() { return new ChinaNovaX(); }
    public NovaLite CreateNovaLite() { return new ChinaNovaLite(); }
}

// Інтернет-магазин (клієнт): працює тільки з IPhoneFactory і Phone,
// не знаючи, яка саме фабрика і які конкретні класи використовуються
class OnlineShop
{
    private IPhoneFactory factory;

    public OnlineShop(IPhoneFactory factory)
    {
        this.factory = factory;
    }

    public Phone Order(int model)
    {
        if (model == 1) return factory.CreateNovaX();
        if (model == 2) return factory.CreateNovaLite();
        return null;
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("\n=== ІНТЕРНЕТ-МАГАЗИН ТЕЛЕФОНІВ NOVA ===");
            Console.WriteLine("Оберіть країну виробника:");
            Console.WriteLine("1. США");
            Console.WriteLine("2. Китай");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");
            string country = Console.ReadLine();

            IPhoneFactory factory;
            if (country == "1") factory = new UsaPhoneFactory();
            else if (country == "2") factory = new ChinaPhoneFactory();
            else if (country == "0") return;
            else { Console.WriteLine("Невірний вибір."); continue; }

            Console.WriteLine("\nОберіть модель:");
            Console.WriteLine("1. Nova X");
            Console.WriteLine("2. Nova Lite");
            Console.Write("Ваш вибір: ");
            int.TryParse(Console.ReadLine(), out int model);

            OnlineShop shop = new OnlineShop(factory);
            Phone phone = shop.Order(model);

            if (phone == null)
            {
                Console.WriteLine("Невірна модель.");
                continue;
            }

            Console.WriteLine("\nЗамовлення оформлено!");
            Console.WriteLine(phone);
        }
    }
}