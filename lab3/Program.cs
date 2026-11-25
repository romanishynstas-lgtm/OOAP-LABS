using System;
using System.Collections.Generic;
using System.IO;


class Student
{
    public string Name;
    public string BirthDate;
    public string Faculty;
    public string Group;
    public bool Budget; 

    public Student(string name, string birthDate, string faculty, string group, bool budget)
    {
        Name = name;
        BirthDate = birthDate;
        Faculty = faculty;
        Group = group;
        Budget = budget;
    }


    public Student Clone()
    {
        return new Student(Name, BirthDate, Faculty, Group, Budget);
    }

    public string Info()
    {
        string form = Budget ? "державне замовлення" : "за кошти фізичних осіб";
        return Name + ", д.н. " + BirthDate + ", " + Faculty + ", гр. " + Group + ", " + form;
    }
}


abstract class Room
{
    public int Number;
    public string Type;
    public int Places;
    public double BudgetRent;  
    public double ContractRent; 
    public List<Student> Students = new List<Student>();

  
    public abstract Room Clone();

 
    protected void CopyTo(Room copy)
    {
        copy.Number = Number;
        foreach (Student s in Students)
            copy.Students.Add(s.Clone());
    }

    public bool AddStudent(Student s)
    {
        if (Students.Count >= Places)
        {
            Console.WriteLine("Кімната " + Number + " заповнена! " + s.Name + " не заселено.");
            return false;
        }
        Students.Add(s);
        return true;
    }

    public double GetRent(Student s)
    {
        return s.Budget ? BudgetRent : ContractRent;
    }

    public string Report()
    {
        string text = "Кімната №" + Number + "\n";
        text += "Тип: " + Type + "\n";
        text += "Мешканців: " + Students.Count + " з " + Places + "\n";
        foreach (Student s in Students)
            text += "  - " + s.Info() + "; квартплата: " + GetRent(s) + " грн\n";
        return text;
    }
}

class DoubleRoom : Room
{
    public DoubleRoom()
    {
        Type = "двомісна";
        Places = 2;
        BudgetRent = 900;
        ContractRent = 1200;
    }

    public override Room Clone()
    {
        DoubleRoom copy = new DoubleRoom();
        CopyTo(copy);
        return copy;
    }
}

class TripleRoom : Room
{
    public TripleRoom()
    {
        Type = "трьохмісна";
        Places = 3;
        BudgetRent = 700;
        ContractRent = 1000;
    }

    public override Room Clone()
    {
        TripleRoom copy = new TripleRoom();
        CopyTo(copy);
        return copy;
    }
}


class Dormitory
{
    private List<Room> rooms = new List<Room>();

    public void AddRoom(Room r) { rooms.Add(r); }


    public void SaveReport(string fileName)
    {
        string text = "ЗВІТ ПО ГУРТОЖИТКУ\n\n";
        foreach (Room r in rooms)
            text += r.Report() + "\n";

        File.WriteAllText(fileName, text);
        Console.WriteLine(text);
        Console.WriteLine("Звіт збережено у файл " + fileName);
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

      
        Room doubleProto = new DoubleRoom();
        Room tripleProto = new TripleRoom();

   
        Student studentProto = new Student("", "", "ФІОТ", "ІП-21", true);


        Room r101 = doubleProto.Clone();
        r101.Number = 101;

        Room r102 = tripleProto.Clone();
        r102.Number = 102;

        Room r103 = doubleProto.Clone();
        r103.Number = 103;

        // створюємо студентів клонуванням прототипу
        Student s1 = studentProto.Clone();
        s1.Name = "Петренко Іван Олегович";
        s1.BirthDate = "12.03.2005";

        Student s2 = studentProto.Clone();
        s2.Name = "Коваль Андрій Петрович";
        s2.BirthDate = "05.07.2005";
        s2.Budget = false;

        Student s3 = studentProto.Clone();
        s3.Name = "Шевчук Максим Ігорович";
        s3.BirthDate = "21.11.2004";

        Student s4 = studentProto.Clone();
        s4.Name = "Мельник Олег Васильович";
        s4.BirthDate = "30.01.2005";
        s4.Faculty = "ФЕЛ";
        s4.Group = "ДЕ-22";

        Student s5 = new Student("Бондар Ярослав Миколайович", "14.09.2004", "ФЕЛ", "ДЕ-22", false);

        r101.AddStudent(s1);
        r101.AddStudent(s2);

        r102.AddStudent(s3);
        r102.AddStudent(s4);
        r102.AddStudent(s5);

        Student s6 = studentProto.Clone();
        s6.Name = "Ткаченко Богдан Сергійович";
        s6.BirthDate = "08.04.2005";
        r103.AddStudent(s6);

     
        Room r104 = r101.Clone();
        r104.Number = 104;
        r104.Students[0].Name = "Лисенко Дмитро Андрійович";

        // глибока копія: зміна в r104 не зачепила r101
        Console.WriteLine("У кімнаті 101 перший мешканець: " + r101.Students[0].Name);
        Console.WriteLine("У кімнаті 104 перший мешканець: " + r104.Students[0].Name + "\n");

        Dormitory dorm = new Dormitory();
        dorm.AddRoom(r101);
        dorm.AddRoom(r102);
        dorm.AddRoom(r103);
        dorm.AddRoom(r104);

        dorm.SaveReport("report.txt");

        Console.ReadKey();
    }
}
