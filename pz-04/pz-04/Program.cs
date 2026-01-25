namespace pz_04;

class Program
{
    public static Database dbService = new Database();

    static void WorkerThreadMethodOne()
    {
        Console.WriteLine("Поток 1 начал");
        dbService.SaveData("Поток 1");
        Console.WriteLine("Поток 1 кончил \n");
    }
    
    static void WorkerThreadMethodTwo()
    {
        Console.WriteLine("Поток 2 начал");
        dbService.SaveData("Поток 2");
        Console.WriteLine("Поток 2 кончил \n");
    }
    
    static void Main(string[] args)
    {
        var threadOne = new Thread(new ThreadStart(WorkerThreadMethodOne));
        var threadTwo = new Thread(new ThreadStart(WorkerThreadMethodTwo));
        
        threadOne.Start();
        threadTwo.Start();
    }
}