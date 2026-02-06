using System.Runtime.InteropServices;
using System.Threading;

namespace pz_07;

class Program
{
    [DllImport("kernel32.dll")]
    static extern IntPtr HeapCreate(uint flOptions, UIntPtr dwInitialSize, UIntPtr dwMaximumSize);

    [DllImport("kernel32.dll")]
    static extern IntPtr HeapAlloc(IntPtr hHeap, uint dwFlags, UIntPtr dwBytes);

    static void Main(string[] args)
    {
        Console.WriteLine($"{Environment.ProcessId}");
        
        Thread.Sleep(30000);
        
        for (int i = 0; i < 3; i++)
        {
            Thread thread = new Thread(Worker);
            thread.IsBackground = true;
            thread.Start(i);
        }

        IntPtr heap = HeapCreate(0, (UIntPtr)(50 * 1024 * 1024), UIntPtr.Zero);

        Console.WriteLine("Доп куча");

        IntPtr mem = HeapAlloc(
            heap,
            0,
            (UIntPtr)(100 * 1024 * 1024)
            );
        
        Console.WriteLine("Выделено 100 MB в Heap");

        Console.ReadLine();
    }

    static void Worker(object id)
    {
        Console.WriteLine($"Поток {id} запущен");

        while (true)
        {
            Thread.Sleep(1000);
        }
    }
}