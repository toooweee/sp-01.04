using System.Runtime.InteropServices;
using System.Text;

namespace pz_02;

class Program
{
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    static extern bool GetComputerName(StringBuilder lpBuffer, ref int lpnSize);
    
    [DllImport("kernel32.dll")]
    static extern uint GetVersion();
    
    static void Main(string[] args)
    {
        var buffer = new StringBuilder(256);
        int size = buffer.Capacity;

        if (GetComputerName(buffer, ref size))
        {
            Console.WriteLine($"Имя компьютера: {buffer.ToString()}");
        }
        
        Console.WriteLine("Каталог Windows: " + Environment.GetFolderPath(Environment.SpecialFolder.Windows));
        Console.WriteLine("Каталог System: " + Environment.SystemDirectory);
        Console.WriteLine("Каталог Temp: " + System.IO.Path.GetTempPath());
        
        uint version = GetVersion();

        uint major = version & 0xFF;
        uint minor = (version >> 8) & 0xFF;

        uint build = 0;
        if (version < 0x80000000)
        {
            build = (version >> 16) & 0xFFFF;
        }

        Console.WriteLine($"Версия Windows: {major}.{minor}, сборка {build}");
    }
}