#include <iostream>
#include <windows.h>

using namespace std;

int main() {
    SetConsoleOutputCP(CP_UTF8);

    MEMORYSTATUSEX memInfo;
    memInfo.dwLength = sizeof(MEMORYSTATUSEX);

    if (!GlobalMemoryStatusEx(&memInfo)) {
        cout << "Ошибка" << endl;
        return 1;
    }

    cout << "Процент используемой памяти: " << memInfo.dwMemoryLoad << "%" << endl;

    cout << "Физическая память всего: "
            << memInfo.ullTotalPhys / (1024 * 1024) << " MB" << endl;

    cout << "Физическая память доступна: "
            << memInfo.ullAvailPhys / (1024 * 1024) << " MB\n\n";

    cout << "Файл подкачки всего: "
            << memInfo.ullTotalPageFile / (1024 * 1024) << " MB\n";

    cout << "Файл подкачки доступен: "
            << memInfo.ullAvailPageFile / (1024 * 1024) << " MB\n\n";

    cout << "Виртуальная память всего: "
            << memInfo.ullTotalVirtual / (1024 * 1024) << " MB\n";

    cout << "Виртуальная память доступна: "
            << memInfo.ullAvailVirtual / (1024 * 1024) << " MB\n";

    return 0;
}
