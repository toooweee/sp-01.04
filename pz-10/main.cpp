#include <iostream>
#include <windows.h>

using namespace std;

void PrintLastError(const char* msg)
{
    DWORD error = GetLastError();
    LPSTR buffer = nullptr;

    FormatMessageA(
        FORMAT_MESSAGE_ALLOCATE_BUFFER |
        FORMAT_MESSAGE_FROM_SYSTEM |
        FORMAT_MESSAGE_IGNORE_INSERTS,
        NULL,
        error,
        MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
        (LPSTR)&buffer,
        0,
        NULL
    );

    cout << msg << endl;
    cout << "Код ошибки: " << error << endl;
    if (buffer)
    {
        cout << "Описание: " << buffer << endl;
        LocalFree(buffer);
    }
}

int main()
{
    SetConsoleOutputCP(CP_UTF8);

    SYSTEM_INFO systemInfo;
    GetSystemInfo(&systemInfo);
    DWORD pageSize = systemInfo.dwPageSize;

    cout << "Размер страницы: " << pageSize << " байт\n";

    LPVOID region = VirtualAlloc(
        NULL,
        pageSize,
        MEM_RESERVE,
        PAGE_READWRITE
    );

    if (!region)
    {
        PrintLastError("Ошибка MEM_RESERVE");
        return 1;
    }

    if (!VirtualAlloc(
        region,
        pageSize,
        MEM_COMMIT,
        PAGE_READWRITE))
    {
        PrintLastError("Ошибка MEM_COMMIT");
        return 1;
    }

    cout << "Регион памяти: " << region << endl;

    FillMemory(region, pageSize, 0x7F);
    cout << "Память заполнена значением 0x7F\n";

    cout << "Проверка чтения (до VirtualProtect): ";
    if (IsBadReadPtr(region, pageSize))
        cout << "недоступнав\n";
    else
        cout << "доступна\n";

    DWORD oldProtect;
    if (!VirtualProtect(
        region,
        pageSize,
        PAGE_NOACCESS,
        &oldProtect))
    {
        PrintLastError("Ошибка VirtualProtect");
        return 1;
    }

    cout << "Атрибут защиты изменен на PAGE_NOACCESS\n";

    cout << "Проверка чтения (после VirtualProtect): ";
    if (IsBadReadPtr(region, pageSize))
        cout << "Недоступна\n";
    else
        cout << "доступна\n";

    MEMORY_BASIC_INFORMATION mbi;
    if (!VirtualQuery(region, &mbi, sizeof(mbi)))
    {
        PrintLastError("Ошибка VirtualQuery");
        return 1;
    }

    cout << "\nИнформация о регионе памяти:\n";
    cout << "BaseAddress: " << mbi.BaseAddress << endl;
    cout << "AllocationProtect: 0x" << hex << mbi.AllocationProtect << dec << endl;
    cout << "RegionSize:" << mbi.RegionSize << " байт\n";
    cout << "State: ";

    if (mbi.State == MEM_COMMIT)
        cout << "MEM_COMMIT\n";
    else if (mbi.State == MEM_RESERVE)
        cout << "MEM_RESERVE\n";
    else if (mbi.State == MEM_FREE)
        cout << "MEM_FREE\n";

    VirtualFree(region, 0, MEM_RELEASE);

    return 0;
}
