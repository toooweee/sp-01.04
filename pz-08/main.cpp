#include <iostream>
#include <windows.h>

using namespace std;

int main() {
    SetConsoleOutputCP(CP_UTF8);

    SYSTEM_INFO systemInfo;
    GetSystemInfo(&systemInfo);

    DWORD pageSize = systemInfo.dwPageSize;
    cout << "Размер страницы: " << pageSize << " байта" << endl;

    SIZE_T regionSize = pageSize * 2;

    LPVOID firstRegion = VirtualAlloc(
        NULL,
        regionSize,
        MEM_RESERVE,
        PAGE_READWRITE
    );

    LPVOID secondRegion = VirtualAlloc(
        NULL,
        regionSize,
        MEM_RESERVE,
        PAGE_READWRITE
    );

    cout << "Адресс первого региона: " << firstRegion << endl;
    cout << "Адресс второго региона: " << secondRegion << endl;

    VirtualAlloc(firstRegion, regionSize, MEM_COMMIT, PAGE_READWRITE);
    VirtualAlloc(secondRegion, regionSize, MEM_COMMIT, PAGE_READWRITE);

    ZeroMemory(firstRegion, regionSize);

    int value;
    cout << "Введите число от 0 до 127" << endl;
    cin >> value;

    if (value < 0 || value > 127) {
        cout << "Невернй диапазон значений" << endl;
        return 1;
    }

    FillMemory(secondRegion, regionSize, (BYTE)value);

    cout << "Содержимое первого региона" << endl;;
    BYTE* p1 = (BYTE*)firstRegion;
    for (int i = 0; i < 32; i++) {
        cout << (int)p1[i] << " ";
    }

    cout << endl;

    cout << "Содержимое второго региона" << endl;;
    BYTE* p2 = (BYTE*)secondRegion;
    for (int i = 0; i < 32; i++) {
        cout << (int)p2[i] << " ";
    }

    VirtualFree(firstRegion, 0, MEM_RELEASE);
    VirtualFree(secondRegion, 0, MEM_RELEASE);

    return 0;
}