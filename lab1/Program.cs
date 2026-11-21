// Лабораторна робота №1. Варіант 5
// Записна книжка. Шаблони Delegation та Interface

using System;
using System.Collections.Generic;
using System.Text;

// Шаблон Interface: робота з адресою тільки через інтерфейс
interface IAddress
{
    void SetAddress(string city, string street, string house);
    void ChangeCity(string city);
    void ChangeStreet(string street);
    string GetAddress();
}

// Окремий клас адреси
class Address : IAddress
{
    private string city, street, house;

    public Address(string city, string street, string house)
    {
        SetAddress(city, street, house);
    }

    public void SetAddress(string city, string street, string house)
    {
        this.city = city;
        this.street = street;
        this.house = house;
    }

    public void ChangeCity(string city) { this.city = city; }
    public void ChangeStreet(string street) { this.street = street; }
    public string GetAddress() { return $"м. {city}, вул. {street}, {house}"; }
}

// Шаблон Delegation: Person не наслідує Address, а делегує їй роботу з адресою
class Person
{
    public string Name { get; }
    public string Phone { get; }
    private IAddress address;   // делегат

    public Person(string name, string phone, IAddress address)
    {
        Name = name;
        Phone = phone;
        this.address = address;
    }

    // делегування викликів об'єкту адреси
    public void SetAddress(string city, string street, string house) { address.SetAddress(city, street, house); }
    public void ChangeCity(string city) { address.ChangeCity(city); }
    public void ChangeStreet(string street) { address.ChangeStreet(street); }

    public override string ToString() { return $"{Name}, тел. {Phone}, {address.GetAddress()}"; }
}

// Записна книжка з довільною кількістю записів
class NoteBook
{
    private List<Person> records = new List<Person>();

    public void Add(Person p) { records.Add(p); }
    public bool Remove(string name) { return records.Remove(Find(name)); }
    public Person Find(string name) { return records.Find(p => p.Name == name); }

    public void Print()
    {
        if (records.Count == 0) Console.WriteLine("Книжка порожня.");
        foreach (var p in records)
            Console.WriteLine(p);
    }
}

class Program
{
    static string Ask(string text)
    {
        Console.Write(text);
        return Console.ReadLine() ?? "";
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        NoteBook book = new NoteBook();
        book.Add(new Person("Шевченко", "0671234567", new Address("Київ", "Хрещатик", "22")));
        book.Add(new Person("Коваленко", "0509876543", new Address("Львів", "Городоцька", "105")));

        while (true)
        {
            Console.WriteLine("\n=== ЗАПИСНА КНИЖКА ===");
            Console.WriteLine("1. Додати запис");
            Console.WriteLine("2. Показати всі записи");
            Console.WriteLine("3. Знайти запис");
            Console.WriteLine("4. Змінити адресу");
            Console.WriteLine("5. Видалити запис");
            Console.WriteLine("0. Вихід");
            string choice = Ask("Ваш вибір: ");
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    string name = Ask("Ім'я: ");
                    string phone = Ask("Телефон: ");
                    IAddress address = new Address(Ask("Місто: "), Ask("Вулиця: "), Ask("Будинок: "));
                    book.Add(new Person(name, phone, address));
                    Console.WriteLine("Запис додано.");
                    break;

                case "2":
                    book.Print();
                    break;

                case "3":
                    Person found = book.Find(Ask("Ім'я для пошуку: "));
                    Console.WriteLine(found != null ? found.ToString() : "Не знайдено.");
                    break;

                case "4":
                    Person p = book.Find(Ask("Ім'я: "));
                    if (p == null) { Console.WriteLine("Не знайдено."); break; }
                    p.SetAddress(Ask("Нове місто: "), Ask("Нова вулиця: "), Ask("Новий будинок: "));
                    Console.WriteLine("Адресу змінено: " + p);
                    break;

                case "5":
                    Console.WriteLine(book.Remove(Ask("Ім'я: ")) ? "Запис видалено." : "Не знайдено.");
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Невірний пункт меню.");
                    break;
            }
        }
    }
}