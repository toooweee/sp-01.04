namespace pz_05;

class Program
{
    static int value = 1;
    static Thread generatorThread;

    static void Main(string[] args)
    {
        generatorThread = new Thread(GenerateProgressionThread);
        Thread printValueThread = new Thread(PrintValuesThread);

        generatorThread.Start();
        printValueThread.Start();

        Console.ReadLine();
    }

    static void GenerateProgressionThread()
    {
        try
        {
            while (true)
            {
                value *= 2;
                Thread.Sleep(500);
            }
        }
        catch (ThreadAbortException)
        {
            Console.WriteLine("Аварийное завершение потока");
        }
    }

    static void PrintValuesThread()
    {
        while (true)
        {
            Console.WriteLine($"Текущее значение: {value}");

            if (value >= 64)
            {
                Console.WriteLine("Достигнуто критическое значение");
                generatorThread.Abort();
                break;
            }

            if (value == 16)
            {
                Console.WriteLine("Блокировка потока генерации на 3 секунды");
                generatorThread.Suspend();

                Thread.Sleep(3000);

                Console.WriteLine("Возобновление потока генерации");
                generatorThread.Resume();
            }

            Thread.Sleep(500);
        }
    }
}