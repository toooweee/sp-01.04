namespace pz_04;

public class Database
{
    public void SaveData(string text)
    {
        Console.WriteLine("Сохранение...");

        for (var i = 0; i <= 5; i++)
        {
            Console.WriteLine($"[{text}] запись {i}");
            Thread.Sleep(3000);
        }

        Console.WriteLine("Сохранение завершено");
    }
}