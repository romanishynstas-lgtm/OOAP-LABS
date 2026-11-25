using System;


interface IPhone
{
    void MakeCall(string contact);
}


class AudioPhone : IPhone
{
    public void MakeCall(string contact)
    {
        Console.WriteLine("Вмикаємо мікрофон...");
        Console.WriteLine("Голосовий дзвінок до " + contact + " розпочато.");
    }
}


class VideoCamera
{
    public void TurnOn()
    {
        Console.WriteLine("Камеру увімкнено.");
    }

    public void StartStream(string receiver)
    {
        Console.WriteLine("Відеопотік передається до " + receiver + ".");
    }

    public void TurnOff()
    {
        Console.WriteLine("Камеру вимкнено.");
    }
}


class VideoCallAdapter : IPhone
{
    private VideoCamera camera; 

    public VideoCallAdapter(VideoCamera camera)
    {
        this.camera = camera;
    }


    public void MakeCall(string contact)
    {
        Console.WriteLine("Вмикаємо мікрофон...");
        camera.TurnOn();
        camera.StartStream(contact);
        Console.WriteLine("Відеодзвінок до " + contact + " розпочато.");
    }

    public void EndCall()
    {
        camera.TurnOff();
    }
}


class Skype
{
    public void Call(IPhone phone, string contact)
    {
        Console.WriteLine("\n[Skype] Виклик абонента " + contact + "...");
        phone.MakeCall(contact);
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        Skype skype = new Skype();
        IPhone audio = new AudioPhone();
        VideoCallAdapter video = new VideoCallAdapter(new VideoCamera());

        while (true)
        {
            Console.WriteLine("\n===== SKYPE =====");
            Console.WriteLine("1. Голосовий дзвінок");
            Console.WriteLine("2. Відеодзвінок");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();

            if (choice == "0")
                break;

            if (choice != "1" && choice != "2")
            {
                Console.WriteLine("Невірний пункт меню.");
                continue;
            }

            Console.Write("Кому дзвонимо: ");
            string contact = Console.ReadLine();

            if (choice == "1")
            {
                skype.Call(audio, contact);
            }
            else
            {
                skype.Call(video, contact); 
            }

            Console.WriteLine("Натисніть Enter, щоб завершити дзвінок...");
            Console.ReadLine();

            if (choice == "2")
                video.EndCall();
            Console.WriteLine("Дзвінок завершено.");
        }
    }
}