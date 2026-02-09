#include <iostream>
#include <windows.h>
#include <stdio.h>

const int THREAD_COUNT = 5;

HANDLE event;

DWORD WINAPI ThreadProc(LPVOID param) {
    int id = (int)(size_t)param;
    printf("Поток %d ждет сигнала...\n", id);
    WaitForSingleObject(event, INFINITE);
    printf("Поток %d получил сигнал и продолжает работу!\n", id);
    return 0;
}

int main()
{
    SetConsoleOutputCP(CP_UTF8);

    event = CreateEvent(NULL, TRUE, FALSE, NULL);

    HANDLE threads[THREAD_COUNT];
    for (int i = 0; i < THREAD_COUNT; ++i) {
        threads[i] = CreateThread(NULL, 0, ThreadProc, (LPVOID)(size_t)i, 0, NULL);
    }

    Sleep(3000);
    printf("Главный поток посылает сигнал...\n");
    SetEvent(event);

    WaitForMultipleObjects(THREAD_COUNT, threads, TRUE, INFINITE);
    for (int i = 0; i < THREAD_COUNT; ++i) CloseHandle(threads[i]);

    CloseHandle(event);
    return 0;
}
