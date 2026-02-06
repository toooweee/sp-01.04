#include <iostream>
#include <windows.h>

using namespace std;

int main() {
    SetConsoleOutputCP(CP_UTF8);

    SYSTEM_INFO systemInfo;
    GetSystemInfo(&systemInfo);

    DWORD pageSize = systemInfo.dwPageSize;
    cout << "Размер страницы: " << pageSize << " байта" << endl;

    LPVOID region = VirtualAlloc(
        NULL,
        pageSize,
        MEM_RESERVE,
        PAGE_READWRITE
    );

    VirtualAlloc(
        region,
        pageSize,
        MEM_COMMIT,
        PAGE_READWRITE
    );

    FillMemory(region, pageSize, 0x7f);

    MEMORY_BASIC_INFORMATION mbi;
    SIZE_T result = VirtualQuery(
        region,
        &mbi,
        sizeof(mbi)
    );

    cout << "Информация о регионе памяти: " << endl;
    cout << "BaseAddress: " << mbi.BaseAddress << endl;
    cout << "AllocationProtect: 0x" << hex << mbi.AllocationProtect << dec << endl;
    cout << "RegionSize: " << mbi.RegionSize << " байт" << endl;
    cout << "State: ";

    if (mbi.State == MEM_COMMIT)
        std::cout << "MEM_COMMIT\n";
    else if (mbi.State == MEM_RESERVE)
        std::cout << "MEM_RESERVE\n";
    else if (mbi.State == MEM_FREE)
        std::cout << "MEM_FREE\n";

    VirtualFree(region, 0, MEM_RELEASE);

    return 0;
}
