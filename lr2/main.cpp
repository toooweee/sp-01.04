#include <windows.h>
#include <psapi.h>
#include <tlhelp32.h>
#include <iostream>
#include <iomanip>
#include <string>

using namespace std;

void GetModuleInfoByHandle(HMODULE hModule, char* baseName, size_t baseSize, char* fullPath, size_t pathSize) {
    GetModuleBaseNameA(GetCurrentProcess(), hModule, baseName, (DWORD)baseSize);
    GetModuleFileNameA(hModule, fullPath, (DWORD)pathSize);
}

bool GetModuleInfoByBaseName(const char* baseName, HMODULE* hModule, char* fullPath, size_t pathSize) {
    *hModule = GetModuleHandleA(baseName);
    if (*hModule == NULL) return false;
    GetModuleFileNameA(*hModule, fullPath, (DWORD)pathSize);
    return true;
}

void DemonstrateCurrentProcess() {
    cout << "Информация о текущем процессе \n";
    DWORD pid = GetCurrentProcessId();
    cout << "PID: " << pid << "\n";

    HANDLE pseudo = GetCurrentProcess();
    cout << "Псевдодескриптор: " << pseudo << "\n";

    HANDLE duplicated = NULL;
    DuplicateHandle(GetCurrentProcess(), pseudo, GetCurrentProcess(), &duplicated, 0, FALSE, DUPLICATE_SAME_ACCESS);
    cout << "Дескриптор от DuplicateHandle: " << duplicated << "\n";

    HANDLE opened = OpenProcess(PROCESS_ALL_ACCESS, FALSE, pid);
    cout << "Дескриптор от OpenProcess: " << opened << "\n";

    CloseHandle(duplicated);
    CloseHandle(opened);
    cout << "\n";
}

void EnumProcessesThreadsModules() {
    cout << "Процессы и их модули \n";
    HANDLE hSnap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
    if (hSnap == INVALID_HANDLE_VALUE) return;

    PROCESSENTRY32 pe = { sizeof(pe) };
    if (Process32First(hSnap, &pe)) {
        do {
            cout << "\nПроцесс: " << pe.szExeFile
                 << " (PID: " << pe.th32ProcessID
                 << ", потоков: " << pe.cntThreads << ")\n";

            HANDLE hMod = CreateToolhelp32Snapshot(TH32CS_SNAPMODULE, pe.th32ProcessID);
            if (hMod != INVALID_HANDLE_VALUE) {
                MODULEENTRY32 me = { sizeof(me) };
                if (Module32First(hMod, &me)) {
                    do {
                        cout << "  Модуль: " << me.szModule
                             << " | Путь: " << me.szExePath
                             << " | Адрес: " << me.modBaseAddr
                             << " | Размер: " << me.modBaseSize << "\n";
                    } while (Module32Next(hMod, &me));
                }
                CloseHandle(hMod);
            }
        } while (Process32Next(hSnap, &pe));
    }
    CloseHandle(hSnap);

    cout << "\nВсе потоки системы \n";
    HANDLE hThread = CreateToolhelp32Snapshot(TH32CS_SNAPTHREAD, 0);
    if (hThread != INVALID_HANDLE_VALUE) {
        THREADENTRY32 te = { sizeof(te) };
        if (Thread32First(hThread, &te)) {
            do {
                cout << "Поток ID: " << te.th32ThreadID
                     << " | Владелец PID: " << te.th32OwnerProcessID
                     << " | Приоритет: " << te.tpBasePri << "\n";
            } while (Thread32Next(hThread, &te));
        }
        CloseHandle(hThread);
    }
}

void EnumDrivers() {
    cout << "\nЗагруженные драйверы устройств \n";
    void* drivers[1024];
    DWORD needed;
    if (EnumDeviceDrivers(drivers, sizeof(drivers), &needed)) {
        int count = needed / sizeof(void*);
        for (int i = 0; i < count; i++) {
            char base[MAX_PATH] = {0};
            char file[MAX_PATH] = {0};
            GetDeviceDriverBaseNameA(drivers[i], base, MAX_PATH);
            GetDeviceDriverFileNameA(drivers[i], file, MAX_PATH);
            cout << "Драйвер: " << base
                 << " | Путь: " << file
                 << " | Адрес: " << drivers[i] << "\n";
        }
    }
}

int main() {
    SetConsoleOutputCP(CP_UTF8);

    DemonstrateCurrentProcess();
    EnumProcessesThreadsModules();
    EnumDrivers();

    cout << "\nНажми Enter для выхода...\n";
    cin.get();
    return 0;
}