namespace pz20;

class Program
{
    static void Main(string[] args)
    {
        for (int i = 1; i <= 10; i++)
        {
            Car car = new Car(i);
            Thread.Sleep(250);
        }

        Console.ReadLine();
    }
}

