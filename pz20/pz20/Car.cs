namespace pz20;

public class Car
{
    private static Semaphore sem = new Semaphore(4, 4);
    Thread thread;
    int carNumber;

    public Car(int number)
    {
        carNumber = number;
        thread = new Thread(() => DriveToStation(carNumber));
        thread.Start();
    }

    private void DriveToStation(int carNumber)
    {
        Console.WriteLine($"Машина {carNumber} встала в очередь");
        sem.WaitOne();
        Console.WriteLine($"Машина {carNumber} начала заправляться");
        int fuelTime = new Random().Next(2000, 5000);
        Thread.Sleep(fuelTime);
        Console.WriteLine($"Машина {carNumber} заправилась за {fuelTime} и уехала");
        sem.Release();
    }
    

}