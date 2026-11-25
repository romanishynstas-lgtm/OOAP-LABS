using System;
using System.Collections.Generic;




interface IAddress
{
    void SetAddress(string city, string street, string house);
    void ChangeCity(string city);
    void ChangeStreet(string street);
    string GetAddress();
}


class Address : IAddress
{
    private string city;
    private string street;
    private string house;

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

    public string GetAddress()
    {
        return "м. " + city + ", вул. " + street + ", буд. " + house;
    }
}


class Person
{
    public string Name;
    public string Phone;
    private IAddress address; // об'єкт-делегат

    public Person(string name, string phone, IAddress address)
    {
        Name = name;
        Phone = phone;
        this.address = address;
    }

  
    public void ChangeCity(string city) { address.ChangeCity(city); }
    public void ChangeStreet(string street) { address.ChangeStreet(street); }
    public void SetAddress(string city, string street, string house) { address.SetAddress(city, street, house); }

    public void Print()
    {
        Console.WriteLine(Name + ", тел. " + Phone + ", " + address.GetAddress());
    }
}


class NoteBook
{
    private List<Person> people = new List<Person>();

    public void Add(Person p) { people.Add(p); }

    public Person Find(string name)
    {
        return people.Find(p => p.Name == name);
    }

    public void PrintAll()
    {
        foreach (Person p in people)
            p.Print();
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        NoteBook book = new NoteBook();
        book.Add(new Person("Іван Петренко", "0501112233", new Address("Київ", "Хрещатик", "10")));
        book.Add(new Person("Олена Коваленко", "0672223344", new Address("Львів", "Городоцька", "45")));
        book.Add(new Person("Андрій Шевчук", "0933334455", new Address("Одеса", "Дерибасівська", "7")));

        Console.WriteLine("Усі записи:");
        book.PrintAll();

    
        Person p = book.Find("Іван Петренко");
        p.ChangeCity("Харків");
        p.ChangeStreet("Сумська");

        Person p2 = book.Find("Олена Коваленко");
        p2.SetAddress("Дніпро", "Яворницького", "20");

        Console.WriteLine("\nПісля зміни адрес:");
        book.PrintAll();

        Console.ReadKey();
    }
}